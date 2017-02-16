using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPositionComponent))]
[RequireComponent(typeof(ResourceSink))]
public class Conveyor : MonoBehaviour {
    public Direction InitialOrientation;
    public Direction Orientation;
    public GridPosition ExitLocation
    {
        get
        {
            return PositionHolder.Position.InDirection(Orientation);
        }
    }
    public float ConveyingTimeSeconds = 0.5f;

    private ConveyorSolver Solver;
    private GridPositionComponent PositionHolder;
    private ResourceSink ItemSource;

    private float LastConveyanceTime;
    private Vector2 initialPosition;

    [HideInInspector]
    public bool ReadyToOffer;
    [HideInInspector]
    public Resource CurrentlyConveyedItem;

    // Use this for initialization
    void Awake()
    {
        Solver = GetComponentInParent<ConveyorSolver>();

        PositionHolder = GetComponent<GridPositionComponent>();

        ItemSource = GetComponent<ResourceSink>();
        ItemSource.CanAcceptItem = (itemType) => CurrentlyConveyedItem == null;
        ItemSource.DeliverItem = (item) =>
        {
            CurrentlyConveyedItem = item;
            LastConveyanceTime = Time.time;
            initialPosition = item.transform.position;
            return false;
        };

        ReadyToOffer = false;

        Orientation = InitialOrientation;
	}

    void Start()
    {
        // This guy will handle deciding if we're blocked or not, and giving us input, at Update time
        Solver.RegisterConveyor(this);
    }

    void OnDestroy()
    {
        Solver.DeregisterConveyor(this);

        if (CurrentlyConveyedItem != null)
        {
            Destroy(CurrentlyConveyedItem.gameObject);
        }
    }

    void LateUpdate ()
    {
        if (CurrentlyConveyedItem != null)
        {
            if (!ReadyToOffer)
            {
                //do two-phase movement; from entry point to center to exit point
                float conveyanceProportion = (Time.time - LastConveyanceTime) / ConveyingTimeSeconds;

                if (conveyanceProportion < 0.5f)
                {
                    //phase 1; range 0-0.5
                    //double that to 0-1
                    CurrentlyConveyedItem.transform.position = Vector2.Lerp(initialPosition, transform.position, 2 * conveyanceProportion);
                }
                else if (conveyanceProportion < 1.0f)
                {
                    //phase 2; range 0.5-1
                    //double that to 1-2 and shift down by 1 to 0-1
                    CurrentlyConveyedItem.transform.position = Vector2.Lerp(transform.position, getExitPosition(), conveyanceProportion * 2 - 1);
                }
                else
                {
                    //complete movement
                    CurrentlyConveyedItem.transform.position = getExitPosition();

                    //complete conveying the item
                    ReadyToOffer = true;

                    LastConveyanceTime += ConveyingTimeSeconds;
                }
            }
        }
    }

    /// <returns>True iff this conveyor is pointing at a ResourceSink that can currently accept an item.</returns>
    public bool CanDeliverItem()
    {
        GridPositionComponent targetMachine = GetComponent<GridPositionComponent>().Grid.GetGridObjectAt(ExitLocation);
        if (targetMachine == null
            || targetMachine.GetComponent<ResourceSink>() == null)
        {
            return false;
        }
        if (CurrentlyConveyedItem == null)
        {
            return false;
        }

        return targetMachine.GetComponent<ResourceSink>().CanAcceptItem(CurrentlyConveyedItem.ResourceType);
    }

    /// <summary>
    /// Delivers the item this conveyor is holding to its destination, if possible.
    /// </summary>
    /// <returns>true iff the item was accepted.</returns>
    public bool DeliverItem()
    {
        if (PositionHolder.Grid.GetGridObjectAt(ExitLocation).GetComponent<ResourceSink>().OfferItem(CurrentlyConveyedItem))
        {
            CurrentlyConveyedItem = null;
            ReadyToOffer = false;
            return true;
        }

        return false;
    }

    private Vector2 getExitPosition()
    {
        // this isn't right, should depend on scale somehow
        return ((Vector2)PositionHolder.Grid.gridToWorldSpace(PositionHolder.Position) + PositionHolder.Grid.gridToWorldSpace(ExitLocation)) / 2;
    }
}

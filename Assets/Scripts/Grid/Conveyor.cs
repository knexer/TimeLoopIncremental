using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPositionComponent))]
[RequireComponent(typeof(ResourceSink))]
public class Conveyor : MonoBehaviour {
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
    public bool ReadyToOffer;
    public Resource CurrentlyConveyedItem;

    // Use this for initialization
    void Start()
    {
        Solver = GetComponentInParent<ConveyorSolver>();
        // This guy will handle deciding if we're blocked or not, and giving us input, at Update time
        Solver.RegisterConveyor(this);

        PositionHolder = GetComponent<GridPositionComponent>();
        PositionHolder.Grid.SetGridObjectAt(PositionHolder.Position, PositionHolder);

        ItemSource = GetComponent<ResourceSink>();
        ItemSource.DeliverItem = (item) =>
        {
            CurrentlyConveyedItem = item;
            LastConveyanceTime = Time.time;
            initialPosition = item.transform.position;
            return false;
        };

        ReadyToOffer = false;
	}

    void LateUpdate ()
    {
        if (CurrentlyConveyedItem != null)
        {
            if (!ReadyToOffer)
            {
                Debug.Log("Lerp factor: " + (Time.time - LastConveyanceTime) / ConveyingTimeSeconds);
                //lerp from initial to final
                CurrentlyConveyedItem.transform.position = Vector2.Lerp(initialPosition, getExitPosition(), (Time.time - LastConveyanceTime) / ConveyingTimeSeconds);
                if (LastConveyanceTime + ConveyingTimeSeconds <= Time.time)
                {
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

        return targetMachine.GetComponent<ResourceSink>().CanAcceptItem;
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
            ItemSource.CanAcceptItem = true;
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

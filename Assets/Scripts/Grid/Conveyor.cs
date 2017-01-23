using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPositionComponent))]
[RequireComponent(typeof(ResourceSink))]
public class Conveyor : MonoBehaviour {
    public Grid Grid;
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
    public bool ReadyToOffer;
    public Resource CurrentlyConveyedItem;

    // Use this for initialization
    void Start()
    {
        Solver = Grid.gameObject.transform.parent.gameObject.GetComponent<ConveyorSolver>();
        // This guy will handle deciding if we're blocked or not, and giving us input, at Update time
        Solver.RegisterConveyor(this);

        PositionHolder = GetComponent<GridPositionComponent>();

        ItemSource = GetComponent<ResourceSink>();
        ItemSource.DeliverItem = (item) =>
        {
            CurrentlyConveyedItem = item;
            return false;
        };

        ReadyToOffer = false;
	}

    void LateUpdate ()
    {
        /// CurrentyConveyedItem is updated for us at Update time
        if (CurrentlyConveyedItem == null)
        {
            //check if any items are available for conveyance, and pick one up if so
            LastConveyanceTime = Time.time;
        }
        else
        {
            //lerp from initial to final
            CurrentlyConveyedItem.transform.position = Vector2.Lerp(CurrentlyConveyedItem.transform.position, getExitPosition(), Time.deltaTime / (LastConveyanceTime + ConveyingTimeSeconds - Time.time));
            if (LastConveyanceTime + ConveyingTimeSeconds >= Time.time)
            {
                //complete conveying the item
                ReadyToOffer = true;

                LastConveyanceTime += ConveyingTimeSeconds;
            }
        }
    }

    /// <returns>True iff this conveyor is pointing at a ResourceSink that can currently accept an item.</returns>
    public bool CanDeliverItem()
    {
        return GetComponent<GridPositionComponent>().Grid.GetGridObjectAt(ExitLocation).GetComponent<ResourceSink>().CanAcceptItem;
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
        return ((Vector2)Grid.gridToWorldSpace(PositionHolder.Position) + Grid.gridToWorldSpace(ExitLocation)) / 2;
    }
}

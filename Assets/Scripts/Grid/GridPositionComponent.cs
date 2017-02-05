using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to GameObjects that exist in a simulation Grid.
/// </summary>
public class GridPositionComponent : MonoBehaviour {
    public GridPosition InitialPosition;

    public Grid Grid;
    public GridPosition Position
    {
        get
        {
            return PositionImpl;
        }
        set
        {
            Grid.SetGridObjectAt(PositionImpl, null);
            PositionImpl = value;
            transform.position = Grid.gridToWorldSpace(PositionImpl);
            Grid.SetGridObjectAt(PositionImpl, this);
        }
    }

    private GridPosition PositionImpl;

	// Use this for initialization
	void Awake () {
        // Establish relationship with the containing Grid.
        // This object should be fully initialized (parent transform and initial position) before it's enabled for the first time.
        Grid = gameObject.transform.GetComponentInParent<Grid>();
        PositionImpl = InitialPosition;
    }

    void Start ()
    {
        Position = InitialPosition;
    }
}

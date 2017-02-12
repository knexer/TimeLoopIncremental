using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attach to GameObjects that exist in a simulation Grid.
/// </summary>
public class GridPositionComponent : MonoBehaviour {
    public GridPosition InitialPosition;

    public Grid Grid;
    public GridPosition Position
    {
        get; private set;
    }

	// Use this for initialization
	void Awake () {
        // Establish relationship with the containing Grid.
        // This object should be fully initialized (parent transform and initial position) before it's enabled for the first time.
        Grid = gameObject.transform.GetComponentInParent<Grid>();
        Position = InitialPosition;
    }

    void Start ()
    {
        transform.position = Grid.gridToWorldSpace(Position);
    }

    public void RegisterAsMachine()
    {
        Grid.SetGridObjectAt(Position, this);
    }

    public IEnumerable<GridPositionComponent> GetAdjacentComponents()
    {
        List<GridPosition> adjacentPositions = new List<GridPosition>();
        adjacentPositions.Add(new GridPosition(Position.X + 1, Position.Y));
        adjacentPositions.Add(new GridPosition(Position.X - 1, Position.Y));
        adjacentPositions.Add(new GridPosition(Position.X, Position.Y + 1));
        adjacentPositions.Add(new GridPosition(Position.X, Position.Y - 1));

        return adjacentPositions
            .Select((pos) => Grid.GetGridObjectAt(pos))
            .Where((pos) => pos != null);
    }
}

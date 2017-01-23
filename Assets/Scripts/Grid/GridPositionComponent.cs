using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to GameObjects that exist in a simulation Grid.
/// </summary>
public class GridPositionComponent : MonoBehaviour {
    public int XInitial;
    public int YInitial;

    public Grid Grid;
    public GridPosition Position
    {
        get
        {
            return PositionImpl;
        }
        set
        {
            
            PositionImpl = value;
            transform.position = Grid.gridToWorldSpace(PositionImpl);
        }
    }

    private GridPosition PositionImpl;

	// Use this for initialization
	void Start () {
        Grid = gameObject.transform.GetComponentInParent<Grid>();
        Position = new GridPosition(XInitial, YInitial);
	}
}

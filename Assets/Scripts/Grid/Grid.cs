using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConveyorSolver))]
public class Grid : MonoBehaviour {

    //Unity configurables
    public int Width = 1;
    public int Height = 1;
    public GameObject CellPrefab;
    public int NumInputs = 1;
    public GridInput InputPrefab;

    private GameObject[,] GridCells;
    private GridPositionComponent[,] GridObjects;

	// Use this for initialization
	void Awake () {
        // Populate the grid
        GridCells = new GameObject[Width, Height];
        for (int w = 0; w < Width; w++)
        {
            for (int h = 0; h < Height; h++)
            {
                GridCells[w, h] = InstantiateCell(new GridPosition(w, h));
            }
        }

        GridObjects = new GridPositionComponent[Width, Height];
        
        // Create the inputs
        for (int i = 0; i < NumInputs; i++)
        {
            float cellFractionToTheLeft = ((float) i + 1) / (NumInputs + 1);
            int cellsToTheLeft = Mathf.FloorToInt(cellFractionToTheLeft * Width);

            GridInput currentInput = Instantiate(InputPrefab, transform, false);

            currentInput.ProvidedResource = ResourceType.IRON;

            GridPositionComponent positionHolder = currentInput.GetComponent<GridPositionComponent>();
            positionHolder.XInitial = cellsToTheLeft;
            positionHolder.YInitial = 0;
        }
	}

    private GameObject InstantiateCell(GridPosition pos)
    {
        GameObject cell = Instantiate(CellPrefab, transform, false);

        cell.name += " " + pos.ToString();
        cell.transform.position = gridToWorldSpace(pos);

        return cell;
    }

    public bool IsInGrid(GridPosition pos)
    {
        return pos.X >= 0 && pos.X < Width
            && pos.Y >= 0 && pos.Y < Height;
    }

    public GridPositionComponent GetGridObjectAt(GridPosition pos)
    {
        if (!IsInGrid(pos)) return null;
        return GridObjects[pos.X, pos.Y];
    }

    /// <summary>
    /// Sets the GridObject at pos to to.
    /// </summary>
    /// <param name="pos">The position to update.</param>
    /// <param name="to">The object to place.</param>
    /// <returns>The object previously at that location, or null if there was nothing there.</returns>
    public GridPositionComponent SetGridObjectAt(GridPosition pos, GridPositionComponent to)
    {
        GridPositionComponent previous = GetGridObjectAt(pos);

        GridObjects[pos.X, pos.Y] = to;

        return previous;
    }

    /// <summary>
    /// Converts a GridPosition to a Vector2 in Unity world space.
    /// </summary>
    /// <param name="pos">A GridPosition.</param>
    /// <returns>A Vector2 describing the corresponding point in Unity world space.</returns>
    public Vector2 gridToWorldSpace(GridPosition pos)
    {
        Vector2 cellSizeLocalSpace = CellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector2 gridSizeLocalSpace = new Vector2(cellSizeLocalSpace.x, cellSizeLocalSpace.y);
        gridSizeLocalSpace.Scale(new Vector2(Width, Height));
        Vector2 topLeft = gridSizeLocalSpace / -2;
        Vector2 cellOffsetLocalSpace = new Vector2(cellSizeLocalSpace.x, cellSizeLocalSpace.y);
        cellOffsetLocalSpace.Scale(pos.ToVector());
        cellOffsetLocalSpace += topLeft;
        cellOffsetLocalSpace += cellSizeLocalSpace / 2;

        return transform.TransformVector(cellOffsetLocalSpace) + transform.position;
    }
}

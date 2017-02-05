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
    public float CellWidth = 1.0f;
    public float CellHeight = 1.0f;
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
                GridCells[w, h].GetComponent<HotkeyListener>().SpawnPosition = new GridPosition(w, h);
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
            positionHolder.InitialPosition = new GridPosition(cellsToTheLeft, 0);
        }
	}

    void Start ()
    {
        //Resize the cells
        foreach (GameObject cell in GridCells)
        {
            Vector2 cellSize = cell.GetComponent<SpriteRenderer>().sprite.bounds.size;
            cell.transform.localScale = new Vector2(CellWidth / cellSize.x, CellHeight / cellSize.y);
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
        if (pos == null) return false;
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
    /// Spawns an instance of machinePrefab at pos iff there's not already a machine at pos.
    /// </summary>
    /// <param name="machinePrefab">The prefab to spawn an instance of.</param>
    /// <param name="pos">The position to place the instance at.</param>
    /// <returns>The spawned machine, or null if pos was already occupied or was not contained in the grid.</returns>
    public GameObject TrySpawnMachineAt(GameObject machinePrefab, GridPosition pos, Action<GameObject> beforeAwake = null)
    {
        if (!IsInGrid(pos)) return null;

        if (GetGridObjectAt(pos) != null) return null;

        return ForceSpawnMachineAt(machinePrefab, pos, beforeAwake);
    }

    /// <summary>
    /// Spawns an instance of machinePrefab at pos, replacing whatver machine was already at pos.
    /// </summary>
    /// <param name="machinePrefab">The prefab to spawn an instance of.</param>
    /// <param name="pos">The position to place the instance at.</param>
    /// <returns>The spawned machine, or null if pos was not contained in the grid.</returns>
    public GameObject ForceSpawnMachineAt(GameObject machinePrefab, GridPosition pos, Action<GameObject> beforeAwake = null)
    {
        if (!IsInGrid(pos)) return null;

        bool wasActive = machinePrefab.activeSelf;

        machinePrefab.SetActive(false);

        GameObject machine = Instantiate(machinePrefab, transform);
        machine.GetComponent<GridPositionComponent>().InitialPosition = pos;

        beforeAwake(machine);

        machine.SetActive(wasActive);
        machinePrefab.SetActive(wasActive);

        return machine;
    }

    /// <summary>
    /// Converts a GridPosition to a Vector2 in Unity world space.
    /// </summary>
    /// <param name="pos">A GridPosition.</param>
    /// <returns>A Vector2 describing the corresponding point in Unity world space.</returns>
    public Vector2 gridToWorldSpace(GridPosition pos)
    {
        Vector2 gridSizeLocalSpace = new Vector2(CellWidth, CellHeight);
        gridSizeLocalSpace.Scale(new Vector2(Width, Height));
        Vector2 topLeft = gridSizeLocalSpace / -2;
        Vector2 cellOffsetLocalSpace = new Vector2(CellWidth, CellHeight);
        cellOffsetLocalSpace.Scale(pos.ToVector());
        cellOffsetLocalSpace += topLeft;
        cellOffsetLocalSpace += new Vector2(CellWidth, CellHeight) / 2;

        return transform.TransformVector(cellOffsetLocalSpace) + transform.position;
    }
}

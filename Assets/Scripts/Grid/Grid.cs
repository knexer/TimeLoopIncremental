﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConveyorSolver))]
public class Grid : MonoBehaviour {

    private const float CellWidth = 1;
    private const float CellHeight = 1;

    //Unity configurables
    [SerializeField] private int Width = 1;
    [SerializeField] private int Height = 1;
    [SerializeField] private GameObject CellPrefab;
    [SerializeField] private int NumInputs = 1;
    [SerializeField] private GridInput InputPrefab;
    [SerializeField] private int NumOutputs = 1;
    [SerializeField] private GridOutput OutputPrefab;

    private GameObject[,] GridCells;
    private GridPositionComponent[,] GridObjects;

	// Use this for initialization
    private void Awake () {
        // scale the grid to correspond to the size indicated in the UI layout configuration
        LayoutElement size = GetComponent<LayoutElement>();
        transform.localScale = new Vector2(size.preferredWidth / Width, size.preferredHeight / Height);

        // Populate the grid
        CreateCells();
	    CreateInputs();
        CreateOutputs();
    }

    private void CreateCells()
    {
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
    }

    private void CreateInputs()
    {
        for (int i = 0; i < NumInputs; i++)
        {
            float cellFractionToTheLeft = ((float) i + 1) / (NumInputs + 1);
            int cellsToTheLeft = Mathf.FloorToInt(cellFractionToTheLeft * Width);

            GridInput currentInput = ForceSpawnMachineAt(InputPrefab, new GridPosition(cellsToTheLeft, 0));

            Array resourceTypes = Enum.GetValues(typeof(ResourceType));
            currentInput.ProvidedResource = (ResourceType) resourceTypes.GetValue(i % resourceTypes.Length);
        }
    }

    private void CreateOutputs()
    {
        for (int i = 0; i < NumOutputs; i++)
        {
            float cellFractionToTheLeft = ((float) i + 1) / (NumOutputs + 1);
            int cellsToTheLeft = Mathf.FloorToInt(cellFractionToTheLeft * Width);

            GridOutput currentOutput = ForceSpawnMachineAt(OutputPrefab, new GridPosition(cellsToTheLeft, Height - 1));
        }
    }

    private void Start ()
    {
        //Resize the cells
        foreach (GameObject cell in GridCells)
        {
            Vector2 cellSize = cell.GetComponent<SpriteRenderer>().sprite.bounds.size;
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

    public T TrySpawnMachineAt<T>(T prefab, GridPosition pos) where T : MonoBehaviour
    {
        GameObject gameObject = TrySpawnMachineAt(prefab.gameObject, pos);
        return gameObject.GetComponent<T>();
    }

    /// <summary>
    /// Spawns an instance of machinePrefab at pos iff there's not already a machine at pos.
    /// </summary>
    /// <param name="machinePrefab">The prefab to spawn an instance of.</param>
    /// <param name="pos">The position to place the instance at.</param>
    /// <returns>The spawned machine, or null if pos was already occupied, was not contained in the grid, or if the player can't afford the machine.</returns>
    public GameObject TrySpawnMachineAt(GameObject machinePrefab, GridPosition pos)
    {
        if (!IsInGrid(pos)) return null;

        if (GetGridObjectAt(pos) != null) return null;

        return ForceSpawnMachineAt(machinePrefab, pos);
    }

    public T ForceSpawnMachineAt<T>(T prefab, GridPosition pos) where T : MonoBehaviour
    {
        GameObject gameObject = ForceSpawnMachineAt(prefab.gameObject, pos);
        return gameObject.GetComponent<T>();
    }

    /// <summary>
    /// Spawns an instance of machinePrefab at pos, replacing whatver machine was already at pos.
    /// </summary>
    /// <param name="machinePrefab">The prefab to spawn an instance of.</param>
    /// <param name="pos">The position to place the instance at.</param>
    /// <returns>The spawned machine, or null if pos was not contained in the grid or the player can't afford the machine.</returns>
    public GameObject ForceSpawnMachineAt(GameObject machinePrefab, GridPosition pos)
    {
        if (!IsInGrid(pos)) return null;

        PlacementCostComponent cost = machinePrefab.GetComponent<PlacementCostComponent>();
        if (cost != null && !GetComponentInParent<ResourceStorage>().Resources.RemoveResources(cost.Cost))
        {
            return null;
        }

        bool wasActive = machinePrefab.activeSelf;

        machinePrefab.SetActive(false);

        GameObject machine = Instantiate(machinePrefab, transform, false);
        machine.GetComponent<GridPositionComponent>().InitialPosition = pos;

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

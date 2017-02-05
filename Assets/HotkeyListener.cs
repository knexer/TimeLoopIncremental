﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyListener : MonoBehaviour {
    /// <summary>
    /// The grid position to create machines at.
    /// </summary>
    [HideInInspector]
    public GridPosition SpawnPosition;

    public GameObject ConveyorPrefab;

    private Grid ContainingGrid;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ContainingGrid.TrySpawnMachineAt(ConveyorPrefab, SpawnPosition, (machine) => machine.GetComponent<Conveyor>().InitialOrientation = Direction.UP);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ContainingGrid.TrySpawnMachineAt(ConveyorPrefab, SpawnPosition, (machine) => machine.GetComponent<Conveyor>().InitialOrientation = Direction.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ContainingGrid.TrySpawnMachineAt(ConveyorPrefab, SpawnPosition, (machine) => machine.GetComponent<Conveyor>().InitialOrientation = Direction.DOWN);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
             ContainingGrid.TrySpawnMachineAt(ConveyorPrefab, SpawnPosition, (machine) => machine.GetComponent<Conveyor>().InitialOrientation = Direction.RIGHT);
        }
    }

	// Use this for initialization
	void Start () {
        ContainingGrid = GetComponentInParent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

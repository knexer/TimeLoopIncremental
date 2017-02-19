﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyListener : MonoBehaviour {
    /// <summary>
    /// The grid position to create machines at.
    /// </summary>
    [HideInInspector]
    public GridPosition SpawnPosition;

    private Grid ContainingGrid;
    private BuildablePrefabs Buildables;

    void OnMouseOver()
    {
        foreach (GameObject buildable in Buildables.Buildables)
        {
            HotkeyBuildable hotkey = buildable.GetComponent<HotkeyBuildable>();
            if (Input.GetKey(hotkey.Hotkey.ToLowerInvariant()))
            {
                if (hotkey.IsDestroy)
                {
                    GridPositionComponent currentMachine = ContainingGrid.GetGridObjectAt(SpawnPosition);
                    if (currentMachine != null) {
                        currentMachine.GetComponent<GridMachine>().TryDestroy();
                    }
                }
                else if (hotkey.IsUpgrade)
                {
                    GridPositionComponent currentMachine = ContainingGrid.GetGridObjectAt(SpawnPosition);
                    if (currentMachine != null)
                    {
                        Upgradeable upgradeable = currentMachine.GetComponent<Upgradeable>();
                        if (upgradeable != null)
                        {
                            upgradeable.TryDoUpgrade(transform.GetComponentInParent<ResourceStorage>());
                        }
                    }
                }
                else
                {
                    BuildPrestigeAction buildAction = new BuildPrestigeAction(buildable, SpawnPosition);
                    if (buildAction.ApplyChangeToPrestige(ContainingGrid.gameObject))
                    {
                        ContainingGrid.GetComponentInParent<PrestigeRecorder>().RecordAction(buildAction);
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        ContainingGrid = GetComponentInParent<Grid>();
        Buildables = GetComponentInParent<BuildablePrefabs>();

        // Don't allow editing of previous grids.
        FindObjectOfType<PrestigeController>().OnPrestige += (previousPrestige) =>
        {
            Destroy(this);
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

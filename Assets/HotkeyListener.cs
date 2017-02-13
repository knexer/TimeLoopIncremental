using System.Collections;
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
            if (Input.GetKeyDown(hotkey.Hotkey.ToLowerInvariant()))
            {
                if (hotkey.IsDestroy)
                {
                    ContainingGrid.GetGridObjectAt(SpawnPosition).GetComponent<GridMachine>().TryDestroy();
                }
                else
                {
                    ContainingGrid.TrySpawnMachineAt(buildable, SpawnPosition);
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        ContainingGrid = GetComponentInParent<Grid>();
        Buildables = GetComponentInParent<BuildablePrefabs>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

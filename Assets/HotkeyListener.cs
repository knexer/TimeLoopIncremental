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
    private ResourceStorage PlayerResources;
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
                        if (currentMachine.GetComponent<GridMachine>() != null)
                        {
                            DeletePrestigeAction deleteAction = new DeletePrestigeAction(new Resources(PlayerResources.Resources), SpawnPosition);
                            deleteAction.ApplyChangeToPrestige(ContainingGrid.gameObject);
                            ContainingGrid.GetComponentInParent<PrestigeRecorder>().RecordAction(deleteAction);
                        }
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
                            Resources playerResourcesCopy = new Resources(PlayerResources.Resources);
                            if (upgradeable.TryDoUpgrade(transform.GetComponentInParent<ResourceStorage>()))
                            {
                                UpgradePrestigeAction upgradeAction = new UpgradePrestigeAction(playerResourcesCopy, SpawnPosition);
                                ContainingGrid.GetComponentInParent<PrestigeRecorder>().RecordAction(upgradeAction);
                            }
                        }
                    }
                }
                else
                {
                    BuildPrestigeAction buildAction = new BuildPrestigeAction(buildable, new Resources(PlayerResources.Resources), SpawnPosition);
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
        PlayerResources = GetComponentInParent<ResourceStorage>();
        Buildables = GetComponentInParent<BuildablePrefabs>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

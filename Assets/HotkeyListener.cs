using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyListener : MonoBehaviour {
    /// <summary>
    /// The grid position to create machines at.
    /// </summary>
    [HideInInspector]
    public GridPosition SpawnPosition;
    
    private ResourceStorage PlayerResources;
    private BuildablePrefabs Buildables;
    private PrestigeRecorder ActionRecorder;

    private void OnMouseOver()
    {
        foreach (GameObject buildable in Buildables.Buildables)
        {
            HotkeyBuildable hotkey = buildable.GetComponent<HotkeyBuildable>();
            if (Input.GetKey(hotkey.Hotkey.ToLowerInvariant()))
            {
                if (hotkey.IsDestroy)
                {
                    DeletePrestigeAction deleteAction = new DeletePrestigeAction(new Resources(PlayerResources.Resources), SpawnPosition);
                    ActionRecorder.ApplyAndRecordAction(deleteAction);
                }
                else if (hotkey.IsUpgrade)
                {
                    UpgradePrestigeAction upgradeAction = new UpgradePrestigeAction(new Resources(PlayerResources.Resources), SpawnPosition);
                    ActionRecorder.ApplyAndRecordAction(upgradeAction);
                }
                else
                {
                    BuildPrestigeAction buildAction = new BuildPrestigeAction(buildable, new Resources(PlayerResources.Resources), SpawnPosition);
                    ActionRecorder.ApplyAndRecordAction(buildAction);
                }
            }
        }
    }

	// Use this for initialization
    private void Start () {
        PlayerResources = GetComponentInParent<ResourceStorage>();
        Buildables = GetComponentInParent<BuildablePrefabs>();
        ActionRecorder = GetComponentInParent<PrestigeRecorder>();
	}
}

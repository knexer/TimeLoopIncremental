using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BuildPrestigeAction : IPrestigeAction
{
    private GameObject BuildablePrefab;
    private GridPosition SpawnPosition;
    public Resources Cost
    {
        get; private set;
    }

    public BuildPrestigeAction(GameObject buildablePrefab, GridPosition spawnPosition)
    {
        BuildablePrefab = buildablePrefab;
        SpawnPosition = spawnPosition;
        PlacementCostComponent costHolder = buildablePrefab.GetComponent<PlacementCostComponent>();
        if (costHolder != null)
        {
            Cost = costHolder.Cost;
        } else
        {
            Cost = new Resources();
        }
    }

    public bool ApplyChangeToPrestige(GameObject prestige)
    {
        return prestige.GetComponentInChildren<Grid>().TrySpawnMachineAt(BuildablePrefab, SpawnPosition) != null;
    }
}

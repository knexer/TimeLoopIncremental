using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BuildPrestigeAction : IPrestigeAction
{
    private GameObject BuildablePrefab;
    private GridPosition SpawnPosition;
    public Resources ResourcesThreshold
    {
        get; private set;
    }
    public Sprite Sprite
    {
        get; private set;
    }

    public BuildPrestigeAction(GameObject buildablePrefab, Resources playerStorage, GridPosition spawnPosition)
    {
        BuildablePrefab = buildablePrefab;
        SpawnPosition = spawnPosition;
        ResourcesThreshold = playerStorage;
        Sprite = buildablePrefab.GetComponent<SpriteRenderer>().sprite;
    }

    public bool ApplyChangeToPrestige(GameObject prestige)
    {
        return prestige.GetComponentInChildren<Grid>().TrySpawnMachineAt(BuildablePrefab, SpawnPosition) != null;
    }
}

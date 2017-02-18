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
        get { return BuildablePrefab.GetComponent<PlacementCostComponent>().Cost; }
    }

    public void ApplyChangeToPrestige(GameObject prestige)
    {
        throw new NotImplementedException();
    }

    public bool CanApplyChangeToPrestige(GameObject prestige)
    {
        throw new NotImplementedException();
    }
}

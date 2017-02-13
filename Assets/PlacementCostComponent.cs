using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementCostComponent : MonoBehaviour {
    /// <summary>
    /// Editor-driven cost.
    /// </summary>
    [SerializeField]
    private ResourceAmount[] Cost;

    private Dictionary<ResourceType, int> CostDict
    {
        get
        {
            // Merge duplicate resource types
            return Cost
            .GroupBy(cost => cost.Type, cost => cost.Amount)
            .ToDictionary(group => group.Key, group => group.Sum());
        }
    }

    public bool CostIsMet(ResourceStorage storage)
    {
        return CostDict.All(pair => pair.Value <= storage.GetResourceAmount(pair.Key));
    }

    public bool ExactCost(ResourceStorage storage)
    {
        if (!CostIsMet(storage))
        {
            return false;
        }

        CostDict.All(pair => storage.RemoveResource(pair.Key, pair.Value));

        return true;
    }
}

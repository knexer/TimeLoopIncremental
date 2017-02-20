using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Resources
{
    [SerializeField]
    private ResourceAmount[] ResourceAmounts;

    private Dictionary<ResourceType, int> _internalCostDict;
    private Dictionary<ResourceType, int> CostDict
    {
        get
        {
            if (_internalCostDict == null)
            {
                if (ResourceAmounts == null)
                {
                    ResourceAmounts = new ResourceAmount[0];
                }

                // Merge duplicate resource types
                _internalCostDict = ResourceAmounts
                    .GroupBy(cost => cost.Type, cost => cost.Amount)
                    .ToDictionary(group => group.Key, group => group.Sum());
            }
            return _internalCostDict;
        }
    }

    public Resources() { }

    public Resources(Resources original)
    {
        foreach ( KeyValuePair<ResourceType, int> resource in original.CostDict )
        {
            CostDict[resource.Key] = resource.Value;
        }
    }

    public void AddResource(ResourceType type, int amount)
    {
        if (!CostDict.ContainsKey(type))
        {
            CostDict[type] = 0;
        }

        CostDict[type] += amount;
    }

    public void AddResource(ResourceAmount resource)
    {
        AddResource(resource.Type, resource.Amount);
    }

    public int GetResourceAmount(ResourceType type)
    {
        int amount;
        CostDict.TryGetValue(type, out amount);
        return amount;
    }

    public bool RemoveResource(ResourceType type, int amount)
    {
        if (GetResourceAmount(type) < amount)
        {
            return false;
        }

        CostDict[type] -= amount;
        return true;
    }

    public bool RemoveResource(ResourceAmount resource)
    {
        return RemoveResource(resource.Type, resource.Amount);
    }

    public bool IsAtLeast(Resources other)
    {
        Dictionary<ResourceType, int> otherResources = other.CostDict;

        return otherResources.All(kvp => GetResourceAmount(kvp.Key) >= other.GetResourceAmount(kvp.Key));
    }

    public bool RemoveResources(Resources other)
    {
        if (!IsAtLeast(other))
        {
            return false;
        }

        other.CostDict.All(pair => RemoveResource(pair.Key, pair.Value));
        return true;
    }
}

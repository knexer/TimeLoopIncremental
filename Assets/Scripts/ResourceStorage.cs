﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour {
    private Dictionary<ResourceType, long> Resources = new Dictionary<ResourceType, long>();

    public void AddResource(ResourceType type, long amount)
    {
        if (!Resources.ContainsKey(type)) {
            Resources[type] = 0;
        }

        Resources[type] += amount;
    }

    public long GetResourceAmount(ResourceType type)
    {
        long amount;
        Resources.TryGetValue(type, out amount);
        return amount;
    }

    public bool RemoveResource(ResourceType type, int amount)
    {
        if (GetResourceAmount(type) < amount)
        {
            return false;
        }

        Resources[type] -= amount;
        return true;
    }
}

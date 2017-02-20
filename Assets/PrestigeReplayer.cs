using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeReplayer : MonoBehaviour {
    public List<IPrestigeAction> actions;

    private int NextAction;

    public void Init(List<IPrestigeAction> actionsToReplay)
    {
        if (actions != null)
        {
            throw new InvalidOperationException("PrestigeReplayer has already been initiailized.");
        }

        actions = actionsToReplay;
    }

	// Use this for initialization
	void Start () {
        NextAction = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (actions == null) return;
        ResourceStorage currentStorage = GetComponent<ResourceStorage>();

        while (NextAction < actions.Count
            && currentStorage.Resources.IsAtLeast(actions[NextAction].ResourcesThreshold)
            && actions[NextAction].ApplyChangeToPrestige(gameObject)) {
            NextAction++;
        }
	}
}

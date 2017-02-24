using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PrestigeActions))]
public class PrestigeReplayer : MonoBehaviour {
    private PrestigeActions Actions;

    private int NextAction;

	// Use this for initialization
	void Start () {
        Actions = GetComponent<PrestigeActions>();
        NextAction = 0;
	}
	
	// Update is called once per frame
	void Update () {
        ResourceStorage currentStorage = GetComponent<ResourceStorage>();

        while (NextAction < Actions.Actions.Count
            && currentStorage.Resources.IsAtLeast(Actions.Actions[NextAction].ResourcesThreshold)
            && Actions.Actions[NextAction].ApplyChangeToPrestige(gameObject)) {
            NextAction++;
        }
	}
}

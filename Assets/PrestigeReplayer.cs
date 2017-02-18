using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeReplayer : MonoBehaviour {
    private List<IPrestigeAction> RecordedActions;

    private int NextAction;

    private PrestigeController CurrentPrestigeRef;

    public void Init(List<IPrestigeAction> actionsToReplay)
    {
        if (RecordedActions == null)
        {
            throw new InvalidOperationException("PrestigeReplayer has already been initiailized.");
        }

        RecordedActions = actionsToReplay;
    }

	// Use this for initialization
	void Start () {
        CurrentPrestigeRef = transform.GetComponentInParent<PrestigeController>();
        CurrentPrestigeRef.OnPrestige += Reset;
        Reset(CurrentPrestigeRef.CurrentPrestige);
	}

    private void Reset(GameObject currentPrestige)
    {
        NextAction = 0;
    }
	
	// Update is called once per frame
	void Update () {
        ResourceStorage currentStorage = CurrentPrestigeRef.CurrentPrestige.GetComponent<ResourceStorage>();

        while (NextAction < RecordedActions.Count
            && currentStorage.Resources.IsAtLeast(RecordedActions[NextAction].Cost)
            && RecordedActions[NextAction].CanApplyChangeToPrestige(CurrentPrestigeRef.CurrentPrestige)) {
            RecordedActions[NextAction].ApplyChangeToPrestige(CurrentPrestigeRef.CurrentPrestige);
            NextAction++;
        }
	}
}

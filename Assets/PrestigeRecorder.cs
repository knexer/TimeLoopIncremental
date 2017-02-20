﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeRecorder : MonoBehaviour {
    public List<IPrestigeAction> actions;

    public void ApplyAndRecordAction(IPrestigeAction action)
    {
        if (action.ApplyChangeToPrestige(gameObject))
        {
            actions.Add(action);
        }
    }

	// Use this for initialization
	void Start () {
        actions = new List<IPrestigeAction>();
	}
}

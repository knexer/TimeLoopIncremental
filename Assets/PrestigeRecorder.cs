using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PrestigeActions))]
public class PrestigeRecorder : MonoBehaviour {
    PrestigeActions Actions;

    public void ApplyAndRecordAction(IPrestigeAction action)
    {
        if (action.ApplyChangeToPrestige(gameObject))
        {
            Actions.Actions.Add(action);
        }
    }

	// Use this for initialization
	void Start () {
        Actions = GetComponent<PrestigeActions>();
	}
}

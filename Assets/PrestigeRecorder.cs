using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeRecorder : MonoBehaviour {
    private List<IPrestigeAction> actions;

    public void RecordAction(IPrestigeAction action)
    {
        actions.Add(action);
    }

	// Use this for initialization
	void Start () {
        transform.GetComponentInParent<PrestigeController>().OnPrestige += HandlePrestige;
	}

    private void HandlePrestige(GameObject currentPrestige)
    {
        // pass the torch
        gameObject.AddComponent<PrestigeReplayer>().Init(actions);

        Destroy(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

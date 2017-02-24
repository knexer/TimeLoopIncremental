using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeController : MonoBehaviour {
    //// UNITY CONFIGURATION
    [SerializeField]
    private GameObject PrestigePrefab;
    //// END UNITY CONFIGURATION

    [HideInInspector]
    public GameObject CurrentPrestige;

	// Use this for initialization
	void Awake () {
        DoPrestige();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            DoPrestige();
        }
	}

    private void DoPrestige()
    {
        List<string> names = new List<string>();
        List<List<IPrestigeAction>> replays = new List<List<IPrestigeAction>>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject prestige = transform.GetChild(i).gameObject;
            names.Add(prestige.name);
            replays.Add(prestige.GetComponent<PrestigeActions>().Actions);
            Destroy(prestige);
        }

        for (int i = 0; i < names.Count; i++)
        {
            GameObject previousPrestige = Instantiate(PrestigePrefab, transform, false);
            previousPrestige.name = names[i];
            Destroy(previousPrestige.GetComponent<PrestigeRecorder>());
            previousPrestige.GetComponent<PrestigeActions>().Actions = replays[i];
            previousPrestige.AddComponent<PrestigeReplayer>();
        }
        
        CurrentPrestige = Instantiate(PrestigePrefab, transform, false);
        CurrentPrestige.name = "Prestige " + names.Count + 1;
    }
}

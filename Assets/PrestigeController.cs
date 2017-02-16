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

    /// <summary>
    /// Fired immediately before a prestige occurs.
    /// GameObject parameter: the current prestige.
    /// </summary>
    public event Action<GameObject> OnPrestige;

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
        if (OnPrestige != null)
        {
            OnPrestige(CurrentPrestige);
        }

        CurrentPrestige = Instantiate(PrestigePrefab, transform, false);
        CurrentPrestige.name = "Prestige " + transform.childCount;
    }
}

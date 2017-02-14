using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeController : MonoBehaviour {
    public GameObject PrestigePrefab;

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
        CurrentPrestige = Instantiate(PrestigePrefab, transform, false);
        CurrentPrestige.name = "Prestige " + transform.childCount;
    }
}

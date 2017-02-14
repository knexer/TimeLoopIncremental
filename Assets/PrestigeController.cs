using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeController : MonoBehaviour {
    public GameObject PrestigePrefab;

    [HideInInspector]
    public GameObject CurrentPrestige;

	// Use this for initialization
	void Awake () {
        CurrentPrestige = Instantiate(PrestigePrefab, transform, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

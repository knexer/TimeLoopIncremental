using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays the build queue of a previous prestige in the UI.
/// </summary>
public class PreviousQueueController : MonoBehaviour {
    private PrestigeActions Actions;
    private GameObject UpcomingQueue;

	// Use this for initialization
	void Start () {
        Actions = GetComponentInParent<PrestigeActions>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

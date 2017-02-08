using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPositionComponent))]
[RequireComponent(typeof(ResourceSink))]
public class GridOutput : MonoBehaviour {
    private ResourceStorage ItemDestination;
    private ResourceSink ItemSource;

	// Use this for initialization
	void Start () {
        ItemDestination = GetComponentInParent<ResourceStorage>();
        ItemSource = GetComponent<ResourceSink>();

        ItemSource.DeliverItem = (item) =>
        {
            Debug.Log(new System.Diagnostics.StackTrace());
            Debug.Log(ItemDestination);
            Debug.Log(item);
            ItemDestination.AddResource(item.ResourceType, 1);
            // TODO destroy the in-game item
            return true;
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

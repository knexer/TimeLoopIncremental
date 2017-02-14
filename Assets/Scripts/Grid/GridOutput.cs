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
            ItemDestination.AddResource(item.ResourceType, 1);
            Destroy(item.gameObject);
            return true;
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

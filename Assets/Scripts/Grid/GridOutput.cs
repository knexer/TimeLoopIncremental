using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPositionComponent))]
[RequireComponent(typeof(ResourceSink))]
public class GridOutput : MonoBehaviour {
    private ResourceStorage ItemDestination;
    private ResourceSink ItemSource;
    
    private void Start () {
        ItemDestination = GetComponentInParent<ResourceStorage>();
        ItemSource = GetComponent<ResourceSink>();

        ItemSource.CanAcceptItem = (itemType) => true;
        ItemSource.DeliverItem = (item) =>
        {
            ItemDestination.Resources.AddResource(item.ResourceType, 1);
            Destroy(item.gameObject);
            return true;
        };
	}
}

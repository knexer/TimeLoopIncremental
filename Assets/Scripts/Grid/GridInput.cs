using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPositionComponent))]
public class GridInput : MonoBehaviour {
    public ResourceType? ProvidedResource = null;
    public float ProvidedPerSecond;

    private GridPositionComponent PositionHolder;

	// Use this for initialization
	void Start () {
        PositionHolder = GetComponent<GridPositionComponent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ProvidedResource != null)
        {
            ResourceType providedResource = ProvidedResource.Value;
            
            // Create resource(s) and insert them into the grid at the correct location.
            ResourceSink itemDestination = PositionHolder.Grid.GetGridObjectAt(PositionHolder.Position).GetComponent<ResourceSink>();
            if (itemDestination.CanAcceptItem)
            {
                itemDestination.OfferItem(new Resource() { ResourceType = providedResource });
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridPositionComponent))]
public class GridInput : MonoBehaviour {
    public Resource ResourcePrefab;
    public ResourceType? ProvidedResource = null;
    public float ProvidedPerSecond;

    private GridPositionComponent PositionHolder;

	// Use this for initialization
	void Start () {
        PositionHolder = GetComponent<GridPositionComponent>();
	}
	
	// Update is called once per frame
	void Update () {
        // Create resource(s) and insert them into the grid at the correct location.
        GridPositionComponent machineAtLocation = PositionHolder.Grid.GetGridObjectAt(PositionHolder.Position);
        if (machineAtLocation != null)
        {
            ResourceSink itemDestination = machineAtLocation.GetComponent<ResourceSink>();
            ResourceType providedResource = ProvidedResource ?? RandomResource();
            if (itemDestination != null && itemDestination.CanAcceptItem(providedResource))
            {
                SpawnItemAt(itemDestination, providedResource);
            }
        }
    }

    private void SpawnItemAt(ResourceSink itemDestination, ResourceType providedResource)
    {
        Resource input = Instantiate(ResourcePrefab, PositionHolder.Grid.transform, false);

        input.ResourceType = providedResource;
        input.transform.position = itemDestination.GetComponent<GridPositionComponent>().Grid.gridToWorldSpace(itemDestination.GetComponent<GridPositionComponent>().Position);

        if (!itemDestination.OfferItem(input))
        {
            Destroy(input);
        }
    }

    private ResourceType RandomResource()
    {
        return (ResourceType) Random.Range(0, 3);
    }
}

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
        if (ProvidedResource != null)
        {
            // Create resource(s) and insert them into the grid at the correct location.
            GridPositionComponent machineAtLocation = PositionHolder.Grid.GetGridObjectAt(PositionHolder.Position);
            if (machineAtLocation != null)
            {
                ResourceSink itemDestination = machineAtLocation.GetComponent<ResourceSink>();
                if (itemDestination != null && itemDestination.CanAcceptItem)
                {
                    SpawnItemAt(itemDestination);
                }
            }
        }
    }

    private void SpawnItemAt(ResourceSink itemDestination)
    {
        ResourceType providedResource = ProvidedResource.Value;

        Resource input = Instantiate(ResourcePrefab, PositionHolder.Grid.transform, false);

        input.ResourceType = RandomResource();
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

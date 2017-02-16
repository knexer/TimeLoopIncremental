using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceSink))]
[RequireComponent(typeof(GridMachine))]
public class AssemblyComponent : MonoBehaviour {
    public ResourceType input;
    public ResourceType output;

    public Resource ResourcePrefab;

    private int inputQuantity = 0;
    private int outputQuantity = 0;

	// Use this for initialization
	void Start () {
        GetComponent<ResourceSink>().CanAcceptItem = (itemType) => itemType.Equals(input);
        
        GetComponent<ResourceSink>().DeliverItem = (item) =>
        {
            inputQuantity++;

            Destroy(item.gameObject);

            return true;
        };
	}
	
	// Update is called once per frame
	void Update () {
        outputQuantity += inputQuantity;
        inputQuantity = 0;
        IEnumerable<GridPositionComponent> exitLocations = GetComponent<GridPositionComponent>().GetAdjacentComponents()
            .Where((pos) => pos.GetComponent<ResourceSink>() != null)
            .Where((pos) => !(pos.GetComponent<Conveyor>() != null && pos.GetComponent<Conveyor>().ExitLocation.Equals(GetComponent<GridPositionComponent>().Position)));

        foreach (GridPositionComponent exitLocation in exitLocations)
        {
            TrySpawnOutputAt(exitLocation);
        }
	}

    private void TrySpawnOutputAt(GridPositionComponent position)
    {
        if (outputQuantity <= 0)
        {
            return;
        }

        if (!position.GetComponent<ResourceSink>().CanAcceptItem(output))
        {
            return;
        }

        outputQuantity--;

        Resource outputResource = Instantiate(ResourcePrefab, transform.parent);
        outputResource.ResourceType = output;
        outputResource.transform.position = transform.position;
        position.GetComponent<ResourceSink>().OfferItem(outputResource);
    }
}

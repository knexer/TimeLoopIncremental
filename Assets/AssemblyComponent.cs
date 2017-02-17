using System;
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

    public float CraftedPerSecond;

    private float lastCraftedTime = 0;
    private int inputQuantity = 0;
    private int outputQuantity = 0;
    private Upgradeable upgradeLevel;

    // Use this for initialization
    void Start () {
        GetComponent<ResourceSink>().CanAcceptItem = (itemType) => itemType.Equals(input) && inputQuantity == 0;
        
        GetComponent<ResourceSink>().DeliverItem = (item) =>
        {
            inputQuantity++;

            Destroy(item.gameObject);

            return true;
        };

        upgradeLevel = GetComponent<Upgradeable>();
	}
	
	// Update is called once per frame
	void Update () {
        if (lastCraftedTime + 1 / (CraftedPerSecond * upgradeLevel.UpgradeLevel) <= Time.time)
        {
            if (inputQuantity > 0 && outputQuantity < 4)
            {
                outputQuantity += Mathf.FloorToInt(Mathf.Sqrt(upgradeLevel.UpgradeLevel));
                inputQuantity--;

            }
        }

        if (outputQuantity > 0)
        {
            IEnumerable<GridPositionComponent> exitLocations = GetComponent<GridPositionComponent>().GetAdjacentComponents()
                        .Where((pos) => pos.GetComponent<ResourceSink>() != null)
                        .Where((pos) => !(pos.GetComponent<Conveyor>() != null && pos.GetComponent<Conveyor>().ExitLocation.Equals(GetComponent<GridPositionComponent>().Position)));

            foreach (GridPositionComponent exitLocation in exitLocations)
            {
                if (TrySpawnOutputAt(exitLocation))
                {
                    lastCraftedTime = Time.time;
                    outputQuantity--;
                    if (outputQuantity <= 0)
                    {
                        break;
                    }
                }
            }
        }
	}

    private bool TrySpawnOutputAt(GridPositionComponent position)
    {
        if (!position.GetComponent<ResourceSink>().CanAcceptItem(output))
        {
            return false;
        }

        Resource outputResource = Instantiate(ResourcePrefab, transform.parent);
        outputResource.ResourceType = output;
        outputResource.transform.position = transform.position;
        position.GetComponent<ResourceSink>().OfferItem(outputResource);

        return true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceStorageDisplay : MonoBehaviour
{
    public GameObject ResourceDisplayPrefab;
    public ResourceSprites ResourceSprites;

    private ResourceStorage Storage;
    private Dictionary<ResourceType, Text> ResourceDisplays;

    void Awake()
    {
        Storage = GetComponentInParent<ResourceStorage>();
        ResourceDisplays = new Dictionary<ResourceType, Text>();

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            GameObject resourceDisplay = Instantiate(ResourceDisplayPrefab, gameObject.transform, false);
            ResourceDisplays[type] = resourceDisplay.transform.FindChild("Text").GetComponent<Text>();
            resourceDisplay.transform.FindChild("Image").GetComponent<Image>().sprite = ResourceSprites.GetSpriteForResourceType(type);
        }
    }
	
	void Update () {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            ResourceDisplays[type].text = ": " + Storage.Resources.GetResourceAmount(type);
        }
	}
}

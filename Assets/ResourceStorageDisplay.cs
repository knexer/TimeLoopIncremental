using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceStorageDisplay : MonoBehaviour
{
    [SerializeField] private GameObject ResourceDisplayPrefab;
    [SerializeField] private ResourceSprites ResourceSprites;

    private ResourceStorage Storage;
    private Dictionary<ResourceType, Text> ResourceDisplays;

    private void Awake()
    {
        Storage = GetComponentInParent<ResourceStorage>();
        ResourceDisplays = new Dictionary<ResourceType, Text>();

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            GameObject resourceDisplay = Instantiate(ResourceDisplayPrefab, gameObject.transform, false);
            ResourceDisplays[type] = resourceDisplay.transform.Find("Text").GetComponent<Text>();
            resourceDisplay.transform.Find("Image").GetComponent<Image>().sprite = ResourceSprites.GetSpriteForResourceType(type);
        }
    }

    private void Update () {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            ResourceDisplays[type].text = ": " + Storage.Resources.GetResourceAmount(type);
        }
	}
}

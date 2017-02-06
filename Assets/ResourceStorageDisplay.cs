using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceStorageDisplay : MonoBehaviour {
    private ResourceStorage Storage;
    private Text DisplayLabel;

    void Awake()
    {
        Storage = GetComponentInParent<ResourceStorage>();
        DisplayLabel = GetComponent<Text>();
    }
	
	void Update () {
        String label = "";
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            label += type + ": " + Storage.GetResourceAmount(type) + " ";
        }
        
        DisplayLabel.text = label.TrimEnd(' ');
	}
}

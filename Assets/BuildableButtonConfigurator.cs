using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableButtonConfigurator : MonoBehaviour {
    [HideInInspector]
    public GameObject BuildablePrefab;
    [HideInInspector]
    public PrestigeController CurrentPrestigeRef;

	// Use this for initialization
	void Start () {
        transform.FindChild("Icon").GetComponent<Image>().sprite = BuildablePrefab.GetComponent<SpriteRenderer>().sprite;
        transform.FindChild("Hotkey").GetComponent<Text>().text = BuildablePrefab.GetComponent<HotkeyBuildable>().Hotkey;
	}
	
	// Update is called once per frame
	void Update () {
        PlacementCostComponent costHolder = BuildablePrefab.GetComponent<PlacementCostComponent>();
        if (costHolder != null)
        {
            GetComponent<Button>().interactable = CurrentPrestigeRef.CurrentPrestige.GetComponent<ResourceStorage>().Resources.IsAtLeast(costHolder.Cost);
        }
	}
}

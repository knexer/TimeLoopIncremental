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
    private void Start () {
        transform.Find("Icon").GetComponent<Image>().sprite = BuildablePrefab.GetComponent<SpriteRenderer>().sprite;
        transform.Find("Hotkey").GetComponent<Text>().text = BuildablePrefab.GetComponent<HotkeyBuildable>().Hotkey;
	}
	
	// Update is called once per frame
    private void Update () {
        PlacementCostComponent costHolder = BuildablePrefab.GetComponent<PlacementCostComponent>();
        if (costHolder != null)
        {
            GetComponent<Button>().interactable = CurrentPrestigeRef.CurrentPrestige.GetComponent<ResourceStorage>().Resources.IsAtLeast(costHolder.Cost);
        }
	}
}

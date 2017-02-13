using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableButtonConfigurator : MonoBehaviour {
    public GameObject BuildablePrefab;

	// Use this for initialization
	void Start () {
        transform.FindChild("Icon").GetComponent<Image>().sprite = BuildablePrefab.GetComponent<SpriteRenderer>().sprite;
        transform.FindChild("Hotkey").GetComponent<Text>().text = BuildablePrefab.GetComponent<HotkeyBuildable>().Hotkey;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

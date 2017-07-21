using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildablesButtonMaker : MonoBehaviour {
    [SerializeField] private GameObject BuildablesButtonPrefab;

	// Use this for initialization
    private void Start () {
        PrestigeController prestigeController = FindObjectOfType<PrestigeController>();

		foreach (GameObject buildablePrefab in prestigeController.CurrentPrestige.GetComponent<BuildablePrefabs>().Buildables)
        {
            GameObject buildablesButton = Instantiate(BuildablesButtonPrefab, gameObject.transform, false);
            BuildableButtonConfigurator configurator = buildablesButton.GetComponent<BuildableButtonConfigurator>();
            configurator.BuildablePrefab = buildablePrefab;
            configurator.CurrentPrestigeRef = prestigeController;
        }
	}
}

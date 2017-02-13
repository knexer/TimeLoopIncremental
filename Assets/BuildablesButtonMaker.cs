using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildablesButtonMaker : MonoBehaviour {
    public GameObject BuildablesButtonPrefab;

	// Use this for initialization
	void Start () {
        ResourceStorage[] resourceStorages = FindObjectsOfType<ResourceStorage>();
        if (resourceStorages.Length > 1)
        {
            throw new InvalidOperationException("More than one ResourceStorage exists in the scene!");
        }

		foreach (GameObject buildablePrefab in FindObjectOfType<BuildablePrefabs>().Buildables)
        {
            GameObject buildablesButton = Instantiate(BuildablesButtonPrefab, gameObject.transform, false);
            BuildableButtonConfigurator configurator = buildablesButton.GetComponent<BuildableButtonConfigurator>();
            configurator.BuildablePrefab = buildablePrefab;
            configurator.PlayerResources = resourceStorages[0];
        }
	}
}

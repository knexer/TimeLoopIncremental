using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildablesButtonMaker : MonoBehaviour {
    public GameObject BuildablesButtonPrefab;

	// Use this for initialization
	void Start () {
		foreach (GameObject buildablePrefab in FindObjectOfType<BuildablePrefabs>().Buildables)
        {
            GameObject buildablesButton = Instantiate(BuildablesButtonPrefab, gameObject.transform, false);
            buildablesButton.GetComponent<BuildableButtonConfigurator>().BuildablePrefab = buildablePrefab;
        }
	}
}

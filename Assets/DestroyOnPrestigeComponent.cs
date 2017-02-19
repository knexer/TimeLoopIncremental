using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPrestigeComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInParent<PrestigeController>().OnPrestige += Destroy;
	}

    void Destroy(GameObject currentPrestige)
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        GetComponentInParent<PrestigeController>().OnPrestige -= Destroy;
    }
}

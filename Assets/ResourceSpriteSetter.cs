using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpriteSetter : MonoBehaviour {
    public ResourceSprites ResourceSprites;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = ResourceSprites.GetSpriteForResourceType(GetComponent<Resource>().ResourceType);
	}
}

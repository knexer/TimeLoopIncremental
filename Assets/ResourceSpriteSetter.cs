using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpriteSetter : MonoBehaviour {
    [SerializeField] private ResourceSprites ResourceSprites;

	// Use this for initialization
    private void Start () {
        GetComponent<SpriteRenderer>().sprite = ResourceSprites.GetSpriteForResourceType(GetComponent<Resource>().ResourceType);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpriteSetter : MonoBehaviour {

    public Sprite CoalSprite;
    public Sprite IronSprite;
    public Sprite SteelSprite;

	// Use this for initialization
	void Start () {
		switch(GetComponent<Resource>().ResourceType)
        {
            case ResourceType.COAL:
                GetComponent<SpriteRenderer>().sprite = CoalSprite;
                break;
            case ResourceType.IRON:
                GetComponent<SpriteRenderer>().sprite = IronSprite;
                break;
            case ResourceType.STEEL:
                GetComponent<SpriteRenderer>().sprite = SteelSprite;
                break;
            default:
                throw new ArgumentException(GetComponent<Resource>().ResourceType + " is not a recognized resource type.");
        }
	}
}

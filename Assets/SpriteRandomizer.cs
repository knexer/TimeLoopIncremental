using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour {
    public Sprite[] Sprites;

	// Use this for initialization
	void Awake () {
        GetComponent<SpriteRenderer>().sprite = Sprites[Mathf.FloorToInt(Random.value * Sprites.Length)];
	}
}

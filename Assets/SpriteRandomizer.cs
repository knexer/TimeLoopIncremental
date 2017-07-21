using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour {
    [SerializeField] private Sprite[] Sprites;

	// Use this for initialization
    private void Awake () {
        GetComponent<SpriteRenderer>().sprite = Sprites[Mathf.FloorToInt(Random.value * Sprites.Length)];
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.size = GetComponent<SpriteRenderer>().sprite.bounds.size;
        }
	}
}

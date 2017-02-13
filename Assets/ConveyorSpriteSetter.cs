using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Conveyor))]
public class ConveyorSpriteSetter : MonoBehaviour
{
    public Sprite VerticalSprite;
    public Sprite HorizontalSprite;

    // Use this for initialization
    void Start()
    {
        switch (GetComponent<Conveyor>().Orientation)
        {
            case Direction.DOWN:
            case Direction.UP:
                GetComponent<SpriteRenderer>().sprite = VerticalSprite;
                break;
            case Direction.LEFT:
            case Direction.RIGHT:
                GetComponent<SpriteRenderer>().sprite = HorizontalSprite;
                break;
            default:
                throw new ArgumentException(GetComponent<Conveyor>().Orientation + " is not a recognized Direction.");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

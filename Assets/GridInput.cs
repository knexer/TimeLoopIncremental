using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInput : MonoBehaviour {
    public ResourceType? ProvidedResource = null;
    public int X;
    public int Y;
    public Grid Grid;
    public float ProvidedPerSecond;

	// Use this for initialization
	void Start () {
        transform.position = Grid.gridToWorldSpace(X, Y);
	}
	
	// Update is called once per frame
	void Update () {
		// TODO Create resource(s) and insert them into the grid at the correct location.
	}
}

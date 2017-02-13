using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMachine : MonoBehaviour {
    public bool IsDestroyable = false;

    public void TryDestroy()
    {
        if (IsDestroyable)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        GetComponent<GridPositionComponent>().RegisterAsMachine();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

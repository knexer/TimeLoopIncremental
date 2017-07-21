using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMachine : MonoBehaviour {
    [SerializeField] private bool IsDestroyable = false;

    public void TryDestroy()
    {
        if (IsDestroyable)
        {
            GetComponent<GridPositionComponent>().Grid.SetGridObjectAt(GetComponent<GridPositionComponent>().Position, null);
            Destroy(gameObject);
        }
    }

	// Use this for initialization
    private void Start () {
        GetComponent<GridPositionComponent>().RegisterAsMachine();
	}
}

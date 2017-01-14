using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    //Unity configurables
    public int Width = 1;
    public int Height = 1;
    public GameObject CellPrefab;

    private GameObject[,] GridCells;

	// Use this for initialization
	void Start () {
        GridCells = new GameObject[Width, Height];
        for (int w = 0; w < Width; w++)
        {
            for (int h = 0; h < Height; h++)
            {
                GridCells[w, h] = InstantiateCell(w, h);
            }
        }
	}

    private GameObject InstantiateCell(int w, int h)
    {
        GameObject cell = Instantiate(CellPrefab, transform, false);

        cell.name += " (" + w + ", " + h + ")";
        cell.transform.position = gridToWorldSpace(w, h);

        return cell;
    }

    private Vector2 gridToWorldSpace(int w, int h)
    {
        Vector2 cellSizeLocalSpace = CellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector2 gridSizeLocalSpace = new Vector2(cellSizeLocalSpace.x, cellSizeLocalSpace.y);
        gridSizeLocalSpace.Scale(new Vector2(Width, Height));
        Vector2 topLeft = gridSizeLocalSpace / -2;
        Vector2 cellOffsetLocalSpace = new Vector2(cellSizeLocalSpace.x, cellSizeLocalSpace.y);
        cellOffsetLocalSpace.Scale(new Vector2(w, h));
        cellOffsetLocalSpace += topLeft;
        cellOffsetLocalSpace += cellSizeLocalSpace / 2;

        return transform.TransformVector(cellOffsetLocalSpace) + transform.position;
    }
}

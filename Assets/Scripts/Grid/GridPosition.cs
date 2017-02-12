using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class GridPosition
{
    public int X;
    public int Y;

    public GridPosition(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Creates a GridPosition corresponding to the closest integral grid location to gridSpaceVector.
    /// </summary>
    /// <param name="gridSpaceVector">A Vector2 in grid space.</param>
    public GridPosition(Vector2 gridSpaceVector)
    {
        this.X = Mathf.RoundToInt(gridSpaceVector.x);
        this.Y = Mathf.RoundToInt(gridSpaceVector.y);
    }

    public GridPosition InDirection(Direction dir)
    {
        Vector2 offset = dir.ToUnitVector();

        return new GridPosition(X + Mathf.RoundToInt(offset.x), Mathf.RoundToInt(Y + offset.y));
    }

    public Vector2 ToVector()
    {
        return new Vector2(X, Y);
    }

    public override bool Equals(object obj)
    {
        GridPosition other = obj as GridPosition;
        if (other == null)
        {
            return false;
        }

        return X.Equals(other.X) && Y.Equals(other.Y);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() + 31 * Y.GetHashCode();
    }

    public override string ToString()
    {
        return "{" + X + ", " + Y + "}";
    }
}

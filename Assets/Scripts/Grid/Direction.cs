using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum Direction
{
    UP = 0,
    RIGHT = 1,
    DOWN = 2,
    LEFT = 3
}
public static class DirectionExtensions
{
    public static Vector2 ToUnitVector(this Direction direction)
    {
        int x = 0;
        int y = 0;
        switch (direction)
        {
            case Direction.UP:
                y = 1;
                break;
            case Direction.RIGHT:
                x = 1;
                break;
            case Direction.DOWN:
                y = -1;
                break;
            case Direction.LEFT:
                x = -1;
                break;
        }

        return new Vector2(x, y);
    }

    public static Direction Clockwise(this Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.LEFT;
            case Direction.LEFT:
                return Direction.UP;
            default:
                throw new ArgumentException("OMGWTFBBQ");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DeletePrestigeAction : IPrestigeAction
{

    public Resources ResourcesThreshold
    {
        get; private set;
    }
    public Sprite Sprite
    {
        get; private set;
    }

    private GridPosition DeletePosition;

    public DeletePrestigeAction(Resources playerStorage, GridPosition deletePosition)
    {
        ResourcesThreshold = playerStorage;
        DeletePosition = deletePosition;
        // TODO Sprite for delete
    }

    public bool ApplyChangeToPrestige(GameObject prestige)
    {
        GridPositionComponent currentMachine = prestige.GetComponentInChildren<Grid>().GetGridObjectAt(DeletePosition);
        if (currentMachine != null)
        {
            currentMachine.GetComponent<GridMachine>().TryDestroy();
            return true;
        }

        return false;
    }
}

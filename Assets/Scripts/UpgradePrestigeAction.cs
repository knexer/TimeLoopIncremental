using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UpgradePrestigeAction : IPrestigeAction
{
    public Resources ResourcesThreshold
    {
        get; private set;
    }

    private GridPosition Position;

    public UpgradePrestigeAction(Resources resourcesThreshold, GridPosition position)
    {
        ResourcesThreshold = resourcesThreshold;
        Position = position;
    }

    public bool ApplyChangeToPrestige(GameObject prestige)
    {
        GridPositionComponent currentMachine = prestige.GetComponentInChildren<Grid>().GetGridObjectAt(Position);
        if (currentMachine != null)
        {
            Upgradeable upgrade = currentMachine.GetComponent<Upgradeable>();
            if (upgrade != null)
            {
                return upgrade.TryDoUpgrade(prestige.GetComponent<ResourceStorage>());
            }
        }

        return false;
    }
}

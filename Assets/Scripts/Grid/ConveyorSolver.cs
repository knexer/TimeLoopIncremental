using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorSolver : MonoBehaviour {
    private HashSet<Conveyor> Conveyors;

    private void Awake()
    {
        Conveyors = new HashSet<Conveyor>();
    }
    
    private void Update() {
        // Find the set of conveyors that wish to drop off their item
        List<Conveyor> blockedConveyors = Conveyors.Where((c) => c.ReadyToOffer).ToList<Conveyor>();

        // For each destination location, find the conveyor that will place an item in that location if it becomes available.
        Dictionary<GridPosition, Conveyor> winnerConveyors = new Dictionary<GridPosition, Conveyor>();
        foreach (IGrouping<GridPosition, Conveyor> group in blockedConveyors.GroupBy<Conveyor, GridPosition>((c) => c.ExitLocation))
        {
            // TODO impl round robin tie-breaking
            winnerConveyors[group.Key] = group.First();
        }

        // Find the set of conveyors that are pointing to something that can accept an item.
        Queue<Conveyor> openConveyors = new Queue<Conveyor>(blockedConveyors.Where((c) => c.CanDeliverItem()).ToList());

        while (openConveyors.Count > 0)
        {
            Conveyor openConveyor = openConveyors.Dequeue();
            if (openConveyor.DeliverItem())
            {
                if (winnerConveyors.ContainsKey(openConveyor.GetComponent<GridPositionComponent>().Position))
                {
                    openConveyors.Enqueue(winnerConveyors[openConveyor.GetComponent<GridPositionComponent>().Position]);
                }
            }
        }
	}

    public void RegisterConveyor(Conveyor toAdd)
    {
        Conveyors.Add(toAdd);
    }

    public void DeregisterConveyor(Conveyor toRemove)
    {
        Conveyors.Remove(toRemove);
    }
}

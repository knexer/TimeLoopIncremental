﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorSolver : MonoBehaviour {
    private HashSet<Conveyor> Conveyors;

    void Awake()
    {
        Conveyors = new HashSet<Conveyor>();
    }

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        // Find the set of conveyors that wish to drop off their item
        List<Conveyor> blockedConveyors = Conveyors.Where((c) => c.ReadyToOffer).ToList<Conveyor>();

        // For each destination location, find the conveyor that will place an item in that location if it becomes available.
        Dictionary<GridPosition, Conveyor> winnerConveyors = new Dictionary<GridPosition, Conveyor>();
        foreach (IGrouping<GridPosition, Conveyor> group in blockedConveyors.GroupBy<Conveyor, GridPosition>((c) => c.GetComponent<GridPositionComponent>().Position))
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
                Conveyor nextConveyor = winnerConveyors[openConveyor.GetComponent<GridPositionComponent>().Position];
                if (nextConveyor != null)
                {
                    openConveyors.Enqueue(nextConveyor);
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO implement stuff
public class ResourceSink : MonoBehaviour {
    public bool CanAcceptItem;

    public Func<Resource, bool> DeliverItem;

    public bool OfferItem(Resource item)
    {
        if (CanAcceptItem)
        {
            CanAcceptItem = DeliverItem(item);
            return true;
        }

        return false;
    }

	// Use this for initialization
	void Start () {
        CanAcceptItem = true;
	}
}

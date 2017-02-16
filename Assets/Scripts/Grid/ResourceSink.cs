using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO implement stuff
public class ResourceSink : MonoBehaviour {
    public Predicate<ResourceType> CanAcceptItem;
    public Func<Resource, bool> DeliverItem { set; private get; }

    public bool OfferItem(Resource item)
    {
        if (CanAcceptItem(item.ResourceType))
        {
            DeliverItem(item);
            return true;
        }

        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSprites : MonoBehaviour
{
    public Sprite CoalSprite;
    public Sprite IronSprite;
    public Sprite SteelSprite;

    // Use this for initialization
    public Sprite GetSpriteForResourceType(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.COAL:
                return CoalSprite;
            case ResourceType.IRON:
                return IronSprite;
            case ResourceType.STEEL:
                return SteelSprite;
            default:
                throw new ArgumentException(GetComponent<Resource>().ResourceType + " is not a recognized resource type.");
        }
    }
}

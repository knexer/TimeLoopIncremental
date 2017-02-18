using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour {
    public Resources Resources;

    private void Awake()
    {
        Resources = new Resources();
    }
}

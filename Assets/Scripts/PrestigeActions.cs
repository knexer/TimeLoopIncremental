using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeActions : MonoBehaviour {
    public List<IPrestigeAction> Actions;

    private void Start()
    {
        if (Actions == null)
        {
            Actions = new List<IPrestigeAction>();
        }
    }
}

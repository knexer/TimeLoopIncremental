using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO this needs to be an interface with impls for building stuff, destroying, and upgrading.
public class HotkeyBuildable : MonoBehaviour {
    public string Hotkey;
    public bool IsDestroy = false;
    public bool IsUpgrade = false;
}

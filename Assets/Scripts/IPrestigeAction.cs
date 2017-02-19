using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IPrestigeAction
{
    Resources Cost { get; }
    bool ApplyChangeToPrestige(GameObject prestige);
}

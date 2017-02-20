using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IPrestigeAction
{
    Resources ResourcesThreshold { get; }
    bool ApplyChangeToPrestige(GameObject prestige);
}

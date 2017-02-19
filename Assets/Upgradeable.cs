using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradeable : MonoBehaviour {
    [SerializeField]
    private ResourceAmount BaseCost;
    public ResourceAmount UpgradeCost
    {
        get
        {
            return new ResourceAmount() { Type = BaseCost.Type, Amount = BaseCost.Amount * UpgradeLevel };
        }
    }
    
    public int UpgradeLevel
    {
        get; private set;
    }

    public bool TryDoUpgrade(ResourceStorage paymentInstrument)
    {
        if (paymentInstrument.Resources.RemoveResource(UpgradeCost.Type, UpgradeCost.Amount))
        {
            UpgradeLevel++;
            return true;
        }

        return false;
    }

    private void Reset(GameObject gameObject)
    {
        UpgradeLevel = 1;
    }

    private void Start()
    {
        UpgradeLevel = 1;
        GetComponentInParent<PrestigeController>().OnPrestige += Reset;
    }

    private void OnDestroy()
    {
        GetComponentInParent<PrestigeController>().OnPrestige -= Reset;
    }
}

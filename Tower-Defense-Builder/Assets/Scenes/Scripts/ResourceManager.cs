using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance
    {
        get;
        private set; //这里这样做的目的是，为了让别的类只能得到这里的属性但是不能修改这里的属性；
    }

    public event EventHandler OnResourceAmountChanged;


    [SerializeField] private List<ResourceAmount> startingResourceAmountist;
    private Dictionary<ResouceTypeSO, int> resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResouceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>("ResourceTypeListSO");

        foreach (ResouceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        foreach (ResourceAmount resourceAmount in startingResourceAmountist)
        {
            AddResource(resourceAmount.resourceType,resourceAmount.amount);
        }
    }
    public void AddResource(ResouceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this,EventArgs.Empty); //send signals to the ResourceUI by event 
    }

    public int GetResourceAmount(ResouceTypeSO resouceTypeSo)
    {
        return resourceAmountDictionary[resouceTypeSo];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountarray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountarray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
                //Can afford
            }
            else
            {
                //Can't afford
                return false;
            }
        }
        return true;
    }
    
    public void SpendResources(ResourceAmount[] resourceAmountarray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountarray)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }

}

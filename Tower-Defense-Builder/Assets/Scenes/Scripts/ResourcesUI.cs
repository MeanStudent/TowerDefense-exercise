using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResouceTypeSO, Transform> resourceTypeTransformDictionary;
    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>("ResourceTypeListSO");
        resourceTypeTransformDictionary = new Dictionary<ResouceTypeSO, Transform>();
        Transform resourceTemplate = transform.Find("ResourceTemplate");
        
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach (ResouceTypeSO resourceType in  resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            float offsetAmount = -150f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount*index,0);
            
            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.resourceSprite;
            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index ++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged; // Add Event Listener
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResouceTypeSO resouceTypeSo in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resouceTypeSo];
            
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resouceTypeSo);
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}

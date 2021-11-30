using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNearbyOverly : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        int nearbyResourceAmount =
            ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / this.resourceGeneratorData.maxResourceAmount * 100F);
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }
    
    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);

        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite =
            resourceGeneratorData.resourceType.resourceSprite;
    }

    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

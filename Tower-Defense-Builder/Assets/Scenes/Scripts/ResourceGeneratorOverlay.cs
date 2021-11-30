using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField]
    private ResourceGenerator resourceGenerator;


    private Transform barTransform;

    private void Start()
    {
        barTransform = transform.transform.Find("bar");
        ResourceGeneratorData resourceGeneratorData =  resourceGenerator.GetResourceGeneratorData();
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.resourceSprite;
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
        //TextMeshProUGUI 是针对canvas上使用的，world坐标下的时候使用TextMeshPro
        closeAnimationifNotGeneratingResources();
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(1-resourceGenerator.GetTimerNormalized(), 1, 1);
    }

    private void closeAnimationifNotGeneratingResources()
    {
        if (resourceGenerator.GetAmountGeneratedPerSecond() == 0)
        {
            transform.parent.GetChild(0).GetComponent<Animator>().enabled = false;
        }
    }
    
}

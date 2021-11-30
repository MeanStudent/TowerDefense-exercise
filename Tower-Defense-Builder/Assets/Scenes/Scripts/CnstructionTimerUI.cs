using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CnstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstructionScript buildingConstruction;
    private Image constructionProgressImage;

    private void Awake()
    {
        constructionProgressImage = transform.Find("Mask").Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionNormalized();
    }
}

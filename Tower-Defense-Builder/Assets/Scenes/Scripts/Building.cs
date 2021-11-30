using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    
    private Transform buildingDemolishButton;
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void Awake()
    {
        buildingDemolishButton = transform.Find("BuildingDemolishBtn");
        
        HideBuildingDemolishbtn();

    }



    private void HealthSystem_OnDied(object snder, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishbtn();
    }

    private void OnMouseExit()
    {
        HideBuildingDemolishbtn();
    }

    private void ShowBuildingDemolishbtn()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }
    private void HideBuildingDemolishbtn()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }
}

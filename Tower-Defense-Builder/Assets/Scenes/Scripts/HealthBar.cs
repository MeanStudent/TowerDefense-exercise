using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Transform barTransform;

    private void Start()
    {
        healthSystem = transform.parent.GetComponent<HealthSystem>();
        barTransform = transform.Find("bar").transform;
        healthSystem.OnDamaged += HealthSystem_Ondamaged;
        UpdateHealthBarVisible();
    }

    private void HealthSystem_Ondamaged(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsHealthFull())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    
}

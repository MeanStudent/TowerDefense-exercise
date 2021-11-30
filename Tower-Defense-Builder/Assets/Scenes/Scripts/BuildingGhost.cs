using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{

    private GameObject spriteGameObject;
    private ResourceNearbyOverly resourceNearbyOverly;
    private void Awake()
    {
        spriteGameObject = transform.Find("Sprite").gameObject;
        resourceNearbyOverly = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverly>();
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveTypeChange += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveTypeChangeEventArgs e)
    {
        //控制building ghost上面的效率显示面板是否出现
        if (e.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverly.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            if (e.activeBuildingType.haveResourceGeneratedData)
            {
                resourceNearbyOverly.Show(e.activeBuildingType.ResourceGeneratorData);//如果该建筑不生成资源，就不显示资源收集效率面板；
            }
            else
            {
                resourceNearbyOverly.Hide();
            }

        }
    }


    private void Update()
    {
        transform.position = Tools.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}

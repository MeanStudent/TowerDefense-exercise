using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField]
    private Transform buttonTemplate;

    [SerializeField] 
    private Sprite arrowSprite;

    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;

    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;

    private Transform arrowBtn;

    private BuildingTypeListSO buildingtypelist;
    private void Awake()
    {
        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        buttonTemplate.gameObject.SetActive(false);
        buildingtypelist = Resources.Load<BuildingTypeListSO>("BuildingTypeList");

        int index = 0;
        
        arrowBtn = Instantiate(buttonTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        float offsetAmount = +120f;
        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        arrowBtn.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
        arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        index += 1;
        MouseEnterExitEvents mouseEnterExitEvents = arrowBtn.transform.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Hide();
        };
        

        foreach (BuildingTypeSO buildingType in buildingtypelist.list)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;
            Vector3 initialPosition = buttonTemplate.transform.position;
            Vector3 newPosition = new Vector3();
            newPosition.x = initialPosition.x + index * offsetAmount;
            newPosition.y = 63;
            GameObject  button = Instantiate(buttonTemplate.gameObject, transform.position,Quaternion.identity);
            button.transform.SetParent(transform);
            button.SetActive(true);
            button.transform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            
            button.transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            button.transform.position = newPosition;

            mouseEnterExitEvents = button.transform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
            {
                ToolTipUI.Instance.Show(buildingType.nameString+"\n"+buildingType.GetConStructionResourceCostString());
            };
            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
            {
                ToolTipUI.Instance.Hide();
            };
            

            buttonTransformDictionary[buildingType] = button.transform;
            index += 1;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveTypeChange += GameManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuildingType();
    }

    private void GameManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveTypeChangeEventArgs e)
    {
        UpdateActiveBuildingType();
    }

    private void UpdateActiveBuildingType()
    {
        arrowBtn.Find("Selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingTyoe();
        if (activeBuildingType == null)
        {
            arrowBtn.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            buttonTransformDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
        }

    }
    
}

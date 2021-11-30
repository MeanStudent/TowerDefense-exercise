using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{

    public event EventHandler<OnActiveTypeChangeEventArgs> OnActiveTypeChange;

    public class OnActiveTypeChangeEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField] private Building hqbuilding;
    
    public static BuildingManager Instance { get; private set; }
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;
    private Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>("BuildingTypeList");
    }

    private void Start() {
        mainCamera  = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
            {
                if (CanSpawnBuilding(activeBuildingType, Tools.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                        //Instantiate(activeBuildingType.perfabs, Tools.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstructionScript.Create(Tools.GetMouseWorldPosition(), activeBuildingType);
                    }
                    else
                    {
                        ToolTipUI.Instance.Show("Cannot afford" + activeBuildingType.GetConStructionResourceCostString(),
                            new ToolTipUI.TooltipTimer{timer = 2f});
                    }
                }
                else
                {
                    ToolTipUI.Instance.Show(errorMessage, new ToolTipUI.TooltipTimer{timer = 2f});
                }
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingTypeSo)
    {
        activeBuildingType = buildingTypeSo;
        OnActiveTypeChange?.Invoke(this, new OnActiveTypeChangeEventArgs{activeBuildingType = activeBuildingType});
    }
 

    public BuildingTypeSO GetActiveBuildingTyoe()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position,out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.perfabs.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;

        if (!isAreaClear)
        {
            errorMessage = "Area is not clear.";
            return false;
        }


            collider2DArray = Physics2D.OverlapCircleAll(position, activeBuildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            //Colliders inside the construction radius
            BuildingTypeHolder buildingtypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingtypeHolder != null)
            {
                // Has a buildingTypeHolder
                if (buildingtypeHolder.buildingType == buildingType)
                {
                    errorMessage = "Too close to another building of the same type!";
                    return false;
                }
            }
        }

        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        
        foreach (Collider2D collider2D in collider2DArray)
        {
            //Colliders inside the construction radius
            BuildingTypeHolder buildingtypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingtypeHolder != null)
            {
                // It's a building
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "Too far from anyother buildings";
        return false;
    }

    public Building GetHQBuilding()
    {
        return hqbuilding;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingConstructionScript : MonoBehaviour
{
    public static BuildingConstructionScript Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstructionScript buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstructionScript>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }
    
    
    
    private BuildingTypeSO buildingType;
    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;
    

    private void Awake()
   {
       boxCollider2D = GetComponent<BoxCollider2D>();
       spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
       buildingTypeHolder = GetComponent<BuildingTypeHolder>();
       constructionMaterial = spriteRenderer.material;
   }

   private void Update()
   {
       constructionTimer -= Time.deltaTime;
       constructionMaterial.SetFloat("_Progress",GetConstructionNormalized());
       if (constructionTimer <= 0)
       {
           Debug.Log("Ding");
           Instantiate(buildingType.perfabs, transform.position, quaternion.identity);
           Destroy(gameObject);
       }
           
   }

   
   private void SetBuildingType(BuildingTypeSO buildingType)
   {
       this.buildingType = buildingType;
       constructionTimerMax = buildingType.constructionTimerMax;
       
       
       constructionTimer = constructionTimerMax;

       spriteRenderer.sprite = buildingType.sprite;

       boxCollider2D.offset = buildingType.perfabs.GetComponent<BoxCollider2D>().offset;
       boxCollider2D.size = buildingType.perfabs.GetComponent<BoxCollider2D>().size;
       
       
       buildingTypeHolder.buildingType = buildingType;
       
   }

   public float GetConstructionNormalized()
   {
       return 1 - constructionTimer / constructionTimerMax;
   }
}

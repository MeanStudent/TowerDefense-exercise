using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/Building")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public int maxHealth;
    public Transform perfabs;
    public bool haveResourceGeneratedData;
    public ResourceGeneratorData ResourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;
    public float constructionTimerMax;

    public string GetConStructionResourceCostString()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
        {
            str += "<color=#"+resourceAmount.resourceType.colorHex + ">" +
                   resourceAmount.resourceType.nameString + ":" + resourceAmount.amount +
                   "</color> ";
        }
        return str;
    }
}

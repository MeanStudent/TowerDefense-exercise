using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResouceTypeSO : ScriptableObject
{
   public string nameString;
   public string nameShort;
   public Sprite resourceSprite;
   public string colorHex;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
   public event EventHandler OnDamaged;
   public event EventHandler OnDied;
   
   private int currentHealthAmount;
   private int PFhealthAmountMax;
   private int healAmountMax;

   private void Awake()
   {
      if (GetComponent<BuildingTypeHolder>() != null)
      {
         PFhealthAmountMax = GetComponent<BuildingTypeHolder>().buildingType.maxHealth;
         healAmountMax = PFhealthAmountMax;
         currentHealthAmount = healAmountMax;
      }
      else
      {
         healAmountMax = 30;
         currentHealthAmount = healAmountMax;
      }
      
   }

   public void Damage(int damageAmount)
   {
      currentHealthAmount -= damageAmount;
      currentHealthAmount = Mathf.Clamp(currentHealthAmount, 0, healAmountMax);
      OnDamaged?.Invoke(this,EventArgs.Empty);
      if (IsDead())
      {
         OnDied?.Invoke(this,EventArgs.Empty);
      }
   }

   public bool IsDead()
   {
      return currentHealthAmount == 0;
   }

   public int GetHealthAmount()
   {
      return currentHealthAmount;
   }

   public float GetHealthAmountNormalized()
   {
      return (float)currentHealthAmount / healAmountMax;
   }

   public bool IsHealthFull()
   {
      return currentHealthAmount == healAmountMax;
   }

}

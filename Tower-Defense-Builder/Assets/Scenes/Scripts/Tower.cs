using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float shootTimer;
    [SerializeField]
    private float shootTimerMax;
    private Enemy targetEnemy;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Vector3 projectilespawnposition;

    private void Awake()
    {
        projectilespawnposition = transform.Find("projectilespawnposition").position;
    }

    private void HandleTargting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void Update()
    {
        HandleTargting();
        HandleShooting();
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy != null)
            {
                ArrowProjectile.Create(projectilespawnposition, targetEnemy);
            }
        }
        
        
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 20f;
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider2D in colliderArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Is a enemy!
                if (targetEnemy== null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}

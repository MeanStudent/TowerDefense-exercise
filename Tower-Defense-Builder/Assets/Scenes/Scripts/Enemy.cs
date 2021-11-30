using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }
    private Transform targetTransform;
    private Rigidbody2D rigidbody2D;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private HealthSystem healthSystem;
    private void Start()
    { 
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        targetTransform =  BuildingManager.Instance.GetHQBuilding().transform;
        lookForTargetTimer = UnityEngine.Random.Range(0f, lookForTargetTimerMax);
        LookForTargets();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_Ondied;
    }

    private void HealthSystem_Ondied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargting();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            //collided with a building!
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 movDir = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;
            rigidbody2D.velocity = movDir * moveSpeed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
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
    private void LookForTargets()
    {
        float targetMaxRadius = 10f;
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider2D in colliderArray)
        {
            Building building = collider2D.GetComponent<Building>();
            collider2D.GetComponent<Building>();
            if (building != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }
}

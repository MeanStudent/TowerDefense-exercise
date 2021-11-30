using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;
    private enum State
    {
        WaittingToSpawnNextWave,
        SpaeningWave,
    }

    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private Transform nextWaveSpawnPositionTransform;
    private State state;
    private int waveNumber;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;
    private void Start()
    {
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0,spawnPositionTransformList.Count)].position;
        nextWaveSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;
        spawnPosition = new Vector3(50, 0);
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaittingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0)
                {
                    SpawnWave();
                    nextWaveSpawnPositionTransform.position = spawnPosition;
                }
                break;
            case State.SpaeningWave:
                if (nextEnemySpawnTimer >= 0f)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        Enemy.Create(spawnPosition + Tools.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount -= 1;
                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.WaittingToSpawnNextWave;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0,spawnPositionTransformList.Count)].position;
                        }
                    }
                }
                break;
        }
    }
    private void SpawnWave()
    {
        nextWaveSpawnTimer = 10f;
        remainingEnemySpawnAmount = 5 + 3*waveNumber;
        state = State.SpaeningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this ,EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}

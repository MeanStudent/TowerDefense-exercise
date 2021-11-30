using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    private Camera mainCamera;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    [SerializeField] private float enemySpawnPositionIndicatorOffset;
    [SerializeField]
    private EnemyWaveManager enemyWaveManager;

    private RectTransform enemyWaveSpawnPositionIndicator;
    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPositionIndicator = transform.Find("image").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        SetWaveNumberText("Wave "+enemyWaveManager.GetWaveNumber());
        enemyWaveManager.OnWaveNumberChanged += enemyWaveManager_OnWaveNumberChanged;
    }

    private void enemyWaveManager_OnWaveNumberChanged(object sender, EventArgs e)
    {
        SetWaveNumberText("Wave "+enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicatior();
    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer < -0f)
        {
            SetMessageText("");
        }
        else
        {
            int newWaveSpawnTimerint = (int) nextWaveSpawnTimer;
            SetMessageText("Next Wave in :"+newWaveSpawnTimerint+" seconds.");
        }
    }

    private void HandleEnemyWaveSpawnPositionIndicatior()
    {
        

        Vector3  dirToNextSpawnPosition = enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position.normalized;
        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition*enemySpawnPositionIndicatorOffset;
        enemyWaveSpawnPositionIndicator.eulerAngles =
            new Vector3(0, 0, Tools.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition =
            Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition>mainCamera.orthographicSize*1.5f);
    }

    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}

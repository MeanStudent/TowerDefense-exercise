using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;
using Cinemachine;


public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float orthgraphicSize;
    private float targetorthgraphicSize;

    private void Start()
    {
        orthgraphicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetorthgraphicSize = orthgraphicSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(x, y).normalized;
        float moveSpeed = 5f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        float zoomAmount = 2f;
        targetorthgraphicSize += Input.mouseScrollDelta.y*zoomAmount;

        float minOrthographicSize = 10;
        float maxOrthographicSize = 30;
        targetorthgraphicSize = Mathf.Clamp(targetorthgraphicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthgraphicSize = Mathf.Lerp(orthgraphicSize, targetorthgraphicSize,Time.deltaTime*zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthgraphicSize;
    }
}

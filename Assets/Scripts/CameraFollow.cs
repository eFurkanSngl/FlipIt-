using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _mainCamFollow : MonoBehaviour
{
    [SerializeField] private float zoomMultiplier = 1.2f;
    [SerializeField] private Camera _mainCam;

    private void Start()
    {
        _mainCam = GetComponent<Camera>();
    }
    private void FitCamToGrid(int gridWidth, int gridHeight)
    {
        if (_mainCam == null) return;

        float aspect = (float)Screen.width / Screen.height;

        float _mainCamSizeByWidth = (gridWidth / aspect) / 2f;
        float _mainCamSizeByHeight = gridHeight / 2f;

        _mainCam.orthographicSize= Mathf.Max(_mainCamSizeByWidth, _mainCamSizeByHeight) * zoomMultiplier;

        float centerX = gridWidth / 2.4f;
        float centerY = gridHeight / 2f;

        _mainCam.transform.position = new Vector3(centerX, centerY, -10f);
    }

    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }

    private void RegisterEvents()
    {
        GridManager.GridManagerEvents += FitCamToGrid;
    }

    private void UnRegisterEvents()
    {
        GridManager.GridManagerEvents -= FitCamToGrid;
    }
}

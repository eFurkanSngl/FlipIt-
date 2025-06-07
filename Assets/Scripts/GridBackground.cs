using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBackground : MonoBehaviour
{
    private SpriteRenderer _sr;
    [SerializeField] private float _padding = 0.5f;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetX;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void AdjustGridbackground(int gridX , int gridY)
    {
        if(_sr != null)
        {
            transform.position = new Vector3(gridX / _offsetX - 0.5f, gridY / _offsetY - 0.5f, 1f);
            _sr.size = new Vector3(gridX + _padding, gridY + _padding, 1f);
        }
    }


    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }

    private void UnRegisterEvents()
    {
        GridManager.GridManagerEvents -= AdjustGridbackground;
    }

    private void RegisterEvents()
    {
        GridManager.GridManagerEvents += AdjustGridbackground;
    }
}

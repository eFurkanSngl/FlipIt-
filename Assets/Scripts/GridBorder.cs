using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBorder : MonoBehaviour
{
    [Header("Grid Border Settings")]
    [SerializeField] private GameObject _borderTop;
    [SerializeField] private GameObject _borderRight;
    [SerializeField] private GameObject _borderLeft;
    [SerializeField] private GameObject _borderBottom;

    private List<GameObject> _borderObjects = new List<GameObject>();


    private void CreateGridBorder(int gridX, int gridY)
    {
        foreach (var obj in _borderObjects)
        {
            Destroy(obj);
        }
        _borderObjects.Clear();  // Grid varsa onu yok ediyoruz.

        // Baþlangýç noktasý
        float startX = transform.position.x;  // objelerin baþlangýç x ve y sini alýyoruz ( 0 )
        float startY = transform.position.y;

        float padding = 0.2f;

        // Top & Bottom
        for (int i = 0; i < gridX; i++)
        {
            Instantiate(_borderTop, new Vector3(startX + i, startY + gridY - 1 + 0.5f + padding, 0), Quaternion.identity, transform);
            Instantiate(_borderBottom, new Vector3(startX + i, startY - 0.5f - padding, 0), Quaternion.identity, transform);
        }

        for (int i = 0; i < gridY; i++)
        {
            // Left Border
            var left = Instantiate(_borderLeft, new Vector3(startX - 0.5f - padding, startY + i, 0), Quaternion.identity, transform);
            left.transform.Rotate(0, 0, 90);
            // Right Border
            var right = Instantiate(_borderRight, new Vector3(startX + gridX - 0.5f + padding, startY + i, 0), Quaternion.identity, transform);
            right.transform.Rotate(0, 0, 90);
        }

        Debug.Log("baþlangýç poz" + startX);
        Debug.Log("baþlangýç poz" + startY);

    }


    private void OnEnable() => RegisterEvents();
    private void OnDisable() => UnRegisterEvents();

    private void RegisterEvents()
    {
        GridManager.GridManagerEvents += CreateGridBorder;
    }
    private void UnRegisterEvents()
    {
        GridManager.GridManagerEvents -= CreateGridBorder;

    }
}      




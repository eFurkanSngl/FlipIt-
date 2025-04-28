using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilebackground : MonoBehaviour
{
    [SerializeField] private GameObject _background;

    private void AdjustTileBackground(int gridX, int gridY)
    {
        for(int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                Vector3 pos = new Vector3(transform.position.x + i , transform.position.y + j , transform.position.z);
                GameObject obj = Instantiate(_background,pos,Quaternion.identity);
                obj.transform.position = pos;
                obj.transform.SetParent(transform);
            }
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
    private void RegisterEvents()
    {
        GridManager.GridManagerEvents += AdjustTileBackground;
    }

    private void UnRegisterEvents()
    {
        GridManager.GridManagerEvents -= AdjustTileBackground;
    }
}

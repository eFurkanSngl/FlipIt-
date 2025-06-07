using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : UIBTN
{
    [SerializeField] private GameObject _nextLevelPanel;
    protected override void OnClick()
    {
        LoadNextlevel();
        Debug.Log("New Level");
    }

    private void LoadNextlevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}

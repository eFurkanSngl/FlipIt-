using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private int _currentLevelIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
        }
    }


    public void LoadNextLevel()
    {
       _currentLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (_currentLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_currentLevelIndex);
        }
        else
        {
            Debug.Log("No more levels!");
            // Ýstersen burada ana menüye atabilirsin
        }
    }

    
}

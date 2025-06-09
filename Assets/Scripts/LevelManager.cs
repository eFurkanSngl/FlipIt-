using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] _levelSelectButton;
    [SerializeField] private CanvasGroup[] _buttonCanvas;
    public static LevelManager Instance;
    [SerializeField] private GameObject _welcomePanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        int unLockedLevel = PlayerPrefs.GetInt("unLockedLevel", 1);

        for (int i = 0; i < _levelSelectButton.Length; i++)
        {
            _levelSelectButton[i].interactable= i < unLockedLevel;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("HasOpenedBefore"))
        {
            PlayerPrefs.SetInt("HasOpenedBefore", 1);
            PlayerPrefs.Save();
            StartCoroutine(WelcomePanelRotuine());
        }
        else
        {
            _welcomePanel.SetActive(false);
        }
    }

    private IEnumerator WelcomePanelRotuine()
    {
        _welcomePanel.gameObject.SetActive(true);
        Debug.Log("Started");

        yield return new WaitForSeconds(4f);
        _welcomePanel.gameObject.SetActive(false);
        Debug.Log("Stopped");
    }
    public void OpenLevel(int levelId)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelId);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level-" + levelId);
    }

    public void UnLockNextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        int unLockedLevel = PlayerPrefs.GetInt("unLockedLevel", 1);
        if (currentLevel >= unLockedLevel)
        {
            PlayerPrefs.SetInt("unLockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
            Debug.Log("UnlockedLevel" + PlayerPrefs.GetInt("UnlockedLevel", 1));
        }
    }

}

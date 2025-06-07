using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _panelButton;

    private void OnClick()
    {
        StartCoroutine(PanelRoutine());
    }

    private void OpenSettingsMenu()
    {
        _settingsPanel.SetActive(true);
    }

    private void ClosedSettingsMenu()
    {
        _settingsPanel.SetActive(false);
    }
    private IEnumerator PanelRoutine()
    {
        if (_panelButton)
        {
            OpenSettingsMenu();
        }
        yield return new WaitForSeconds(0.2f);

        if (_exitButton)
        {
            ClosedSettingsMenu();
        }
    }

    private void RegisterEvents()
    {
        _exitButton.onClick.AddListener(ClosedSettingsMenu);
        _panelButton.onClick.AddListener(OpenSettingsMenu);
    }

    private void UnRegisterEvents()
    {
        _exitButton.onClick.RemoveListener(ClosedSettingsMenu);
        _panelButton.onClick.RemoveListener(OpenSettingsMenu);
    }

    private void OnEnable() => RegisterEvents();
    private void OnDisable() => UnRegisterEvents();
    
        
    



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private void OnClick()
    {
        StartCoroutine(ClosedPanelRoutine());
    }

    private IEnumerator ClosedPanelRoutine()
    {
        ClosedPanel();
        yield return new WaitForSeconds(0.2f);
    }
    private void ClosedPanel()
    {
        PausePanelEvents.PausePanelEvent?.Invoke();
    }

    private void RegisterEvents()
    {
        ExitButtonEvents.ExitButtonEvent += ClosedPanel;
        _button.onClick.AddListener(OnClick);
    }

    private void UnRegisterEvents()
    {
        ExitButtonEvents.ExitButtonEvent -= ClosedPanel;
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }
}

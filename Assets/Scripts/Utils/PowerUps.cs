using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUps : EventListenerMono
{
    [SerializeField] protected Button _button;
    [SerializeField] protected TextMeshProUGUI _currentLiveText;
    public int _currentLives;
    public int _maxLives;

    protected WaitForSeconds _wait = new WaitForSeconds(0.2f);

    private void Start()
    {
        _currentLives = _maxLives;
        UpdateText();

    }
    protected abstract void OnClick();

    protected override void RegisterEvents()
    {
        _button.onClick.AddListener(OnClick);
        GameManager.GameManagerEvents += Restart;
    }

    protected override void UnRegisterEvents()
    {
        _button.onClick.RemoveListener(OnClick); 
        GameManager.GameManagerEvents -= Restart;
    }

    protected virtual void UpdateText()
    {
        if (_currentLiveText != null)
        {
            _currentLiveText.text = _currentLives.ToString();
        }
    }

    protected virtual void Restart()
    {
        _currentLives = _maxLives;
        _button.interactable = true;
        UpdateText();
    }
}

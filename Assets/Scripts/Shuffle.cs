using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shuffle : UIBTN
{
    [SerializeField] private TextMeshProUGUI _currentLivesText;
    private int _currentLives;
    private int _maxLives = 2;
    private WaitForSeconds _wait = new WaitForSeconds(0.2f);
    protected override void OnClick()
    {
       if(_currentLives > 0)
        {
            StartCoroutine(ShuffleRoutine());
        }
    }

    private void Start()
    {
        _currentLives = _maxLives;
        UpdateText();
    }

    private IEnumerator ShuffleRoutine()
    {
        _currentLives--;
        UpdateText();

        GameEvents.ShuffleEvents?.Invoke();
        GameEvents.PowerUpEvents?.Invoke();

        if(_currentLives <= 0)
        {
            _button.interactable = false;
        }

        yield return _wait;
    }

    private void UpdateText()
    {
        if(_currentLivesText.text != null)
        {
            _currentLivesText.text = _currentLives.ToString();
        }
    }
}

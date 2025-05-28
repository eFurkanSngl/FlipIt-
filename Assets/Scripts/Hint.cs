using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : PowerUps
{
    protected override void OnClick()
    {
        StartCoroutine(ShowHintRoutine());
    }

    private IEnumerator ShowHintRoutine()
    {
        _currentLives--;
        UpdateText();
        PowerUpEvents.ShowHintEvents?.Invoke();
        if (_currentLives >= 0)
        {
            _button.interactable = false;
        }
        yield return _wait;
    }
}

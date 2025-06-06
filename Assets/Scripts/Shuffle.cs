using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shuffle : PowerUps
{
    protected override void OnClick()
    {
        StartCoroutine(ShuffleRoutine());
    }

    private IEnumerator ShuffleRoutine()
    {
        _currentLives--;
        UpdateText();

        PowerUpEvents.ShuffleEvents?.Invoke(); 
        PowerUpEvents.PowerUpEvent?.Invoke();

        if (_currentLives <= 0)
        {
            _button.interactable = false;
        }

        yield return _wait;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevalAllBtn : PowerUps
{
   
    protected override void OnClick()
    {
        StartCoroutine(RevallAllCards());
    }
    private IEnumerator RevallAllCards()
    {
        _currentLives--;
        UpdateText();

        PowerUpEvents.PowerUpEvent?.Invoke();
        yield return _wait;
        if (_currentLives >= 0)
        {
            _button.interactable = false;
            Debug.Log("is click");
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevalAllBtn : UIBTN
{
    [SerializeField] private TextMeshProUGUI _currentLiveText;
    private bool _isClick = false;
    private int _currentLive = 1;

    private void Start()
    {
        _currentLiveText.text = 1.ToString();
    }
    protected override  void OnClick()
    {
        StartCoroutine(RevallAllCards());
        Debug.Log("is clicks");
        if(_currentLive >= 0)
        {
            _button.interactable = false;
            Debug.Log(" not clicked");
        }
    }


    private IEnumerator RevallAllCards()
    {
        _isClick = false;
        _currentLive = 1;

        yield return new WaitForSeconds(0.2f);

        _isClick = true;
        _currentLive -= 1;
        _currentLiveText.text = 0.ToString();
        GameEvents.PowerUpEvents?.Invoke();
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Cards _firstCard;
    private Cards _secondCard;
    private List<Cards> matchedCards = new List<Cards>();

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
    }

    public void SelectCard(Cards card)
    {
        if(_firstCard == null)
        {
            _firstCard = card;
        }
        else if (_secondCard == null)
        {
            _secondCard = card;
            StartCoroutine(FindMatch());
        }
    }

    private void ResetSelect()
    {
        _firstCard = null;
        _secondCard = null; 
    }
    private IEnumerator FindMatch()
    {
        yield return new WaitForSeconds(0.2f);
        matchedCards.Clear();

        if(_firstCard.cardId == _secondCard.cardId)
        {
            matchedCards.Add(_firstCard);
            matchedCards.Add(_secondCard);

            foreach(var destroyCard in matchedCards)
            {
                Destroy(destroyCard.gameObject);
                Debug.Log("Destroy card");
            }
        }
        else
        {
            GameEvents.GameEvent?.Invoke();

            Debug.Log("Not Matched");
        }
        ResetSelect();
    }


    private void DestroyAnim(Cards card)
    {

    }
}

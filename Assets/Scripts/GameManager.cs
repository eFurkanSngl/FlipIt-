using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager Settings")]
    private Cards _firstCard;
    private Cards _secondCard;
    private List<Cards> matchedCards = new List<Cards>();

    [Header("Game Over Panel Settings")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;
    [SerializeField] private RectTransform _gameOverPanelTransform;
    [SerializeField] private float  _middlePos;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private CanvasGroup _canvasGroup;

    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        int amount = 10;
        int lives = 1;

        yield return new WaitForSeconds(0.2f);
        matchedCards.Clear();

        if(_firstCard.cardId == _secondCard.cardId)
        {
            matchedCards.Add(_firstCard);
            matchedCards.Add(_secondCard);

            foreach(var destroyCard in matchedCards)
            {
                DestroyAnim(destroyCard.gameObject);
                ScoreEvents.ScoreEvent?.Invoke(amount);
                Debug.Log("Destroy card");
            }
        }
        else
        {
            GameEvents.GameEvent?.Invoke();
            ScoreEvents.CurrentLives?.Invoke(lives);
            Debug.Log("Not Matched");
        }
        ResetSelect();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
       _gameOverPanel.SetActive(true);
        _gameOverScoreText.text = "Score: " + ScoreManager.Instance.Score;
        GameOverPanelIntro();
    }

    private void GameOverPanelIntro()
    {
        _canvasGroup.DOFade(1, _tweenDuration).SetUpdate(true);
        _gameOverPanelTransform.DOAnchorPosX(_middlePos, _tweenDuration).SetUpdate(true);
    }

    private void DestroyAnim(GameObject card)
    {
        Sequence destroySeq = DOTween.Sequence();  // Animasyonlarý Sýrayla çalýþtýrmak için.

        destroySeq.Append(card.transform.DOShakePosition(0.2f, 0.15f, 10, 90f, false, true));
        // 0.2f = Süre
        // 0.15f = Þiddet
        // 10f = titreme sayýsý
        // 90f randomness
        // false yumuþak geçiþ
        // true  zamanla azalan titreme

        destroySeq.Append(card.transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack));
        // Burada Objeyi küçültüyoruz.

        SpriteRenderer sr = card.GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            destroySeq.Join(sr.DOFade(0f, 0.4f));  // Animleri ayný anda çalýþtýr
        }

        destroySeq.OnComplete(() =>
        {
            Destroy(card);
        });
        // Anim bitince Destory edecek
    }

    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }

    private void RegisterEvents()
    {
        GameOverEvents.GameOverEvent += GameOver;
    }

    private void UnRegisterEvents()
    {
        GameOverEvents.GameOverEvent -= GameOver;
    }
}

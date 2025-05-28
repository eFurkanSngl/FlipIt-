using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

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

    [Header("Next Level Panel Settings")]
    [SerializeField] private GameObject _nextLevelPanel;
    [SerializeField] private TextMeshProUGUI _nextLevelScoreText;
    [SerializeField] private RectTransform _nextLevelPanelTransform;



    private GameObject[,] _cardList;
    private int matchedCardCount = 0;
    private int matchedIncrease = 2;
    public static GameManager Instance { get; private set; }
    public static event UnityAction GameManagerEvents;

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

    private void RestartButton()
    {
        RestartButtonEvents.ResetEvents?.Invoke();
        _gameOverPanel.SetActive(false);
        _nextLevelPanel.SetActive(false);
        ScoreManager.Instance.ResetScoreAndCurrentLives();
        Debug.Log("Restart Game");
        GameManager.GameManagerEvents?.Invoke();
        matchedCards.Clear();
        matchedCardCount = 0;
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
            matchedCardCount += 2;


            foreach (var destroyCard in matchedCards)
            {
                DestroyAnim(destroyCard.gameObject);
                ScoreEvents.ScoreEvent?.Invoke(amount);
                Debug.Log("Destroy card");
                Debug.Log("card count" + matchedCardCount);
            }

            if(matchedCardCount >= GridManager.Instance.TotalCardCount)
            {
                Debug.Log("Next Level Panel is Open");
                yield return new WaitForSeconds(0.4f);
                NextLevelPanel();
            } 
        }
        else
        {
            GameEvents.GameEvent?.Invoke();
            ScoreEvents.CurrentLives?.Invoke(lives);
            Debug.Log("Not Matched");
            NotMacthedAnim(_firstCard);
            NotMacthedAnim(_secondCard);
            yield return new WaitForSeconds(0.1f);
        }
        ResetSelect();
    }
    private void NextLevelPanel()
    {
        Debug.Log("Next Level");
        _nextLevelPanel.SetActive(true);
        _nextLevelScoreText.text = "Score: " + ScoreManager.Instance.Score;
        NextLevelPanelIntro();
    }

    private void NextLevelPanelIntro()
    {
        _canvasGroup.DOFade(1, _tweenDuration).SetUpdate(true);
        _nextLevelPanelTransform.DOAnchorPosX(_middlePos, _tweenDuration).SetUpdate(true);
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
    private void NotMacthedAnim(Cards cardOne)
    {
        Sequence notMatchedSeq = DOTween.Sequence();
        notMatchedSeq.Append(cardOne.transform.DOShakePosition(0.2f, 0.15f, 10, 90f, false, true));
        
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
        RestartButtonEvents.RestartEvents += RestartButton;
    }

    private void UnRegisterEvents()
    {
        GameOverEvents.GameOverEvent -= GameOver;
        RestartButtonEvents.RestartEvents -= RestartButton;
    }
}

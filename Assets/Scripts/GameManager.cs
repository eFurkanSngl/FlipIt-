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
    [SerializeField] private TextMeshProUGUI _gameOverPanelHighScoreText;

    [Header("Next Level Panel Settings")]
    [SerializeField] private GameObject _nextLevelPanel;
    [SerializeField] private TextMeshProUGUI _nextLevelScoreText;
    [SerializeField] private RectTransform _nextLevelPanelTransform;
    [SerializeField] private TextMeshProUGUI _nextLevelPanelHighScoreText;
    [SerializeField] private GameObject _pausePanel;


    [Header("Sound Effect")]
    [SerializeField] private AudioSource _findSound;
    [SerializeField] private AudioSource _succsessSound;
    [SerializeField] private AudioSource _wrongMatchSound;
    [SerializeField] private AudioSource _gameOverSound;
    [SerializeField] private AudioSource _mainMusic;




    private WaitForSeconds _waitTime = new WaitForSeconds(0.1f);
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
        ExitButtonEvents.ExitButtonEvent?.Invoke();
        _mainMusic.Play();
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
        yield return _waitTime;
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
                ConffettiParticle(destroyCard);
                _findSound.Play();
                Debug.Log("Destroy card");
                Debug.Log("card count" + matchedCardCount);
                GridManager.Instance._allCards.Remove(destroyCard);

            }

            if (matchedCardCount >= GridManager.Instance.TotalCardCount)
            {
                Debug.Log("Next Level Panel is Open");
                yield return _waitTime;
                MultiScore.Instance.StartBonusScoring(() =>
                {
                    NextLevelPanel();
                    LevelManager.Instance.UnLockNextLevel();

                });

            }
        }
        else
        {
            GameEvents.GameEvent?.Invoke();
            ScoreEvents.CurrentLives?.Invoke(lives);
            Debug.Log("Not Matched");
            NotMacthedAnim(_firstCard);
            NotMacthedAnim(_secondCard);
            yield return _waitTime;
            _wrongMatchSound.Play();
        }
        ResetSelect();
    }
    private void ConffettiParticle(Cards card)
    {
        GameObject effect = ParticlePool.Instance.GetParticlePool();

        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        if(ps != null)
        {
            ps.Play();
            StartCoroutine(RetunParticlePool(effect,2f));
        }
    }
    private IEnumerator RetunParticlePool(GameObject obj , float delay)
    {
        yield return new WaitForSeconds(delay);
        ParticlePool.Instance.ReturnParticlePool(obj);
    }
    private void NextLevelPanel()
    {
        Debug.Log("Next Level");
        _nextLevelPanel.SetActive(true);
        _nextLevelScoreText.text = "Score: " + ScoreManager.Instance.Score;
        _nextLevelPanelHighScoreText.text = "HighScore: " + ScoreManager.Instance.HighScore;
        NextLevelPanelIntro();
        _succsessSound.Play();
        _mainMusic.Stop();
        ScoreManager.Instance.SaveHighScore();

    }

    private void NextLevelPanelIntro()
    {
        _canvasGroup.DOFade(1, _tweenDuration).SetUpdate(true);
        _nextLevelPanelTransform.DOAnchorPosX(-210, _tweenDuration).SetUpdate(true);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
       _gameOverPanel.SetActive(true);
        _gameOverScoreText.text = "Score: " + ScoreManager.Instance.Score;
        _gameOverPanelHighScoreText.text = "HighScore: " + ScoreManager.Instance.HighScore;
        GameOverPanelIntro();
        _gameOverSound.Play();
        _mainMusic.Stop();
        ScoreManager.Instance.SaveHighScore();

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
        Sequence destroySeq = DOTween.Sequence();  // Animasyonlar� S�rayla �al��t�rmak i�in.

        destroySeq.Append(card.transform.DOShakePosition(0.2f, 0.15f, 10, 90f, false, true));
        // 0.2f = S�re
        // 0.15f = �iddet
        // 10f = titreme say�s�
        // 90f randomness
        // false yumu�ak ge�i�
        // true  zamanla azalan titreme

        destroySeq.Append(card.transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack));
        // Burada Objeyi k���lt�yoruz.

        SpriteRenderer sr = card.GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            destroySeq.Join(sr.DOFade(0f, 0.4f));  // Animleri ayn� anda �al��t�r
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

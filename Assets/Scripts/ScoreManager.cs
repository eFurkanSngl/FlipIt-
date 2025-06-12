using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _currentLivesText;
    [SerializeField] private int _currentLives;
    [SerializeField] private int _startLives;
    public TextMeshProUGUI ScoreText => _scoreText;
    public TextMeshProUGUI CurrentLivesText => _currentLivesText;

    private int _score = 0;

    public int CurrentLives
    {
        get
        {
            return _currentLives;
        }
        set
        {
            _currentLives = value;
        }
    }
    public int Score
    {
        get { return _score; }

        set { _score = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        ResetScoreAndCurrentLives();
    }
    public void ResetScoreAndCurrentLives()
    {
        _score = 0;
        _currentLives = _startLives;
        UpdateScore();
        UpdateCurrentLives();
    }
    private void UpdateScore()
    {
        if(_scoreText != null)
        {
            _scoreText.text = "Score: "+_score.ToString();
        }
    }

    private void IncreaseScore(int score)
    {
        _score += score;
        UpdateScore();
    }

    private void DecreaseCurrentLives(int lives)
    {
        _currentLives -= lives;

        if(_currentLives <= 0)
        {
            _currentLives = 0;
            GameOverEvents.GameOverEvent?.Invoke();
        }

        UpdateCurrentLives();
    }

    private void UpdateCurrentLives()
    {
        if(_currentLivesText != null)
        {
            _currentLivesText.text = "Lives: " + _currentLives.ToString();
        }
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
        ScoreEvents.ScoreEvent += IncreaseScore;
        ScoreEvents.CurrentLives += DecreaseCurrentLives;
    }

    private void UnRegisterEvents()
    {
        ScoreEvents.ScoreEvent -= IncreaseScore;
        ScoreEvents.CurrentLives -= DecreaseCurrentLives;
    }


}

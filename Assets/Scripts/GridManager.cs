using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _gridX;
    [SerializeField] private int _gridY;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private GameObject _coverPrefabs;
    [SerializeField] private int _cardCount = 6;
    [SerializeField] private LevelData _levelData;

    private List<GameObject> _cards = new List<GameObject>();

    private List<Cards> _cardList = new List<Cards>();
    private List<Vector3> _posList = new List<Vector3>();
    public List<Cards> _allCards = new List<Cards>();
    private List<Cards> _closedCards = new List<Cards>();

    public static event UnityAction<int, int> GridManagerEvents;
    public int TotalCardCount { get; private set; }
    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
        }
    }

    private void Start()
    {
        FillTheGrid();

    }

    private void ShowHint()
    {
        _closedCards.Clear();
        foreach(Cards card in _allCards)
        {
            Cover cover = card.GetComponentInChildren<Cover>();
            if(cover != null && !cover.IsOpen)
            {
                _closedCards.Add(card);
            }
        }

        List<int> randomID = new List<int>();
        foreach(var card in _allCards)
        {
            if (!randomID.Contains(card.cardId))
            {
                randomID.Add(card.cardId);
            }
        }

        int randomIndex = randomID[Random.Range(0,randomID.Count)];

        List<Cards> findMatchId = new List<Cards>();
        foreach(var card in _closedCards)
        {
            if (card.cardId == randomIndex)
                findMatchId.Add(card);
            if(findMatchId.Count == 2)
            {
                break;
            }
        }

        foreach (var Cards in findMatchId)
        {
            Cards.transform.DOShakePosition(0.5f, 0.2f, 10);
        }

    }

    /// <summary>
    /// GridManager'ýn Altýnda olan bütün Childlarýn transfomlarýna ulaþýp Compenenti Card Olanlarý alýrýz
    /// Listelere cardlarý ve transfomlarýný tutarýz.
    /// Shuffle ile pozisyonlarýný deðiþtiririz.
    /// En son görsel olarak deðiþir.
    /// </summary>
    private void ShuffleInGrid()
    {
        _posList.Clear();
        _cardList.Clear();

       //foreach( Transform child in transform)
       // {
       //     Cards card = child.GetComponent<Cards>();
       //     if (card != null)
       //     {
       //         _cardList.Add(card);
       //         _posList.Add(card.transform.position);
       //     }
       // }

        foreach(Cards card in _allCards)
        {
            if(card != null)
            {
                _cardList.Add(card);
                _posList.Add(card.transform.position);
            }
        }

        for(int i = 0; i < _posList.Count; i++)
        {
            int randIndex = Random.Range(i,_posList.Count);
            Vector3 temp = _posList[i];
            _posList[i] = _posList[randIndex];
            _posList[randIndex] = temp;
        }

        for(int i = 0; i< _cardList.Count;i++)
        {
            Vector3 targetPos = _posList[i];
            //_cardList[i].transform.position = _posList[i];
            _cardList[i].transform.DOMove(targetPos, 0.35f).SetEase(Ease.InOutBack);
        }
    }
    
    private void GenerateNewCard()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        FillTheGrid();
    }
    
    private void FillTheGrid()
    {
        PrepareCards();
        _allCards.Clear();
        int cardCount = 0;
        
        foreach(Vector2Int cellPos in _levelData.activeCells )
        //for (int i = 0; i < _gridX; i++)
        {
            //for (int j = 0; j < _gridY; j++)
            //{
                if (cardCount >= _cards.Count) return;

                Vector3 pos = new Vector3(transform.position.x + cellPos.x, transform.position.y + cellPos.y, transform.position.z);
                GameObject obj = Instantiate(_cards[cardCount], pos, Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                Cards card = obj.GetComponent<Cards>();
                _allCards.Add(card);

                GameObject cover = Instantiate(_coverPrefabs, pos, Quaternion.identity);
                cover.transform.parent = obj.transform;
                cover.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                SpriteRenderer sr = cover.GetComponent<SpriteRenderer>();
                sr.sortingOrder = 1;
            GridManagerEvents?.Invoke(_gridX, _gridY);

            cardCount++;
            //}
        }
    }

    private void PrepareCards()
    {
        _cards.Clear();
        int id = 0;
        foreach (GameObject card in _prefabs)
        {
            for (int i = 0; i < _cardCount; i++)
            {
                _cards.Add(card);
            }
            id++;
            Debug.Log("id" + id);
        }

        TotalCardCount = _cards.Count;
        Debug.Log("Card Count:" + TotalCardCount);

        ShuffleCards();
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            int randomIndex = Random.Range(0, _cards.Count);
            (_cards[i], _cards[randomIndex]) = (_cards[randomIndex], _cards[i]);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _gridX; i++)
        {
            for (int j = 0; j < _gridY; j++)
            {
                Gizmos.DrawWireCube(new Vector3(transform.position.x + i, transform.position.y + j, transform.position.z), new Vector3(1, 1, 1));
            }
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
        RestartButtonEvents.ResetEvents += GenerateNewCard;
        PowerUpEvents.ShuffleEvents += ShuffleInGrid;
        PowerUpEvents.ShowHintEvents += ShowHint;
    }

    private void UnRegisterEvents()
    {
        RestartButtonEvents.ResetEvents -= GenerateNewCard;
        PowerUpEvents.ShuffleEvents -= ShuffleInGrid;
        PowerUpEvents.ShowHintEvents -= ShowHint;
    }
}
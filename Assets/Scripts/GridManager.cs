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

    private List<GameObject> _cards = new List<GameObject>();

    private List<Cards> _cardList = new List<Cards>();
    private List<Vector3> _posList = new List<Vector3>();
    private List<Cards> _allCards = new List<Cards>();

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
            _cardList[i].transform.position = _posList[i];
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
        for (int i = 0; i < _gridX; i++)
        {
            for (int j = 0; j < _gridY; j++)
            {
                if (cardCount >= _cards.Count) return;

                Vector3 pos = new Vector3(transform.position.x + i, transform.position.y + j, transform.position.z);
                GameObject obj = Instantiate(_cards[cardCount], pos, Quaternion.identity);
                obj.transform.parent = transform;
                Cards card = obj.GetComponent<Cards>();
                _allCards.Add(card);

                GameObject cover = Instantiate(_coverPrefabs, pos, Quaternion.identity);
                cover.transform.parent = obj.transform;
                SpriteRenderer sr = cover.GetComponent<SpriteRenderer>();
                sr.sortingOrder = 1;

                cardCount++;
                GridManagerEvents?.Invoke(_gridX, _gridY);

            }
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
        }

        TotalCardCount = _cards.Count;

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
        GameEvents.ShuffleEvents += ShuffleInGrid;
    }

    private void UnRegisterEvents()
    {
        RestartButtonEvents.ResetEvents -= GenerateNewCard;
        GameEvents.ShuffleEvents -= ShuffleInGrid;
    }
}
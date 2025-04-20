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
    public static event UnityAction<int, int> GridManagerEvents;
    private void Start()
    {
        FillTheGrid();
        GridManagerEvents?.Invoke(_gridX,_gridY);
    }

    private void FillTheGrid()
    {
        PrepareCards();

        int cardCount = 0;
        for(int i =  0; i < _gridX; i++)
        {
            for(int j=0; j < _gridY; j++)
            {
                if(cardCount >= _cards.Count) return;
                
                Vector3 pos = new Vector3(transform.position.x + i , transform.position.y +j , transform.position.z);
                GameObject obj = Instantiate(_cards[cardCount],pos,Quaternion.identity);
                obj.transform.parent = transform;


                GameObject cover = Instantiate(_coverPrefabs,pos,Quaternion.identity);
                cover.transform.parent = obj.transform;
                SpriteRenderer sr = cover.GetComponent<SpriteRenderer>();
                sr.sortingOrder = 1;

                cardCount++;
            }
        }
    }

    private void PrepareCards()
    {
        _cards.Clear();
        int id = 0;
        foreach(GameObject card in _prefabs)
        {
            for(int i = 0; i < _cardCount; i++)
            {
                _cards.Add(card);
            }
            id++;
        }

        ShuffleCards();
    }

    private void ShuffleCards()
    {
        for(int i = 0; i < _cards.Count; i++)
        {
            int randomIndex = Random.Range(0, _cards.Count);
            (_cards[i], _cards[randomIndex]) = (_cards[randomIndex], _cards[i]);
        }
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < _gridX; i++)
        {
            for(int j = 0; j < _gridY; j++)
            {
                Gizmos.DrawWireCube(new Vector3(transform.position.x + i , transform.position.y +j, transform.position.z), new Vector3(1,1,1));
            }
        }
    }
}

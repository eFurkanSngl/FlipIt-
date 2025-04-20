using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    [SerializeField] private int _card›d;
    public int cardId => _card›d;

    private void Initilaize(int id)
    {
        _card›d = id;
    }
    
}

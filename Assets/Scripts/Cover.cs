using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cover : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool _isOpen = false;
    private bool _isInteractable = true;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

       StartCoroutine(StartOpenRoutine());
    }
    private void OnMouseDown()
    {
        if(!_isOpen & _isInteractable)
        {
            StartCoroutine(OpenCoverRoutine());
        }
    }

    private IEnumerator OpenCoverRoutine()
    {
        _isOpen = true;
        _isInteractable = false;

        yield return new WaitForSeconds(0.2f);

        sr.color = Color.clear;
        _isInteractable = true;
        GameManager.Instance.SelectCard(transform.parent.GetComponent<Cards>());
    }

    private IEnumerator CloseCoverRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        sr.color = Color.white;
        _isOpen = false;
        _isInteractable = true;
    }
    private IEnumerator StartOpenRoutine()
    {
        sr.color = Color.clear;
        yield return new WaitForSeconds(1.5f);
        sr.color = Color.white;
    }

    private void ResetCover()
    {
        StartCoroutine(CloseCoverRoutine());
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
        GameEvents.GameEvent += ResetCover;
    }

    private void UnRegisterEvents()
    {
        GameEvents.GameEvent -= ResetCover;
    }
}

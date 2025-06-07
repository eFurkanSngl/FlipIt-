using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cover : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

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
        AnimateOpen();

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
        yield return new WaitForSeconds(1.3f);
        sr.color = Color.white;
    }
    private void AnimateOpen()
    {
        Sequence openSeq = DOTween.Sequence();

        // Ýlk minik geri çekilme efekti
        openSeq.Append(transform.DOScale(new Vector3(1f, 0.1f, 1f), 0.15f).SetEase(Ease.InQuad));

        // Sonra yay gibi geniþleme (kart açýlýyor)
        openSeq.Append(transform.DOScale(new Vector3(1f, 1.1f, 1f), 0.25f).SetEase(Ease.OutElastic));

        // Son olarak normal boyuta geri dön
        openSeq.Append(transform.DOScale(1.115f, 0.2f).SetEase(Ease.OutSine));
    }
 
    private void AllOpenCards()
    {
        StartCoroutine(StartOpenRoutine());
    }

    private void ResetCover()
    {
        if (_isOpen)
        {
            StartCoroutine(CloseCoverRoutine());

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
        GameEvents.GameEvent += ResetCover;
        PowerUpEvents.PowerUpEvent += AllOpenCards;
    }

    private void UnRegisterEvents()
    {
        GameEvents.GameEvent -= ResetCover;
        PowerUpEvents.PowerUpEvent -= AllOpenCards;
    }
}

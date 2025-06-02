using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : UIBTN
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private RectTransform _pauseButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _pausePanelTransform;
    [SerializeField] private float _middlePos,_posX;
    [SerializeField] private float _tweenDuration;

    protected override void OnClick()
   {
        _pausePanel.SetActive(true);
        Debug.Log("Panel is opened");
        PausePanelIntro();
   }

    private void PausePanelIntro()
    {
        _canvasGroup.DOFade(1, _tweenDuration).SetUpdate(true);
        _pausePanelTransform.DOAnchorPosX(_middlePos, _tweenDuration).SetUpdate(true);
        _pauseButton.DOAnchorPosX(_posX, _tweenDuration).SetUpdate(true);
    }

    private void PausePanelOutro()
    {
        _canvasGroup.DOFade(0, _tweenDuration).SetUpdate(true);
        _pausePanelTransform.DOAnchorPosX(_posX, _tweenDuration).SetUpdate(true);
        _pausePanel.gameObject.SetActive(false);
        _pauseButton.DOAnchorPosX(-20,0.5f).SetUpdate(true);

    }

    protected override void RegisterEvents()
    {
        base.RegisterEvents();
        PausePanelEvents.PausePanelEvent += PausePanelOutro;
    }

    protected override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        PausePanelEvents.PausePanelEvent -= PausePanelOutro;
    }

    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }
}

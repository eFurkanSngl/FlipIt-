using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScore : MonoBehaviour
{
    [SerializeField] private float _bonusInterval = 1f;
    [SerializeField] private int _bonusMoveScore = 5;
    public static MultiScore Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    private IEnumerator AnimateBonusScoreRoutine(System.Action onComplete)
    {
        int totalBonus = 0;
        int displayedScore = ScoreManager.Instance.Score;
        int lives = ScoreManager.Instance.CurrentLives;

        for(int i = 1; i <= lives; i++)  // Kalan Hak kadar d�necek  �rnk 3 hak kald� 1-2-3
        {
            int bonus = _bonusMoveScore * i;  // bonus skor ile hak �ap�lacak �rnk 5 * 1
            totalBonus += bonus; 

            int start = displayedScore;   // Ba�lang�� Skoru 
            int end = displayedScore + bonus; // bonusu skora eklme 
            displayedScore = end;  // en son bonuslu skor ( yeni i�lemde bu skorun �st�ne ekleyecek )

            DOTween.To(() => start, x =>
            {
                ScoreManager.Instance.ScoreText.text = $"Score: {x}";
            }, end, 0.4f);

            ScoreManager.Instance.Score += bonus; // g�venlik i�in skoru bu kadar att�r�yoruz.

            ScoreManager.Instance.CurrentLives--;  // her d�ng�de Lives� azalt�yoruz
            ScoreManager.Instance.CurrentLivesText.text =$"Lives: {ScoreManager.Instance.CurrentLives}";
            yield return new WaitForSeconds(_bonusInterval); 
        }
        ScoreManager.Instance.CurrentLivesText.text = $"Lives: {ScoreManager.Instance.CurrentLives}";
        ScoreManager.Instance.ScoreText.text =$"Score: {ScoreManager.Instance.Score}";
        onComplete?.Invoke();
    }
    public void StartBonusScoring(System.Action onComplete)
    {
        StartCoroutine(AnimateBonusScoreRoutine(onComplete));
    }

}

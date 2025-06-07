using UnityEngine;

public class SimpleUIParallax : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float resetPositionX = -1920f; // Ekran d��� kald���nda
    [SerializeField] private float startPositionX = 1920f;  // Yeniden ba�a al�naca�� pozisyon

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        if (rectTransform.anchoredPosition.x <= resetPositionX)
        {
            rectTransform.anchoredPosition = new Vector2(startPositionX, rectTransform.anchoredPosition.y);
        }
    }
}

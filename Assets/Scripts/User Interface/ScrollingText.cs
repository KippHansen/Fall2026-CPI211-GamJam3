using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public float moveSpeed = 100f; 
    //public float resetPositionY = 608f; 
    public float startPositionY = -507;
    public float endPositionY = 771;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = startPositionY;
        rectTransform.anchoredPosition = pos;
    }

    void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        pos.y += moveSpeed * Time.deltaTime;

        if (pos.y > endPositionY)
        {
        endPositionY = 771;
        moveSpeed = 0f;
        }
        /*if (pos.y > resetPositionY)
        {
            pos.y = startPositionY;
        }
        */
        rectTransform.anchoredPosition = pos;
    }
}
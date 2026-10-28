using UnityEngine;
using TMPro;

public class MoveText : MonoBehaviour
{
    private RectTransform rectTransform;

    void Launch(){
        rectTransform = GetComponent<RectTransform>();
    }

    void Update(){
        rectTransform.anchoredPosition += new Vector2(50f * Time.deltaTime, 0);
    }
}

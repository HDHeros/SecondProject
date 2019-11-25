using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObjects : MonoBehaviour
{
    public float speed = 5f, checkPos = 0f;
    private RectTransform rectTransform;

    void Start(){
        rectTransform = GetComponent<RectTransform> ();

    }

    void Update(){
        if(rectTransform.offsetMin.y != checkPos){
            rectTransform.offsetMin += new Vector2(rectTransform.offsetMin.x, speed);
            rectTransform.offsetMax += new Vector2(rectTransform.offsetMax.x, speed);
        }
    }
}

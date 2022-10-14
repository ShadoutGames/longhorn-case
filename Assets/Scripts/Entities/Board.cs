using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject paint;

    private Color firstColor;

    private void Awake() 
    {
        firstColor = paint.GetComponent<SpriteRenderer>().color;

        paint.GetComponent<SpriteRenderer>().color = new Color(firstColor.r, firstColor.g, firstColor.b, 0);
    }

    public void Paint()
    {
        LeanTween.alpha(paint, 1, 1);
    }
}

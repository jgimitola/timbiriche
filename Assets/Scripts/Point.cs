using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject Inner;
    private bool isClicked = false;

    private void OnMouseDown()
    {
        SpriteRenderer srCircle = Inner.GetComponent<SpriteRenderer>();

        if (isClicked)
        {
            isClicked = false;
            srCircle.color = Color.white;
        }
        else
        {
            isClicked = true;
            srCircle.color = Color.cyan;
        }
    }
}

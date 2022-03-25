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

        if (GameManager.Instance.GetState == GameManager.GameState.player1)
        {
            srCircle.color = Color.red;
        }
        else
        {
            srCircle.color = Color.cyan;
        }
        isClicked = true;
        BoardManager.Instance.SetPoint(this);
    }
}
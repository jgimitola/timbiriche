using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject Inner;
    public (int, int) pos;

    private Vector2 _position;
    public bool isClicked = false;

    public Vector2 Pos => _position;

    public void Init(Vector2 position)
    {
        _position = position;
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            SpriteRenderer srCircle = Inner.GetComponent<SpriteRenderer>();

            if (GameManager.Instance.GetState == GameManager.GameState.player1)
            {
                srCircle.color = Color.cyan;
            }
            else
            {
                srCircle.color = Color.red;
            }
            BoardManager.Instance.SetLine(this);
        }
    }

    public void changeColor()
    {
        SpriteRenderer srCircle = Inner.GetComponent<SpriteRenderer>();
        srCircle.color = Color.green;
    }

}

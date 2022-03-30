using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject Inner;

    private Vector2 _position;
    private bool _isClicked = false;

    public Vector2 Pos => _position;

    public void Init(Vector2 position)
    {
        this._position = position;
    }

    private void OnMouseDown()
    {
        if (!_isClicked)
        {
            _isClicked = true;
            SpriteRenderer srCircle = Inner.GetComponent<SpriteRenderer>();

            if (GameManager.Instance.GetState == GameManager.GameState.player1)
            {
                srCircle.color = Color.red;
            }
            else
            {
                srCircle.color = Color.cyan;
            }
            BoardManager.Instance.SetLine(this);
        }
    }

}

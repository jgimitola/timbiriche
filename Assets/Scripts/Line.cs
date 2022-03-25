using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject Inner;
    private Vector2 _position;

    public Vector2 Pos => _position;

    public void Init(Vector2 position)
    {
        this._position = position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

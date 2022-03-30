using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 4;
    public Point PointPrefab;
    public Line LinePrefab;

    private void GenerateBoard()
    {
        int c = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var p = new Vector2(i, j);
                Instantiate(PointPrefab, p, Quaternion.identity);
                Debug.Log(++c);
            }
        }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width - 1; j++)
            {
                var p = new Vector2(i, j + 0.5f);
                Instantiate(LinePrefab, p, Quaternion.identity);
            }
        }

        for (int i = 0; i < height - 1; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var p = new Vector2(i + 0.5f, j);
                Line l = Instantiate(LinePrefab, p, Quaternion.identity);
                l.transform.Rotate(0f, 0, 90f, Space.World);
            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);
        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
    }

    public void SetLine(Line l)
    {
        GameManager.Instance.SwitchPlayer();
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

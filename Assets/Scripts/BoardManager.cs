using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public int width = 4;
    public int height = 4;
    public Point PointPrefab;
    public Line LinePrefab;

    private void GenerateBoard()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var p = new Vector2(i, j);
                Instantiate(PointPrefab, p, Quaternion.identity);
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

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);
        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
    }

    public void SetPoint(Point p)
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

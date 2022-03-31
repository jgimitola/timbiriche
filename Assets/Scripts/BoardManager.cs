using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public Point PointPrefab;
    public Line LinePrefab;

    public Canvas canvas;
    public Button button;
    public Dropdown dropdown;

    private void GenerateBoard(int width, int height)
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
        button.onClick.AddListener(() =>
        {
            canvas.gameObject.SetActive(false);
            int index = dropdown.value;
            int n = int.Parse(dropdown.options[index].text);
            GenerateBoard(n, n);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public class Cell
    {
        public (int, int) pos;

        private Line top;
        private Line right;
        private Line bottom;
        private Line left;

        public Cell((int, int) pos, Line top, Line right, Line bottom, Line left)
        {
            this.pos = pos;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
        }

        public bool HasLine(Line l)
        {
            return l == top || l == right || l == bottom || l == left;
        }

        public bool markCell()
        {
            return top.isClicked && right.isClicked && bottom.isClicked && left.isClicked;
        }
    }

    public static BoardManager Instance;
    public Point PointPrefab;
    public Line LinePrefab;
    public Player1Icon p1Icon;
    public Player2Icon p2Icon;

    public Canvas canvas;
    public Text titleText;
    public Button button;
    public Text legendText;
    public Dropdown dropdown;
    public Text resultText;

    IDictionary<(int, int), Line> verticalLinesDictionary;
    IDictionary<(int, int), Line> horizontalLinesDictionary;
    Cell[,] cells;
    int availableCells, pointsP1, pointsP2;

    private void toggleMenu(bool toggle)
    {
        titleText.gameObject.SetActive(toggle);
        legendText.gameObject.SetActive(toggle);
        dropdown.gameObject.SetActive(toggle);
        button.gameObject.SetActive(toggle);
    }

    private void toggleResultMessage(bool toggle)
    {
        resultText.gameObject.SetActive(toggle);
    }

    private List<Cell> PosibleLineCells(Line l)
    {
        List<Cell> containingCells = new List<Cell>();

        int n = cells.GetLength(0);

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (cells[i, j].HasLine(l))
                {
                    containingCells.Add(cells[i, j]);
                }
            }
        }
        return containingCells;
    }

    private void PaintPlayerCell((int, int) pos)
    {
        int i = pos.Item1;
        int j = pos.Item2;
        if (GameManager.Instance.GetState == GameManager.GameState.player1)
        {
            Instantiate(p1Icon, new Vector2(i + 0.5f, j + 0.5f), Quaternion.identity);
        }
        else
        {
            Instantiate(p2Icon, new Vector2(i + 0.5f, j + 0.5f), Quaternion.identity);
        }
    }

    private void GenerateBoard(int width, int height)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var p = new Vector2(i, j);
                Instantiate(PointPrefab, p, Quaternion.identity);
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                var p = new Vector2(i, j + 0.5f);
                Line l = Instantiate(LinePrefab, p, Quaternion.identity);
                l.pos = (i, j);
                verticalLinesDictionary.Add((i, j), l);
            }
        }

        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var p = new Vector2(i + 0.5f, j);
                Line l = Instantiate(LinePrefab, p, Quaternion.identity);
                l.transform.Rotate(0f, 0, 90f, Space.World);
                l.pos = (i, j);
                horizontalLinesDictionary.Add((i, j), l);
            }
        }

        cells = new Cell[width - 1, height - 1];
        availableCells = (width - 1) * (height - 1);
        pointsP1 = 0;
        pointsP2 = 0;

        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                Line top = horizontalLinesDictionary[(j, i + 1)];
                Line right = verticalLinesDictionary[(j + 1, i)];
                Line bottom = horizontalLinesDictionary[(j, i)];
                Line left = verticalLinesDictionary[(j, i)];

                Cell currentCell = new Cell((j, i), top, right, bottom, left);
                cells[i, j] = currentCell;
            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);
        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
    }

    public void SetLine(Line l)
    {
        bool validMovement = false;
        List<Cell> containingCells = PosibleLineCells(l);
        foreach (Cell c in containingCells)
        {
            if (c.markCell())
            {
                if (GameManager.Instance.GetState == GameManager.GameState.player1)
                {
                    pointsP1++;
                }
                else
                {
                    pointsP2++;

                }
                availableCells--;

                validMovement = true;
                PaintPlayerCell(c.pos);
            }
        }

        Debug.Log("Available: " + availableCells);
        Debug.Log("P1: " + pointsP1);
        Debug.Log("P2: " + pointsP2);

        if (availableCells + pointsP1 < pointsP2)
        {
            Debug.Log("Gana Jugador 2");
            resultText.text = "Gana Jugador 2";
            toggleResultMessage(true);
        }
        else if (availableCells + pointsP2 < pointsP1)
        {
            Debug.Log("Gana Jugador 1");
            resultText.text = "Gana Jugador 1";
            toggleResultMessage(true);
        }

        if (!validMovement)
        {
            GameManager.Instance.SwitchPlayer();
        }
    }


    private void Awake()
    {
        Instance = this;
        verticalLinesDictionary = new Dictionary<(int, int), Line>();
        horizontalLinesDictionary = new Dictionary<(int, int), Line>();
    }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            toggleMenu(false);

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

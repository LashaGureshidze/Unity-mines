using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameController : MonoBehaviour {
    public static int LEFT_CLICK = 1;
    public static int RIGHT_CLICK = 2;
    public static int BOMB_COUNT = 10;

    public CellController[] cells;

    private bool gameEnd = false;

    // Use this for initialization
    void Start () {
        initializeCells();
        initializeBombs();
    }

    private void initializeBombs()
    {
        int counter = 0;
        System.Random r = new System.Random();
        while (counter < BOMB_COUNT)
        {
            int next = r.Next(0, 99);
            if (!cells[next].isBomb)
            {
                cells[next].isBomb = true;
                counter++;
            }
        }
    }

    private void initializeCells()
    {
        cells = new CellController[100];
        for (int i = 0; i < 100; i++)
        {
            GameObject cell = GameObject.Find("Cell" + (i / 10) + (i % 10));
            cells[i] = cell.GetComponent<CellController>();
            cells[i].Init(i / 10, i % 10, onClick);
        }
    }

    private void onClick(CellController cell)
    {
        if (gameEnd) return;
        Debug.Log("OnClick cell.[" + cell.x + "," +cell.y + "]");
        if (cell.clickType == LEFT_CLICK)
        {
            expand(cell);
        }
        if (cell.clickType == RIGHT_CLICK)
        {
            if (cell.flag.activeInHierarchy) disableFlag(cell);
            else if (cell.tile.activeInHierarchy) enableFlag(cell);
        }


    }

    private void expand(CellController cell)
    {
        if (gameEnd) return;
        if (!cell.tile.activeInHierarchy) return;
        cell.tile.SetActive(false);

        if (checkGameEnd(cell))
        {
            endGame();
            cell.mine3.SetActive(true);
            return;
        }

        CellController[] neighbours = getNeighbours(cell);
        int bombsCount = countBombs(neighbours);
        if (bombsCount > 0)
        {
            cell.text.SetActive(true);
            cell.text.GetComponent<Text>().text = ""+bombsCount;
            return;
        }

        expandNeighbours(neighbours);
        
    }

    private void endGame()
    {
        this.gameEnd = true;
        for (int i = 0; i < 100; i++)
        {
            if (cells[i].isBomb)
            {
                if (cells[i].flag.activeInHierarchy)
                {
                    cells[i].flag.SetActive(false);
                    cells[i].mine2.SetActive(true);
                } else
                {
                    cells[i].tile.SetActive(false);
                    cells[i].mine1.SetActive(true);
                }
            } else
            {
                
            }
        }
    }

    private void expandNeighbours(CellController[] neighbours)
    {
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] != null)
            {
                expand(neighbours[i]);
            }
        }
    }

    private int countBombs(CellController[] neighbours)
    {
        int counter = 0;
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] != null && neighbours[i].isBomb) counter++; 
        }
        return counter;
    }

    private CellController[] getNeighbours(CellController cell)
    {
        CellController[] neighbours = new CellController[8];
        int counter = 0;
        int x = cell.x;
        int y = cell.y;
        if (checkInRange(x, y+1))
        {
            if (cells[x * 10 + (y + 1)].tile.activeInHierarchy) neighbours[counter++] = cells[x * 10 + (y + 1)];
        }
        if (checkInRange(x, y - 1))
        {
            if (cells[x * 10 + (y - 1)].tile.activeInHierarchy) neighbours[counter++] = cells[x * 10 + (y - 1)];
        }
        if (checkInRange(x + 1, y))
        {
            if (cells[(x +1) * 10 + y].tile.activeInHierarchy) neighbours[counter++] = cells[(x + 1) * 10 + y];
        }
        if (checkInRange(x - 1, y))
        {
            if (cells[(x - 1) * 10 + y].tile.activeInHierarchy) neighbours[counter++] = cells[(x - 1) * 10 + y];
        }
        if (checkInRange(x + 1, y + 1))
        {
            if (cells[(x + 1) * 10 + (y + 1)].tile.activeInHierarchy) neighbours[counter++] = cells[(x + 1) * 10 + (y + 1)];
        }
        if (checkInRange(x + 1, y - 1))
        {
            if (cells[(x + 1) * 10 + (y - 1)].tile.activeInHierarchy) neighbours[counter++] = cells[(x + 1) * 10 + (y - 1)];
        }
        if (checkInRange(x - 1, y + 1))
        {
            if (cells[(x - 1) * 10 + (y + 1)].tile.activeInHierarchy) neighbours[counter++] = cells[(x - 1) * 10 + (y + 1)];
        }

        if (checkInRange(x - 1, y - 1))
        {
            if (cells[(x - 1) * 10 + (y - 1)].tile.activeInHierarchy) neighbours[counter++] = cells[(x - 1) * 10 + (y - 1)];
        }

        return neighbours;

    }

    private bool checkInRange(int x, int y)
    {
        if (x >= 0 && x <= 9 && y >= 0 && y <= 9) return true;
        return false;
    }

    private bool checkGameEnd(CellController cell)
    {
        if (cell.isBomb)
        {
            
            return true;
        }
        else return false;
    }

    

    private void enableFlag(CellController cell)
    {
        cell.tile.SetActive(false);
        cell.flag.SetActive(true);
    }

    private void disableFlag(CellController cell)
    {
        cell.tile.SetActive(true);
        cell.flag.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}
}

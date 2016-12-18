using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
    private GameObject[][] tiles;

    // Use this for initialization
    void Start()
    {
        instansiateTiles();

       
    }

    private void instansiateTiles()
    {
        tiles = new GameObject[10][];

        for (int i = 0; i < 10; i++)
        {
            tiles[i] = new GameObject[10];
            for (int j = 0; j < 10; j++)
            {
                tiles[i][j] = GameObject.Find("Tile" + i + j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

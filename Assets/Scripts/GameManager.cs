using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dicePrefab;
    float x1 = -6;
    float x2 = -6;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateBoard()
    {
        //generate first row
        for (int i = 0; i < 5; i++)
        {
            var dice = Instantiate(dicePrefab);
            dice.transform.position = new Vector2(x1, 0.6f);
            x1 += 3;
        }
        //generate second row
        for (int i = 0; i < 5; i++)
        {
            var dice = Instantiate(dicePrefab);
            dice.transform.position = new Vector2(x2, 3.45f);
            x2 += 3;
        }
    }
}

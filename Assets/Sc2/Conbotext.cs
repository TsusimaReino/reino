using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conbotext : MonoBehaviour
{
    public GameObject[] Candies;

    //配列の大きさを定義。
    private int width = 5;
    private int height = 7;

    //publicでGameObject型の配列を作る。
    public GameObject[,] candyArray = new GameObject[5, 7];

    void Start()
    {
        CreateCandies();
    }

    void CreateCandies()

    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int r = Random.Range(0, 5);

                var candy = Instantiate(Candies[r]);

                //画面の見た目として、candyのtransform.positionを設定
                candy.transform.position = new Vector2(i, j);

                //画面に５×７の表があるイメージで、キャンディの座標をそのまま配列のIndexに利用して、配列の要素にCandyを入れている。
                candyArray[i, j] = candy;
            }
        }
    }
}

    

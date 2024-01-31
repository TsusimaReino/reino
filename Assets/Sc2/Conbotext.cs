using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conbotext : MonoBehaviour
{
    public GameObject[] Candies;

    //�z��̑傫�����`�B
    private int width = 5;
    private int height = 7;

    //public��GameObject�^�̔z������B
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

                //��ʂ̌����ڂƂ��āAcandy��transform.position��ݒ�
                candy.transform.position = new Vector2(i, j);

                //��ʂɂT�~�V�̕\������C���[�W�ŁA�L�����f�B�̍��W�����̂܂ܔz���Index�ɗ��p���āA�z��̗v�f��Candy�����Ă���B
                candyArray[i, j] = candy;
            }
        }
    }
}

    

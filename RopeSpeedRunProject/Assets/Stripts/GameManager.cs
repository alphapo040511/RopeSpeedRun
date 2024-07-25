using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;             //���� �޴��� static���� ����

    public int treasureCount { get; private set; }  //���� ȹ���� ���� ����
    public float TimeScore { get; private set; }    //�ð� ��Ͽ� float

    private float startTime;                        //���� ���� �ð�

    private void Awake()                            //�̱��� ����
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public void GetTreasure()   //���� ȹ��� ���� ����
    {
        treasureCount += 1;
    }

    public void GameStart()     //���� ���۽� �ʱ�ȭ
    {
        treasureCount = 0;
        TimeScore = 0;
        startTime = Time.time;
    }

    public void EndGame()                       //���� ����
    {
        TimeScore = Time.time - startTime;      //Ÿ�� ���ھ �־���
    }
}

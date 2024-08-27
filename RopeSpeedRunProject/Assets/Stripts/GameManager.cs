using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;             //���� �޴��� static���� ����

    public int treasureCount { get; private set; }  //���� ȹ���� ���� ����
    public float TimeScore { get; private set; }    //�ð� ��Ͽ� float

    public string Name;

    private float startTime;                        //���� ���� �ð�

    public string BestScorePlayerName{ get; private set; }
    public float BestScoreTime { get; private set; }

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

    public void StartGameScene(string name)
    {
        Name = name;
        SceneManager.LoadScene("GameScene");
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
        Debug.Log(TimeScore + "��");
        SceneManager.LoadScene("GameOverScene");
    }

    public void NewBestScore()
    {
        BestScorePlayerName = Name;

        BestScoreTime = TimeScore;
    }

    public void TitleSceneLoad()
    {
        SceneManager.LoadScene("GameLobby");
    }
}

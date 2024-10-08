using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;             //게임 메니저 static으로 선언

    public int treasureCount { get; private set; }  //현재 획득한 보물 개수
    public float TimeScore { get; private set; }    //시간 기록용 float

    public string Name;

    private float startTime;                        //게임 시작 시간

    public string BestScorePlayerName{ get; private set; }
    public float BestScoreTime { get; private set; }

    private void Awake()                            //싱글톤 선언
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

    public void GetTreasure()   //보물 획득시 개수 증가
    {
        treasureCount += 1;
    }

    public void StartGameScene(string name)
    {
        Name = name;
        SceneManager.LoadScene("GameScene");
    }

    public void GameStart()     //게임 시작시 초기화
    {
        treasureCount = 0;
        TimeScore = 0;
        startTime = Time.time;
    }

    public void EndGame()                       //게임 종료
    {
        TimeScore = Time.time - startTime;      //타임 스코어를 넣어줌
        Debug.Log(TimeScore + "초");
        SceneManager.LoadScene("GameOverScene");
    }

    public void NewBestScore()
    {
        BestScorePlayerName = Name;

        BestScoreTime = TimeScore;
    }

    public void TitleSceneLoad()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

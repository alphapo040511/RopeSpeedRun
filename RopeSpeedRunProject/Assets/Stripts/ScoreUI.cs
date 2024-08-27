using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI[] ScoreText;
    public TextMeshProUGUI NewRecord;

    private GameManager gameManager;

    private string BestPlayer, NowPlayer;
    private float BestScore, NowScore;

    private float SceneLifeTime = 5f;
    private float RescordUI = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        BestPlayer = gameManager.BestScorePlayerName;
        NowPlayer = gameManager.Name;
        BestScore = gameManager.BestScoreTime;
        NowScore = gameManager.TimeScore;

        ScoreText[0].text = (BestPlayer == null) ? "NoData" : BestPlayer;
        ScoreText[1].text = (BestPlayer == null)? "--:--.--":((int)(BestScore / 60)).ToString("00") + ":" + (BestScore - ((int)(BestScore / 60)) * 60).ToString("00.00");
        ScoreText[2].text = NowPlayer;
        ScoreText[3].text = ((int)(NowScore / 60)).ToString("00") + ":" + (NowScore - ((int)(NowScore / 60)) * 60).ToString("00.00");

        if (NowScore < BestScore || BestScore == 0)
        {
            Debug.Log("½Å±â·Ï!");
            gameManager.NewBestScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SceneLifeTime -= Time.deltaTime;
        RescordUI -= Time.deltaTime;

        if ((NowScore < BestScore || BestScore == 0) && RescordUI <= 0)
        {
            NewRecord.enabled = NewRecord.enabled ? false:true;
            RescordUI = 0.5f;
        }

        if(SceneLifeTime < 0)
        {
            gameManager.TitleSceneLoad();
        }
    }
}

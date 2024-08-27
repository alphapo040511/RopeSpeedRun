using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChecker : MonoBehaviour
{
    public GameObject Player;                   //플레이어
    public Transform StartPos;                  //처음 시작시 이동할 목적지

    private GameManager gameManager;            //게임 메니저
    private PlayerConroller conroller;          //처음 이동시 사용할 플레이어컨트롤 컴포넌트

    private bool IsPlay = false;                //지금 플레이 중인지 확인
    private bool IsOver = false;                //게임이 종료 됐는지 확인

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        conroller = Player.GetComponent<PlayerConroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOver) return;                     //게임이 종료 됐으면 리턴

        //if(Input.GetKeyDown(KeyCode.E))         //E를 누르면 게임 오버 (임시로 만듦)
        //{
        //    GameStop();
        //}

        if(!IsPlay)                                                     //플레이 중이 아닐때 (준비상태)
        {
            conroller.Movement(0.8f);                                   //0.8의 속도로 우측 이동
            if (Player.transform.position.x >= StartPos.position.x)     //도착지까지 x 값이 이동하면
            {
                GameStart();
            }
            return;             //아직 도착 못했다면 리턴(밑에 추가로 코드 들어갈 예정)
        }

    }

    private void GameStart()        //게임 시작 함수
    {
        IsPlay = true;              //게임 시작
        conroller.IsPlay = true;    //플레이 중 활성화
        conroller.Movement(0);      //움직임 일단 정지
        gameManager.GameStart();    //게임 메니저에 게임 시작 함수 호출
    }

    public void GameStop()         //게임 종료 함수
    {
        IsOver = true;              //게임 종료
        gameManager.EndGame();      //게임 메니저에 게임 종료 함수 호출
    }
}

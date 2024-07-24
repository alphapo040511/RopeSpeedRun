using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab : MonoBehaviour
{
    public GameObject Player;
    private PlayerConroller playerClass;

    private RaycastHit2D hit;                               //로프를 던질 위치를 받아올 변수

    private DistanceJoint2D DistanceJoint2D;                //로프 효과를 위한 DistanceJoint2D 컴포넌트

    //라인 랜더러 설정
    private LineRenderer line;                              //라인 랜더러
    private Vector2 startPos;                               //라인을 그릴 시작위치
    private Vector2 endPos;                                 //라인을 그릴 끝 위치
    public float RopeLength;                                //로프의 최대 길이

    // Start is called before the first frame update
    void Start()
    {
        playerClass = Player.GetComponent<PlayerConroller>();

        line = GetComponent<LineRenderer>();            //LineRenderer 컴포넌트

        line.enabled = false;                           //LineRenderer 비활성화

        line.startWidth = 0.1f;                         //라인의 시작 두께
        line.endWidth = 0.1f;                           //라인의 끝 두께

        line.positionCount = 2;                         //라인을 그릴때 꼭짓점으로 사용할 점의 개수

        DistanceJoint2D = GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = Player.transform.position;                           //시작 위치를 플레이어의 위치로 지정
        endPos = transform.position;                                    //끝 위치를 오브젝트 위치로 지정

        line.SetPosition(0, startPos);                                  //0번 점의 위치를 startPos로 설정
        line.SetPosition(1, endPos);                                    //1번 점의 위치를 endPos로 설정
    }


    //로프를 던지는 함수
    public void StartGrab(bool Dash)
    {
        Vector2 point = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)); //마우스 커서 위치를 받아옴

        line.enabled = true;                                                                    //라인 랜더러 활성화

        transform.LookAt(point);                                                                //마우스 커서 위치를 바라보게 한다.

        if(Dash)                                                                                //대쉬 그랩일 경우
        {
            DistanceJoint2D.autoConfigureDistance = false;                                      //조인트 길이 자동 설정 false
            DistanceJoint2D.distance = 0.5f;                                                    //조인트 길이 0.5로 변경
        }
        else
        {
            DistanceJoint2D.autoConfigureDistance = true;                                       //조인트 길이 자동 설정 true
        }

        hit = Physics2D.Raycast(Player.transform.position, transform.forward, RopeLength);      //마우스 커서 위치로 레이를 쏜다.

        if (hit)                                                                                //레이가 부딪힌 물체가 있을때
        {
            if (hit.collider.transform.tag == "AbleGrab")                                       //매달릴 수 있는 물체일 때
            {
                transform.DOMove(hit.point, 0.2f).SetEase(Ease.Linear).OnComplete(this.HangOn);   //목표 위치까지 이동후 고정
            }
            else                                                                                //매달릴 수 없는 물체일 때
            {
                transform.DOMove(hit.point, 0.2f).SetEase(Ease.Linear).OnComplete(ReturnGrab);  //해당 위치까지 날아간 후 돌아옴
            }
        }
        else                                                                                    //로프의 최대 길이보다 먼 경우
        {
            transform.DOMove(transform.position + transform.forward * RopeLength, 0.2f).SetEase(Ease.Linear).OnComplete(ReturnGrab); //최대 거리까지 날아간 후 돌아옴
        }

    }
    

    //후크를 되돌리는 함수
    private void ReturnGrab()
    {            
        line.enabled = false;                       //라인 랜더러 비활성화
        playerClass.isGrapped = false;              //그랩중 상태 해제
    }

    //매달리는 함수
    public void HangOn()
    {
        DistanceJoint2D.enabled = true;             //조인트 컴포넌트 활성화

        //길이에 관한 내용 추가 예정
    }
}

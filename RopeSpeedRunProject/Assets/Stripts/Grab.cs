using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab : MonoBehaviour
{
    public GameObject Player;
    private PlayerConroller playerClass;

    private RaycastHit hit;                                 //로프를 던질 위치를 받아올 변수

    private SpringJoint2D Spring;                           //로프 효과를 위한 SpringJoint2D 컴포넌트

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

        Spring = Player.GetComponent<SpringJoint2D>();  //SpringJoint2D 컴포넌트를 받아옴
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
    public void StartGrab()
    {
        Vector2 point = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)); //마우스 커서 위치를 받아옴

        line.enabled = true;                                                                    //라인 랜더러 활성화

        transform.LookAt(point);                                                                //마우스 커서 위치를 바라보게 한다.

        if(Physics.Raycast(Player.transform.position, transform.forward, out hit, RopeLength))  //마우스 커서 위치로 레이를 쏜다.
        {
            if (hit.collider.transform.tag == "AbleGrab")                                       //매달릴 수 있는 물체일 때
            {
                transform.DOMove(hit.point, 0.2f).SetEase(Ease.Linear).OnComplete(HangOn);      //목표 위치까지 이동후 고정
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
        Vector2 Pos = (Vector2)hit.point;                               //후크가 걸릴 위치

        Spring.enabled = true;                                          //스프링 컴포넌트 활성화
        Spring.autoConfigureConnectedAnchor = false;                    //연결된 앵커 자동설정 비활성화
        Spring.connectedAnchor = Pos;                                   //연결된 앵커 위치를 후크 위치로 설정

        //float dis = Vector2.Distance((Vector2)transform.position, Pos); //후크와 플레이어 사이의 거리
        //Spring.distance = dis * 9.5f;                                   //스프링 길이 설정
    }
}

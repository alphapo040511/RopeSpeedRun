using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public Grab Hook;                                       //후크 오브젝트의 Grab 클래스
    public SpringJoint2D Spring;

    private Rigidbody2D m_rigidbody2D;

    [SerializeField] private float speed = 5.0f;            //이동속도
    [SerializeField] private float JumpForce = 500.0f;      //점프하는 힘
    [SerializeField] private int DashGrabCount = 0;         //돌진 그랩(우클릭) 사용 가능 횟수

    private Timer RopeCoolTime = new Timer(0.5f);           //로프 사용 쿨타임 설정

    public bool isGrapped = false;                          //현재 매달려 있는지 확인할 bool 값

    // Start is called before the first frame update
    void Start()
    {
        Spring = GetComponent<SpringJoint2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RopeCoolTime.Update(Time.deltaTime);                                //타이머의 시간 갱신

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))         //마우스 클릭을 때면
        {
            isGrapped = false;                                              //그랩중 상태 해제
            Spring.enabled = false;                                         //스프링 컴포넌트 비활성화
        }

        if (!isGrapped)                                                     //그랩중이 아닐때
        {
            Hook.transform.position = transform.position;                   //후크가 플레이어를 따라다니게 한다.

            if (Input.GetMouseButtonDown(0) && !RopeCoolTime.IsRunning())   //로프 재사용 대기시간이 아닐때 좌클릭을 하면
            {
                Grab(false);        //그랩 함수
            }

            //대쉬 그랩
            if (Input.GetMouseButtonDown(1) && !RopeCoolTime.IsRunning() && DashGrabCount > 0)  //대쉬 횟수가 남아있을때 우클릭하면
            {
                Grab(true);         //그랩 함수
            }
        }
    }

    private void Grab(bool Dash)    
    {
        RopeCoolTime.Start();           //타이머 실행

        isGrapped = true;               //그랩중 상태로 만듦

        Hook.StartGrab(Dash);           //후크를 날리는 함수 실행
    }
}

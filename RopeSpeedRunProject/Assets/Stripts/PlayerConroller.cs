using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public Grab Hook;                                       //후크 오브젝트의 Grab 클래스

    private DistanceJoint2D DistanceJoint2D;                //로프 효과를 위한 DistanceJoint2D 컴포넌트

    public LayerMask m_WhatIsGround;                        //땅에 해당하는 오브젝트가 존재하는 레이어 마스크
    public Transform GroundCheck;                           //땅에 있는지 체크하기 위한 빈 오브젝트의 Transform

    public bool IsPlay = false;                             //플레이 중인지 확인

    private Rigidbody2D m_rigidbody2D;

    [SerializeField] private float speed = 5.0f;            //이동속도
    [SerializeField] private float JumpForce = 500.0f;      //점프하는 힘
    [SerializeField] private int DashGrabCount = 0;         //돌진 그랩(우클릭) 사용 가능 횟수

    private Timer RopeCoolTime = new Timer(0.5f);           //로프 사용 쿨타임 설정
    private Timer IsDash = new Timer(0.2f);                 //대쉬 지속 시간

    public bool isGrapped = false;                          //현재 매달려 있는지 확인할 bool 값

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        DistanceJoint2D = Hook.GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlay) return;                    //플레이 중이 아니면 리턴

        //타이머의 시간 갱신
        RopeCoolTime.Update(Time.deltaTime);
        IsDash.Update(Time.deltaTime);


        float MoveX = Input.GetAxis("Horizontal");                                      //수평 이동 값 저장

        if (!IsDash.IsRunning() && MoveX != 0)                                          //대쉬를 진행하는 중이 아닐때
        {
            Movement(MoveX);                    //이동 함수
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())                            //현재 땅에 있고, 스페이스를 눌렀을때
        {
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x,0);           //수직방향 속도 초기화
            m_rigidbody2D.AddForce(Vector2.up * JumpForce);                             //점프
        }

        if(m_rigidbody2D.velocity.y >= 10)                                              //수직방향 속도가 최대속도를 넘어갔을때
        {
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 10);         //수직 속도 제한
        }


        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) ||
                            Hook.transform.position.y <= transform.position.y)          //마우스 클릭을 때거나 플레이어의 높이가 후크보다 높을 경우
        {
            isGrapped = false;                                                          //그랩중 상태 해제
            DistanceJoint2D.enabled = false;                                            //조인트 해제
        }

        if (!isGrapped)                                                     //그랩중이 아닐때
        {
            DistanceJoint2D.enabled = false;                                //조인트 해제

            Hook.transform.position = transform.position;                   //후크가 플레이어를 따라다니게 한다.

            if (Input.GetMouseButtonDown(0) && !RopeCoolTime.IsRunning())   //로프 재사용 대기시간이 아닐때 좌클릭을 하면
            {
                Grab(false);        //그랩 함수
            }

            //대쉬 그랩
            if (Input.GetMouseButtonDown(1) && !RopeCoolTime.IsRunning() && DashGrabCount > 0)  //대쉬 횟수가 남아있을때 우클릭하면
            {
                DashGrabCount--;    //대쉬 그랩 횟수 감소
                IsDash.Start();     //대쉬 카운트 시작
                Grab(true);         //그랩 함수
            }
        }
    }

    public void Movement(float MoveX)
    {
        m_rigidbody2D.velocity = new Vector2(MoveX * speed, m_rigidbody2D.velocity.y);      //수평이동 입력
    }

    private void Grab(bool Dash)    
    {
        RopeCoolTime.Start();           //타이머 실행

        isGrapped = true;               //그랩중 상태로 만듦

        Hook.StartGrab(Dash);           //후크를 날리는 함수 실행
    }

    private bool IsGrounded()           //땅에 있는지 확인하는 함수
    {
        Collider2D[] groundColliers = Physics2D.OverlapCircleAll(GroundCheck.position , 0.2f, m_WhatIsGround);  //바닥을 인식하는 오브젝트 위치에서 오버랩서클로 바닥 인식
        
        for(int i = 0; i < groundColliers.Length; i++)              //인식한 모든 콜라이더중에
        {
            if (groundColliers[i].gameObject != gameObject)         //플레이어가 아닌 콜라이더가 있다면
            {
                return true;                                        //true 리턴
            }
        }
        return false;
    }
}

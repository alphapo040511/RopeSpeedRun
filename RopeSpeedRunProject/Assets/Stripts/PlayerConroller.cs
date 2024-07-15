using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public Grab Hook;                                       //후크 오브젝트의 Grab 클래스
    public SpringJoint2D Spring;

    private Timer RopeCoolTime = new Timer(0.5f);           //로프 사용 쿨타임 설정

    public bool isGrapped = false;                          //현재 매달려 있는지 확인할 bool 값

    // Start is called before the first frame update
    void Start()
    {
        Spring = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RopeCoolTime.Update(Time.deltaTime);                                //타이머의 시간 갱신

        if (Input.GetMouseButtonUp(0))                      //마우스 좌클릭을 때면
        {
            isGrapped = false;                              //그랩중 상태 해제
            Spring.enabled = false;                         //스프링 컴포넌트 비활성화
        }

        if (!isGrapped)                                                     //그랩중이 아닐때
        {
            if (Input.GetMouseButtonDown(0) && !RopeCoolTime.IsRunning())   //로프 재사용 대기시간이 아닐때 좌클릭을 하면
            {
                RopeCoolTime.Start();                                       //타이머 실행

                isGrapped = true;                                           //그랩중 상태로 만듦

                Hook.StartGrab();                                           //후크를 날리는 함수 실행

            }
            Hook.transform.position = transform.position;                   //후크가 플레이어를 따라다니게 한다.
        }


    }


}

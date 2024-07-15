using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float duration;             //동작 시간    
    private float remainingTime;        //남은 시간 
    private bool isRunning;             //동작 중인지 판단 

    public Timer(float duration)        //클래스 생성자 [클래스가 만들어질 때 초기화]
    {
        this.duration = duration;
        this.remainingTime = duration;
        this.isRunning = false;
    }

    public void Start()                     //스타트 생명주기에서 사용할때 동작 시작 해주는 함수 
    {
        this.remainingTime = duration;
        this.isRunning = true;
    }

    public void Update(float deltaTime)     //Update 함수에서 DeltaTime을 받아온다.
    {
        if (isRunning)                       //동작 중이면
        {
            remainingTime -= deltaTime;     //받아온 DeltaTime을 감소시킨다. 
            if (remainingTime <= 0)         //시간이 다 소모 되면
            {
                isRunning = false;          //동작 중지
                remainingTime = 0;          //남은 시간 0
            }
        }
    }
    
    public float RemainingTime()            //남은 대기 시간을 리턴 시켜주는 함수
    {
        return remainingTime;
    }

    public bool IsRunning()                 //타이머가 작동중인지 리턴 시켜주는 함수
    {
        return isRunning; 
    }

    public void Reset()                     //초기화 시켜 주는 함수 
    {
        this.remainingTime = duration;
        this.isRunning = false;
    }
}
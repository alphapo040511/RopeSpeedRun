using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float duration;             //���� �ð�    
    private float remainingTime;        //���� �ð� 
    private bool isRunning;             //���� ������ �Ǵ� 

    public Timer(float duration)        //Ŭ���� ������ [Ŭ������ ������� �� �ʱ�ȭ]
    {
        this.duration = duration;
        this.remainingTime = duration;
        this.isRunning = false;
    }

    public void Start()                     //��ŸƮ �����ֱ⿡�� ����Ҷ� ���� ���� ���ִ� �Լ� 
    {
        this.remainingTime = duration;
        this.isRunning = true;
    }

    public void Update(float deltaTime)     //Update �Լ����� DeltaTime�� �޾ƿ´�.
    {
        if (isRunning)                       //���� ���̸�
        {
            remainingTime -= deltaTime;     //�޾ƿ� DeltaTime�� ���ҽ�Ų��. 
            if (remainingTime <= 0)         //�ð��� �� �Ҹ� �Ǹ�
            {
                isRunning = false;          //���� ����
                remainingTime = 0;          //���� �ð� 0
            }
        }
    }
    
    public float RemainingTime()            //���� ��� �ð��� ���� �����ִ� �Լ�
    {
        return remainingTime;
    }

    public bool IsRunning()                 //Ÿ�̸Ӱ� �۵������� ���� �����ִ� �Լ�
    {
        return isRunning; 
    }

    public void Reset()                     //�ʱ�ȭ ���� �ִ� �Լ� 
    {
        this.remainingTime = duration;
        this.isRunning = false;
    }
}
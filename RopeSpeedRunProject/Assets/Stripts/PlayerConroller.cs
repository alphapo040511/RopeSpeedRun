using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public Grab Hook;                                       //��ũ ������Ʈ�� Grab Ŭ����
    public SpringJoint2D Spring;

    private Timer RopeCoolTime = new Timer(0.5f);           //���� ��� ��Ÿ�� ����

    public bool isGrapped = false;                          //���� �Ŵ޷� �ִ��� Ȯ���� bool ��

    // Start is called before the first frame update
    void Start()
    {
        Spring = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RopeCoolTime.Update(Time.deltaTime);                                //Ÿ�̸��� �ð� ����

        if (Input.GetMouseButtonUp(0))                      //���콺 ��Ŭ���� ����
        {
            isGrapped = false;                              //�׷��� ���� ����
            Spring.enabled = false;                         //������ ������Ʈ ��Ȱ��ȭ
        }

        if (!isGrapped)                                                     //�׷����� �ƴҶ�
        {
            if (Input.GetMouseButtonDown(0) && !RopeCoolTime.IsRunning())   //���� ���� ���ð��� �ƴҶ� ��Ŭ���� �ϸ�
            {
                RopeCoolTime.Start();                                       //Ÿ�̸� ����

                isGrapped = true;                                           //�׷��� ���·� ����

                Hook.StartGrab();                                           //��ũ�� ������ �Լ� ����

            }
            Hook.transform.position = transform.position;                   //��ũ�� �÷��̾ ����ٴϰ� �Ѵ�.
        }


    }


}

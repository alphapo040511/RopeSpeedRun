using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public Grab Hook;                                       //��ũ ������Ʈ�� Grab Ŭ����
    public SpringJoint2D Spring;

    private Rigidbody2D m_rigidbody2D;

    [SerializeField] private float speed = 5.0f;            //�̵��ӵ�
    [SerializeField] private float JumpForce = 500.0f;      //�����ϴ� ��
    [SerializeField] private int DashGrabCount = 0;         //���� �׷�(��Ŭ��) ��� ���� Ƚ��

    private Timer RopeCoolTime = new Timer(0.5f);           //���� ��� ��Ÿ�� ����

    public bool isGrapped = false;                          //���� �Ŵ޷� �ִ��� Ȯ���� bool ��

    // Start is called before the first frame update
    void Start()
    {
        Spring = GetComponent<SpringJoint2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RopeCoolTime.Update(Time.deltaTime);                                //Ÿ�̸��� �ð� ����

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))         //���콺 Ŭ���� ����
        {
            isGrapped = false;                                              //�׷��� ���� ����
            Spring.enabled = false;                                         //������ ������Ʈ ��Ȱ��ȭ
        }

        if (!isGrapped)                                                     //�׷����� �ƴҶ�
        {
            Hook.transform.position = transform.position;                   //��ũ�� �÷��̾ ����ٴϰ� �Ѵ�.

            if (Input.GetMouseButtonDown(0) && !RopeCoolTime.IsRunning())   //���� ���� ���ð��� �ƴҶ� ��Ŭ���� �ϸ�
            {
                Grab(false);        //�׷� �Լ�
            }

            //�뽬 �׷�
            if (Input.GetMouseButtonDown(1) && !RopeCoolTime.IsRunning() && DashGrabCount > 0)  //�뽬 Ƚ���� ���������� ��Ŭ���ϸ�
            {
                Grab(true);         //�׷� �Լ�
            }
        }
    }

    private void Grab(bool Dash)    
    {
        RopeCoolTime.Start();           //Ÿ�̸� ����

        isGrapped = true;               //�׷��� ���·� ����

        Hook.StartGrab(Dash);           //��ũ�� ������ �Լ� ����
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public Grab Hook;                                       //��ũ ������Ʈ�� Grab Ŭ����

    private DistanceJoint2D DistanceJoint2D;                //���� ȿ���� ���� DistanceJoint2D ������Ʈ

    public LayerMask m_WhatIsGround;                        //���� �ش��ϴ� ������Ʈ�� �����ϴ� ���̾� ����ũ
    public Transform GroundCheck;                           //���� �ִ��� üũ�ϱ� ���� �� ������Ʈ�� Transform

    public bool IsPlay = false;                             //�÷��� ������ Ȯ��

    private Rigidbody2D m_rigidbody2D;

    [SerializeField] private float speed = 5.0f;            //�̵��ӵ�
    [SerializeField] private float JumpForce = 500.0f;      //�����ϴ� ��
    [SerializeField] private int DashGrabCount = 0;         //���� �׷�(��Ŭ��) ��� ���� Ƚ��

    private Timer RopeCoolTime = new Timer(0.5f);           //���� ��� ��Ÿ�� ����
    private Timer IsDash = new Timer(0.2f);                 //�뽬 ���� �ð�

    public bool isGrapped = false;                          //���� �Ŵ޷� �ִ��� Ȯ���� bool ��

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        DistanceJoint2D = Hook.GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlay) return;                    //�÷��� ���� �ƴϸ� ����

        //Ÿ�̸��� �ð� ����
        RopeCoolTime.Update(Time.deltaTime);
        IsDash.Update(Time.deltaTime);


        float MoveX = Input.GetAxis("Horizontal");                                      //���� �̵� �� ����

        if (!IsDash.IsRunning() && MoveX != 0)                                          //�뽬�� �����ϴ� ���� �ƴҶ�
        {
            Movement(MoveX);                    //�̵� �Լ�
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())                            //���� ���� �ְ�, �����̽��� ��������
        {
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x,0);           //�������� �ӵ� �ʱ�ȭ
            m_rigidbody2D.AddForce(Vector2.up * JumpForce);                             //����
        }

        if(m_rigidbody2D.velocity.y >= 10)                                              //�������� �ӵ��� �ִ�ӵ��� �Ѿ����
        {
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 10);         //���� �ӵ� ����
        }


        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) ||
                            Hook.transform.position.y <= transform.position.y)          //���콺 Ŭ���� ���ų� �÷��̾��� ���̰� ��ũ���� ���� ���
        {
            isGrapped = false;                                                          //�׷��� ���� ����
            DistanceJoint2D.enabled = false;                                            //����Ʈ ����
        }

        if (!isGrapped)                                                     //�׷����� �ƴҶ�
        {
            DistanceJoint2D.enabled = false;                                //����Ʈ ����

            Hook.transform.position = transform.position;                   //��ũ�� �÷��̾ ����ٴϰ� �Ѵ�.

            if (Input.GetMouseButtonDown(0) && !RopeCoolTime.IsRunning())   //���� ���� ���ð��� �ƴҶ� ��Ŭ���� �ϸ�
            {
                Grab(false);        //�׷� �Լ�
            }

            //�뽬 �׷�
            if (Input.GetMouseButtonDown(1) && !RopeCoolTime.IsRunning() && DashGrabCount > 0)  //�뽬 Ƚ���� ���������� ��Ŭ���ϸ�
            {
                DashGrabCount--;    //�뽬 �׷� Ƚ�� ����
                IsDash.Start();     //�뽬 ī��Ʈ ����
                Grab(true);         //�׷� �Լ�
            }
        }
    }

    public void Movement(float MoveX)
    {
        m_rigidbody2D.velocity = new Vector2(MoveX * speed, m_rigidbody2D.velocity.y);      //�����̵� �Է�
    }

    private void Grab(bool Dash)    
    {
        RopeCoolTime.Start();           //Ÿ�̸� ����

        isGrapped = true;               //�׷��� ���·� ����

        Hook.StartGrab(Dash);           //��ũ�� ������ �Լ� ����
    }

    private bool IsGrounded()           //���� �ִ��� Ȯ���ϴ� �Լ�
    {
        Collider2D[] groundColliers = Physics2D.OverlapCircleAll(GroundCheck.position , 0.2f, m_WhatIsGround);  //�ٴ��� �ν��ϴ� ������Ʈ ��ġ���� ��������Ŭ�� �ٴ� �ν�
        
        for(int i = 0; i < groundColliers.Length; i++)              //�ν��� ��� �ݶ��̴��߿�
        {
            if (groundColliers[i].gameObject != gameObject)         //�÷��̾ �ƴ� �ݶ��̴��� �ִٸ�
            {
                return true;                                        //true ����
            }
        }
        return false;
    }
}

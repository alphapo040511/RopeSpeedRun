using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab : MonoBehaviour
{
    public GameObject Player;
    private PlayerConroller playerClass;

    private RaycastHit2D hit;                               //������ ���� ��ġ�� �޾ƿ� ����

    private DistanceJoint2D DistanceJoint2D;                //���� ȿ���� ���� DistanceJoint2D ������Ʈ

    //���� ������ ����
    private LineRenderer line;                              //���� ������
    private Vector2 startPos;                               //������ �׸� ������ġ
    private Vector2 endPos;                                 //������ �׸� �� ��ġ
    public float RopeLength;                                //������ �ִ� ����

    // Start is called before the first frame update
    void Start()
    {
        playerClass = Player.GetComponent<PlayerConroller>();

        line = GetComponent<LineRenderer>();            //LineRenderer ������Ʈ

        line.enabled = false;                           //LineRenderer ��Ȱ��ȭ

        line.startWidth = 0.1f;                         //������ ���� �β�
        line.endWidth = 0.1f;                           //������ �� �β�

        line.positionCount = 2;                         //������ �׸��� ���������� ����� ���� ����

        DistanceJoint2D = GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = Player.transform.position;                           //���� ��ġ�� �÷��̾��� ��ġ�� ����
        endPos = transform.position;                                    //�� ��ġ�� ������Ʈ ��ġ�� ����

        line.SetPosition(0, startPos);                                  //0�� ���� ��ġ�� startPos�� ����
        line.SetPosition(1, endPos);                                    //1�� ���� ��ġ�� endPos�� ����
    }


    //������ ������ �Լ�
    public void StartGrab(bool Dash)
    {
        Vector2 point = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)); //���콺 Ŀ�� ��ġ�� �޾ƿ�

        line.enabled = true;                                                                    //���� ������ Ȱ��ȭ

        transform.LookAt(point);                                                                //���콺 Ŀ�� ��ġ�� �ٶ󺸰� �Ѵ�.

        if(Dash)                                                                                //�뽬 �׷��� ���
        {
            DistanceJoint2D.autoConfigureDistance = false;                                      //����Ʈ ���� �ڵ� ���� false
            DistanceJoint2D.distance = 0.5f;                                                    //����Ʈ ���� 0.5�� ����
        }
        else
        {
            DistanceJoint2D.autoConfigureDistance = true;                                       //����Ʈ ���� �ڵ� ���� true
        }

        hit = Physics2D.Raycast(Player.transform.position, transform.forward, RopeLength);      //���콺 Ŀ�� ��ġ�� ���̸� ���.

        if (hit)                                                                                //���̰� �ε��� ��ü�� ������
        {
            if (hit.collider.transform.tag == "AbleGrab")                                       //�Ŵ޸� �� �ִ� ��ü�� ��
            {
                transform.DOMove(hit.point, 0.2f).SetEase(Ease.Linear).OnComplete(this.HangOn);   //��ǥ ��ġ���� �̵��� ����
            }
            else                                                                                //�Ŵ޸� �� ���� ��ü�� ��
            {
                transform.DOMove(hit.point, 0.2f).SetEase(Ease.Linear).OnComplete(ReturnGrab);  //�ش� ��ġ���� ���ư� �� ���ƿ�
            }
        }
        else                                                                                    //������ �ִ� ���̺��� �� ���
        {
            transform.DOMove(transform.position + transform.forward * RopeLength, 0.2f).SetEase(Ease.Linear).OnComplete(ReturnGrab); //�ִ� �Ÿ����� ���ư� �� ���ƿ�
        }

    }
    

    //��ũ�� �ǵ����� �Լ�
    private void ReturnGrab()
    {            
        line.enabled = false;                       //���� ������ ��Ȱ��ȭ
        playerClass.isGrapped = false;              //�׷��� ���� ����
    }

    //�Ŵ޸��� �Լ�
    public void HangOn()
    {
        DistanceJoint2D.enabled = true;             //����Ʈ ������Ʈ Ȱ��ȭ

        //���̿� ���� ���� �߰� ����
    }
}

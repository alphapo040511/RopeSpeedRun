using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab : MonoBehaviour
{
    public GameObject Player;
    private PlayerConroller playerClass;

    private RaycastHit hit;                                 //������ ���� ��ġ�� �޾ƿ� ����

    private SpringJoint2D Spring;                           //���� ȿ���� ���� SpringJoint2D ������Ʈ

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

        Spring = Player.GetComponent<SpringJoint2D>();  //SpringJoint2D ������Ʈ�� �޾ƿ�
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
    public void StartGrab()
    {
        Vector2 point = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)); //���콺 Ŀ�� ��ġ�� �޾ƿ�

        line.enabled = true;                                                                    //���� ������ Ȱ��ȭ

        transform.LookAt(point);                                                                //���콺 Ŀ�� ��ġ�� �ٶ󺸰� �Ѵ�.

        if(Physics.Raycast(Player.transform.position, transform.forward, out hit, RopeLength))  //���콺 Ŀ�� ��ġ�� ���̸� ���.
        {
            if (hit.collider.transform.tag == "AbleGrab")                                       //�Ŵ޸� �� �ִ� ��ü�� ��
            {
                transform.DOMove(hit.point, 0.2f).SetEase(Ease.Linear).OnComplete(HangOn);      //��ǥ ��ġ���� �̵��� ����
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
        Vector2 Pos = (Vector2)hit.point;                               //��ũ�� �ɸ� ��ġ

        Spring.enabled = true;                                          //������ ������Ʈ Ȱ��ȭ
        Spring.autoConfigureConnectedAnchor = false;                    //����� ��Ŀ �ڵ����� ��Ȱ��ȭ
        Spring.connectedAnchor = Pos;                                   //����� ��Ŀ ��ġ�� ��ũ ��ġ�� ����

        //float dis = Vector2.Distance((Vector2)transform.position, Pos); //��ũ�� �÷��̾� ������ �Ÿ�
        //Spring.distance = dis * 9.5f;                                   //������ ���� ����
    }
}

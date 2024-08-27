using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChecker : MonoBehaviour
{
    public GameObject Player;                   //�÷��̾�
    public Transform StartPos;                  //ó�� ���۽� �̵��� ������

    private GameManager gameManager;            //���� �޴���
    private PlayerConroller conroller;          //ó�� �̵��� ����� �÷��̾���Ʈ�� ������Ʈ

    private bool IsPlay = false;                //���� �÷��� ������ Ȯ��
    private bool IsOver = false;                //������ ���� �ƴ��� Ȯ��

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        conroller = Player.GetComponent<PlayerConroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOver) return;                     //������ ���� ������ ����

        //if(Input.GetKeyDown(KeyCode.E))         //E�� ������ ���� ���� (�ӽ÷� ����)
        //{
        //    GameStop();
        //}

        if(!IsPlay)                                                     //�÷��� ���� �ƴҶ� (�غ����)
        {
            conroller.Movement(0.8f);                                   //0.8�� �ӵ��� ���� �̵�
            if (Player.transform.position.x >= StartPos.position.x)     //���������� x ���� �̵��ϸ�
            {
                GameStart();
            }
            return;             //���� ���� ���ߴٸ� ����(�ؿ� �߰��� �ڵ� �� ����)
        }

    }

    private void GameStart()        //���� ���� �Լ�
    {
        IsPlay = true;              //���� ����
        conroller.IsPlay = true;    //�÷��� �� Ȱ��ȭ
        conroller.Movement(0);      //������ �ϴ� ����
        gameManager.GameStart();    //���� �޴����� ���� ���� �Լ� ȣ��
    }

    public void GameStop()         //���� ���� �Լ�
    {
        IsOver = true;              //���� ����
        gameManager.EndGame();      //���� �޴����� ���� ���� �Լ� ȣ��
    }
}

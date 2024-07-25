using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureInteraction : MonoBehaviour
{
    [Header("��ȣ�ۿ� ���� �ִ� �Ÿ�")][SerializeField] private float Interaction_Dis;    //�÷��̾�� ��ȣ�ۿ� ������ �ִ� �Ÿ�


    [Header("���� �̸�")][SerializeField] private string m_TreasureName;                //Ȥ�� ����� ���� �־ �־���� ���� ����
    [Header("���� �̹���")][SerializeField] private Sprite m_Sprite;                    //���� �̹���
    [Header("���� ��ȣ")][SerializeField] private int m_Number;

    private GameManager gameManager;
    private Transform playerTansform;            //�÷��̾��� Transform
    private bool isInteractable = false;         //��ȣ�ۿ� ������ �������� Ȯ�ο� bool ��

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerTansform = GameObject.FindWithTag("Player").transform;                    //Player �±׷� �÷��̾ ã��
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector2.Distance(playerTansform.position, transform.position);      //�÷��̾�� ������ �Ÿ��� ����

        if(dis <= Interaction_Dis)          //������ �Ÿ��� ��ȣ�ۿ� ���� �ִ�Ÿ����� ����� ���
        {
            isInteractable = true;          //��ȣ�ۿ� ���� ���·� ����
        }

       if(isInteractable == false) return;  //��ȣ�ۿ��� �Ұ��� �ϸ� ����

       if(Input.GetKeyDown(KeyCode.F))
        {
            Interaction();
        }
    }

    private void Interaction()
    {
        Debug.Log("���� ȹ��");

        gameManager.GetTreasure();

        Destroy(gameObject);
    }
}

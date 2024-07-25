using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureInteraction : MonoBehaviour
{
    [Header("상호작용 가능 최대 거리")][SerializeField] private float Interaction_Dis;    //플레이어와 상호작용 가능한 최대 거리


    [Header("보물 이름")][SerializeField] private string m_TreasureName;                //혹시 사용할 수도 있어서 넣어놓은 보물 정보
    [Header("보물 이미지")][SerializeField] private Sprite m_Sprite;                    //보물 이미지
    [Header("보물 번호")][SerializeField] private int m_Number;

    private GameManager gameManager;
    private Transform playerTansform;            //플레이어의 Transform
    private bool isInteractable = false;         //상호작용 가능한 상태인지 확인용 bool 값

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerTansform = GameObject.FindWithTag("Player").transform;                    //Player 태그로 플레이어를 찾음
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector2.Distance(playerTansform.position, transform.position);      //플레이어와 보물의 거리를 저장

        if(dis <= Interaction_Dis)          //떨어진 거리가 상호작용 가능 최대거리보다 가까울 경우
        {
            isInteractable = true;          //상호작용 가능 상태로 만듦
        }

       if(isInteractable == false) return;  //상호작용이 불가능 하면 리턴

       if(Input.GetKeyDown(KeyCode.F))
        {
            Interaction();
        }
    }

    private void Interaction()
    {
        Debug.Log("보물 획득");

        gameManager.GetTreasure();

        Destroy(gameObject);
    }
}

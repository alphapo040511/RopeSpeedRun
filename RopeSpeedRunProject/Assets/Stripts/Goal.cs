using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Goal : MonoBehaviour
{
    public GameObject TreasureGroup;

    [Header("ÃÑ º¸¹° °³¼ö")][SerializeField] private int TotalTreasureCount;

    private GameManager gameManager;
    private bool isOpen = false;

        
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        TotalTreasureCount = TreasureGroup.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(TotalTreasureCount <= gameManager.treasureCount && !isOpen)
        {
            isOpen = true;
            transform.DOMoveY(transform.position.y + 5, 1);
        }
    }
}

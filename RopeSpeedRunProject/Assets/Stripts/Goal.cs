using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Goal : MonoBehaviour
{
    public GameObject TreasureGroup;
    public GameObject Door;
    public ScoreChecker ScoreChecker;

    [Header("ÃÑ º¸¹° °³¼ö")][SerializeField] private int TotalTreasureCount;

    private GameManager gameManager;
    private bool isOpen = false;

        
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        if (TreasureGroup != null)
        {
            TotalTreasureCount = TreasureGroup.transform.childCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TotalTreasureCount <= gameManager.treasureCount && !isOpen)
        {
            isOpen = true;
            Door.transform.DOMoveY(transform.position.y + 10, 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && isOpen)
        {
            ScoreChecker.GameStop();
        }
    }
}

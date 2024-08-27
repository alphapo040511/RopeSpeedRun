using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStart : MonoBehaviour
{
    public TMP_InputField NameInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NameInput.text != "")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("GameStart");
                GameManager.Instance.StartGameScene(NameInput.text);
            }
        }
    }
}

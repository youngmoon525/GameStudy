using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{

    public TextMeshProUGUI talkText;
    public GameObject imageBox;
    public GameObject scanObject;
    public bool isAction;

   
    public void Action(GameObject scanObject)
    {
        if (isAction)
        {
        
            isAction = false;
        }
        else
        {
            isAction = true;
            this.scanObject = scanObject;
            talkText.text = scanObject.name + "";
        }
        imageBox.SetActive(isAction);
     
    }
}

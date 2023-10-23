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
    public TalkManager manager;
    public int talkIndex;
    public Image portrait;
    public void Action(GameObject scanObject)
    {
       
            this.scanObject = scanObject;
            ObjData objData = scanObject.GetComponent<ObjData>();
            talk(objData.id, objData.isNpc);
            //talkText.text = scanObject.name + "";
        imageBox.SetActive(isAction);
     
    }

    public void talk(int id , bool isNpc)
    {
      string talkData = manager.GetTalk(id, talkIndex);
        if (talkData == null)
        {
    
            talkIndex = 0;
            isAction = false;
            return;
        }

        if (isNpc)
        {
          //  Debug.Log(talkData.Split(":")[0] + talkData.Split(":")[1] + id);
            talkText.text = talkData.Split(":")[0];
            portrait.sprite = manager.GetPortrait(id, int.Parse(talkData.Split(":")[1]));
           // portrait.sprite = manager.GetPortrait(1000 , 0);
            portrait.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portrait.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}

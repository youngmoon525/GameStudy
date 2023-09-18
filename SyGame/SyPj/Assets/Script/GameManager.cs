using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int totalPoint, stagePoint, stageIndex , health;
    public PlayerMove player;
    public GameObject[] Stages;

    // Start is called before the first frame updateW
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartButton;


    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }
    public void NextStage()
    {
        if(stageIndex < Stages.Length-1)
        {




        Stages[stageIndex].SetActive(false);
        stageIndex++;
        Stages[stageIndex].SetActive(true);
        PlayerReposition();


            UIStage.text = "STAGE" + (stageIndex + 1);
        }
        else
        {
            Time.timeScale = 0;
            RestartButton.SetActive(true);
            Text btnText = RestartButton.GetComponentInChildren<Text>();
            btnText.text = "Å¬¸®¾î!!";
        }
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    // Update is called once per frame
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(health > 1)
            {
                PlayerReposition();
            }

            HealthDown();
    
        }
    }

    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0,0, 0.2f);
        }
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.2f);
            player.OnDie();
            Debug.Log("Á×À½");
            RestartButton.SetActive(true);

        }
    }

   void PlayerReposition()
    {
        player.VelocityZero();
        player.transform.position = new Vector3(1, 1, -1);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }    
}

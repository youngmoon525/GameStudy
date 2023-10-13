using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
   

    public void onGameStart()
    {
        Debug.Log("T");
        SceneManager.LoadScene("2DPlatt");
    }



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("aaa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

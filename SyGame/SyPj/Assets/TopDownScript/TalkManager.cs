using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;
     void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:0" , "이 곳에 처음 왔구나?:1" });
        talkData.Add(100, new string[] { "박스가 있네요?" });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);

    }
    public string GetTalk(int id , int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
        return talkData[id][talkIndex];
    }
    public Sprite GetPortrait(int id , int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}

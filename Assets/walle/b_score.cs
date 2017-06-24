using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class b_score : MonoBehaviour {
    public int bscore=50;              //初始分数
    public int eve_score = 5;           //每次增减的分数
    public Text testui;
    void Start()
    {

    }
    public void add_score()             //得分方法
    {
        bscore += eve_score;
    }

    public void sub_score()
    {
        bscore -= eve_score;
    }
    void Update()
    {
       // testui.text = bscore.ToString();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class w_score : MonoBehaviour {
    public float wscore = 50;              //初始分数
    public int eve_score = 5;             //每次增减的分数
    public Text testui;
    //public GameObject t;
    void Start()
    {

    }
    public void add_score()               //得分方法
    {
        wscore += eve_score;
    }

    public void sub_score()
    {
        wscore -= eve_score;
    }
    void Update()
    {
        wscore -= Time.deltaTime;
        if(wscore<=0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 3);
        }
       // testui.text = wscore.ToString();
    }

}

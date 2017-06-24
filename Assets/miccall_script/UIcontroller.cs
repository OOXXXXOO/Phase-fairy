using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour {

    private GameObject top;
    private GameObject score;
    private GameObject bottom;
    private GameObject StartButton; 
    private GameObject target_top;
    private GameObject target_left;
    private GameObject target_right;
    private GameObject target_bottom;
    private GameObject back_bg;
    private GameObject button_bg;

    private Vector3 top_pos;
    private Vector3 button_bg_pos;
    private Vector3 score_pos;
    private Vector3 bottom_pos;
    private Vector3 StartButton_pos;
    private Vector3 target_top_pos;
    private Vector3 target_left_pos;
    private Vector3 target_right_pos;
    private Vector3 target_bottom_pos;



    public Canvas text;
    public float movespeed = 1500f ;
    public float path_time = 1f;
    public float duration = 1000F;
    private float startTime;
    private bool isstart = false;
    float step;
    float a;
    Color current_color;
    // Use this for initialization
    void Start () {

        FindUI();

        initUI();

        initposition();

        startTime = Time.time;
        setColor0();
        //Debug.Log("top"+top.transform.localPosition);
    }
	
    void setColor255()
    {
        current_color = new Color(1, 1, 1, 1);
    }

    void setColor0()
    {
        current_color = new Color(1,1,1,0);
    }
	// Update is called once per frame
	void Update () {
        step  = movespeed * Time.deltaTime ;

        if (isstart == true)
        {
            
            ITween(top, target_top.transform.localPosition, path_time);
            ITween(score, target_top.transform.localPosition, path_time);
            ITween(bottom, target_bottom.transform.localPosition, path_time);

            current_color.a += -1f * Time.deltaTime;
            
            StartButton.GetComponent<Image>().color = current_color;
            back_bg.GetComponent<Image>().color = current_color;
            button_bg.GetComponent<Image>().color = current_color;

        }
        else
        {

            ITween_by(top, top_pos, path_time);
            ITween_by(score, score_pos, path_time);
            ITween_by(bottom, bottom_pos, path_time);

            current_color.a += 1f * Time.deltaTime;
            StartButton.GetComponent<Image>().color = current_color;
            back_bg.GetComponent<Image>().color = current_color;
            button_bg.GetComponent<Image>().color = current_color;

        }
	}

    public void onstartclick()
    {
        isstart = true;
        setColor255();
    }


    void initposition()
    {
         top_pos = top.transform.localPosition ;
         score_pos = score.transform.localPosition;
         bottom_pos = bottom.transform.localPosition;
          StartButton_pos = StartButton.transform.localPosition;
          target_top_pos = target_top.transform.localPosition;
          target_left_pos = target_left.transform.localPosition;
          target_right_pos = target_right.transform.localPosition;
          target_bottom_pos = target_bottom.transform.localPosition;
        button_bg_pos = button_bg.transform.localPosition;
}


    public void ongameend()
    {
        isstart = false;
        setColor0();
    }


    private void FindUI()
    {
        top = GameObject.Find("top");
        score = GameObject.Find("score");
        bottom = GameObject.Find("bottom");
        target_top = GameObject.Find("target_top");
        target_left = GameObject.Find("target_left");
        target_right = GameObject.Find("target_right");
        target_bottom = GameObject.Find("target_bottom");
        StartButton = GameObject.Find("StartButton");
        back_bg = GameObject.Find("backgroud");
        button_bg = GameObject.Find("Image");

    }


    private void  initUI()
    {
        
    }

    private void ITween(GameObject target, Vector3 position, float time)
    {
        Hashtable hash = new Hashtable();
        hash.Add("position", position);
        hash.Add("islocal", true);
        hash.Add("time", time);
        hash.Add("easetype", iTween.EaseType.easeInCirc);

        //让模型开始寻路	
        iTween.MoveTo(target, hash);
    }
    private void ITween_by(GameObject target, Vector3 position, float time)
    {
        Hashtable hash = new Hashtable();
        hash.Add("position", position);
        hash.Add("islocal", true);
        hash.Add("time", time);
        hash.Add("easetype", iTween.EaseType.easeInCubic);
        iTween.MoveTo(target, hash);
        
    }


    private void FadeTo(GameObject target, float alpha, float time)
    {
        Hashtable hash = new Hashtable();
        hash.Add("alpha", alpha);
        hash.Add("time", time);
        hash.Add("amount",alpha);
        hash.Add("a", alpha);
        iTween.FadeTo(target,hash);
        iTween.ColorTo(target, hash);
    }

}

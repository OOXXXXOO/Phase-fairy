using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine.SceneManagement;

public class b_move : MonoBehaviour {


    public float max_speed = 20;                                      //最大速度
    public float jia_speed = 1;                                      //加速度倍率
    public float die_height = 14;                                    //死亡高度
    private float v_jia_speed = 5;
    private bool ischanged = false;
    private bool isdead = false;
    public GameObject die;
    // private Vector3 v_jia_speed = new Vector3(0, 10, 0);
    public void Changedirection()
    {
        ischanged = true;
        v_jia_speed = -v_jia_speed;
    }

    public void End_game()
    {
        if(Mathf.Abs( this.GetComponent<Transform>().position.y)>die_height)
        {

            //DontDestroyOnLoad(this);
            //this.gameObject.SetActive(false);
            Destroy(gameObject, 3);
            //Application.LoadLevel(0);
            //Invoke("reStart", 2);
        }

    }

    private void OnDestroy()
    {
        //Application.LoadLevel(0);
        SceneManager.LoadScene(0);
    }



    void Update()
    {

        GetComponent<Rigidbody>().velocity -= new Vector3(0, v_jia_speed * jia_speed, 0) * Time.deltaTime;
        float y_speed = GetComponent<Rigidbody>().velocity.y;
        if (Mathf.Abs(y_speed) > max_speed)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, y_speed / Mathf.Abs(y_speed) * max_speed, 0);
        }

        End_game();

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            if (!isdead)
            {
                Changedirection();
            }
        }
        if (Mathf.Abs(this.GetComponent<Transform>().position.y) > 1)
        {
            isdead = true;
            die.SetActive(true);
            this.gameObject.GetComponent<AudioSource>().enabled = true;
        }
    }
}

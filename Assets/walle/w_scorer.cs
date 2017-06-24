using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w_scorer : MonoBehaviour {

    public float trigger_scale = 1;                         //触发区域倍率
    void Start()
    {
        this.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f) * trigger_scale;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "white")
        {
            GetComponent<w_score>().add_score();
            other.GetComponent<tag_destroy>().destroy_tag();
            //print("w  吃");
        }
        if (other.tag == "black")
        {
            GetComponent<w_score>().sub_score();
            other.GetComponent<tag_destroy>().destroy_tag();
            // print("w  吃错了");
        }

    }
}

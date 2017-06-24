using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b_scorer : MonoBehaviour {
    public float trigger_scale = 1;
    void Start()
    {
        this.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f) * trigger_scale;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="black")
        {
            this.GetComponent<b_score>().add_score();
            other.GetComponent<tag_destroy>().destroy_tag();
            //print("b  吃");
        }
        if (other.tag == "white")
        {
            this.GetComponent<b_score>().sub_score();
            other.GetComponent<tag_destroy>().destroy_tag();
            //print("b  吃错了");
        }

    }

}

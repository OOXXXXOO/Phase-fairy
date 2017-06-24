using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moster : MonoBehaviour {
    public GameObject letf;
    public GameObject left_copy;
    public float Rotation_speed;
    float total_time;
    public GameObject upper;
    public GameObject lower;
    bool drop = false;
    public float tree_height;
    public GameObject centre;
    public float OMEGA, A, Phi;


    public GameObject uppermodel;
    public GameObject lowermodeol;


    float YCOS(float y, float omega, float t, float A, float Phi)
    {
        return A * Mathf.Cos(omega * t + Phi);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        total_time += Time.deltaTime;
        //Debug.Log("TOTAL Time is" + total_time);
        if (upper == null && lower == null)//just one time upper andlower is null
        {
            if (Input.GetKey(KeyCode.Space))//if init
            {
                drop = true;
                if (drop)
                {
                    //up init
                    Vector3 upperposition = new Vector3(letf.transform.position.x, tree_height, letf.transform.position.z);
                    upper = GameObject.Instantiate(letf, upperposition, letf.transform.rotation);

                    //mid init
                    left_copy = GameObject.Instantiate(letf, letf.transform.position, letf.transform.rotation);

                    //low init
                    Vector3 lowerposition= new Vector3(letf.transform.position.x, -tree_height, letf.transform.position.z);
                    lower = GameObject.Instantiate(letf, lowerposition, letf.transform.rotation);
                    //drop = false;
                }

            }
        }
        else
        {
            //update lower& upper
            left_copy.transform.RotateAround(centre.transform.position,Vector3.up, Rotation_speed * Time.deltaTime);
           
            upper.transform.position = new Vector3(left_copy.transform.position.x,
                YCOS(left_copy.transform.position.y, OMEGA, total_time, A, Phi),
                left_copy.transform.position.z);

            lower.transform.position = new Vector3(left_copy.transform.position.x,
                -YCOS(left_copy.transform.position.y, OMEGA, total_time, A, Phi),
                left_copy.transform.position.z);
            if (upper.transform.position.y == 0 && lower.transform.position.y == 0)
            {
                //destory
            }


        }

		
	}
}

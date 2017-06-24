using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_plane : MonoBehaviour {
    public GameObject tree;
    public float rotation_velocity=1.0f;
    public float move_velocity = 1.0f; 
    public bool start = false;
   // public GameObject partical;
    public float Total_Time = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            Total_Time = Total_Time + Time.deltaTime;
           // Debug.Log("Total time=" + Total_Time);
            tree.transform.Rotate(Vector3.up*rotation_velocity*Time.deltaTime);
         //   tree.transform.rotation = new Quaternion(0, Time.deltaTime*rotation_velocity, 0,1);    
        /*
            if (Input.GetKey(KeyCode.S))
            { partical.transform.Translate(Vector3.down); }
            if(Input.GetKey(KeyCode.W))
            { partical.transform.Translate(Vector3.up); 
            }
            */
        
    }
	}
}

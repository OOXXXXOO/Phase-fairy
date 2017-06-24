using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateposition : MonoBehaviour {




    public GameObject upper;
    public GameObject lower;


    public GameObject uppermodel;
    public GameObject lowermodel;



    public float tree_height;
    public GameObject centre;
    public float Rotation_speed;

    public GameObject letf;
    // Use this for initialization
     public float OMEGA, A, Phi;
    float total_time;

    int mark = 0;
    bool init_or_not = false;

    float YCOS(float y, float omega, float t, float A, float Phi)
    {
        return A * Mathf.Cos(omega * t + Phi);
    }




    void update_position(GameObject letf, GameObject upper, GameObject lower)
    {
        //update lower& upper
        if (upper != null && lower != null)
        {
            letf.transform.RotateAround(centre.transform.position, Vector3.up, Rotation_speed * Time.deltaTime);

            upper.transform.position = new Vector3(letf.transform.position.x,
                YCOS(letf.transform.position.y, OMEGA, total_time, A, Phi),
                letf.transform.position.z);

            lower.transform.position = new Vector3(letf.transform.position.x,
                -YCOS(letf.transform.position.y, OMEGA, total_time, A, Phi),
                letf.transform.position.z);
        }

    }

    void initupper()
    {
        Vector3 upperposition = new Vector3(letf.transform.position.x, tree_height, letf.transform.position.z);
        upper = GameObject.Instantiate(uppermodel, upperposition, letf.transform.rotation);
    }
    void initlower()
    {
        Vector3 lowerposition = new Vector3(letf.transform.position.x, -tree_height, letf.transform.position.z);
        lower = GameObject.Instantiate(lowermodel, lowerposition, letf.transform.rotation);
    } 


    void Start () {
   
       
   
            //mid init
            letf = this.transform.gameObject;
           // letf.transform.rotation = new Quaternion;

        //Debug.Break();
        
        if (letf.gameObject.name == "initposition(Clone)" &&upper==null&&lower==null&&!init_or_not)
        {

            //up init
            initupper();


            //low init
            initlower();


            //drop = false;
            init_or_not = true;
            
        }
    }

    // Update is called once per frame
    void Update () {
        total_time += Time.deltaTime;

        update_position(letf, upper, lower);

          
        }   

    
}

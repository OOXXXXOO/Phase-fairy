using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour {

    public float radius = 5f;
    public float speed = 0f;
    public float maxspeed = 200f;
    public float acceleration = 400f ;
    /// <summary>  
    /// 中心箭头  
    /// </summary>  
    public GameObject centerObj;
    /// <summary>  
    /// 消息图片对象  
    /// </summary>  
    public GameObject roateObj;
    /// <summary>  
    /// 四元数  
    /// </summary>  
    Quaternion qua;
    // Use this for initialization
    void Start () {
        roateObj = transform.gameObject;

    }
   

    // Update is called once per frame
    void Update () {
        if(speed <= maxspeed )
        {
            speed += acceleration * Time.deltaTime;
        }
        
        roateObj.transform.RotateAround(centerObj.transform.position, new Vector3(0, 100, 0), speed*Time.deltaTime);
       // roateObj.transform.Rotate(centerObj.transform.position,speed*Time.deltaTime);
        
        qua = roateObj.transform.rotation;
        qua.z = 0;
        roateObj.transform.rotation = qua;
        
    }
}

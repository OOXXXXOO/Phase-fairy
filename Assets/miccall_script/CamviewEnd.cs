using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamviewEnd : MonoBehaviour {

    public Camera mainCAM;
    public float minsize;
    public float maxsize;
    public float speed;

    public float duration = 1000F;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime) / duration;
        mainCAM.fieldOfView = Mathf.SmoothStep(minsize, maxsize, t);
    }


    public void resetCam()
    {
        mainCAM.fieldOfView = Mathf.Lerp(minsize, maxsize, speed);
    }

    private void OnEnable()
    {
        startTime = Time.time;
        Update();
    }
}

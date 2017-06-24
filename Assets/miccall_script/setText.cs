using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setText : MonoBehaviour {

    public TextAsset TxtFile;
    private string Mytxt;

    public Text text;
    void Start()
    {
        //TxtFile = Resources.Load("MyTest") as TextAsset;
        Mytxt = TxtFile.text;
        string[] sArray = Mytxt.Split( '_' );

        int a = GenerateRandomNumber(sArray.Length);
        text.text = sArray[a];

    }

	// Update is called once per frame
	void Update () {
		
	}

    public int GenerateRandomNumber(int Length)
    {
        int iSeed = Length;
        System.Random ro = new System.Random(Length);
        long tick = DateTime.Now.Ticks;
        System.Random ran = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        int iResult;
        int iUp = Length;
        int iDown = 0;
        iResult = ro.Next(iDown, iUp);
        return iResult;

    }
}

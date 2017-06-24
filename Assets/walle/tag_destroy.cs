using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tag_destroy : MonoBehaviour {
    private float time = 0f;
    public float diren_lifetime = 10;
    public void destroy_tag()
    {
        gameObject.SetActive(false);
        DestroyImmediate(this.gameObject);
    }
	void Start () {
		
	}

    void Update()
    {
        time += Time.deltaTime;
        if(time>diren_lifetime)
        {
            destroy_tag();
        }
    }
}

using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    void OnMouseDown()
    {
         GameObject.FindGameObjectWithTag("manager").transform.Find("grayMask").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("HUD").transform.Find("treasure").gameObject.SetActive(true);
        //transform.FindGameObjectWithTag("treasure").SetActive(true);
    }

    // Update is called once per frame
    void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class CheckKey : MonoBehaviour {

    public GameObject setting;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setting.SetActive(true);
        }
    }
}

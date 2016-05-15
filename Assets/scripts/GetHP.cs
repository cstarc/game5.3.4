using UnityEngine;
using System.Collections;

public class GetHP : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    void OnMouseDown()
    {

        GameObject.FindGameObjectWithTag("hero").GetComponent<Hero>().addHP(10);
        Destroy(this.gameObject);
    }
        // Update is called once per frame
    void Update () {
	
	}
}

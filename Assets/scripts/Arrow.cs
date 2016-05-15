using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    public Animator animator;
	// Use this for initialization
	void Start () {
	
	}
    void OnMouseDown()
    {
        animator.SetBool("click",true);
    }
    // Update is called once per frame
    void Update () {
	
	}
}

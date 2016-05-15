using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

    //open
    public GameObject doorOpen;
    GameObject door;
    private Animator animator;
    AnimatorStateInfo info;
    // Use this for initialization
    void Start () {
        door = Instantiate(doorOpen);
        animator = door.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (animator != null)
        {
            // 判断动画是否播放完成
            info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0f)
            {
                Destroy(door);
                animator = null;
            }

        }
    }
}

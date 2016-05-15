using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToChooseHero : MonoBehaviour {

    //close
    public GameObject doorClose;
    private Animator animator1;

    //open
    public GameObject doorOpen;
    GameObject door;
    private Animator animator2;

    AnimatorStateInfo info;

    // Use this for initialization
    void Start () {
        animator1 = null;
        door = Instantiate(doorOpen);
        animator2 = door.GetComponent<Animator>();
    }

  
    void OnMouseDown()
    {
        animator1 = Instantiate(doorClose).GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        if (animator1 != null)
        {
            // 判断动画是否播放完成
            info = animator1.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0f)
            {
                SceneManager.LoadScene("character");
            }

        }
        if (animator2 != null)
        {
            // 判断动画是否播放完成
            info = animator2.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0f)
            {
                Destroy(door);
                animator2 = null;
            }

        }
    }
}

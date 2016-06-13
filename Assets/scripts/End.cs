using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {
    public GameObject doorClose;
    private Animator animator;
    AnimatorStateInfo info;
    // Use this for initialization
    void Start()
    {

    }

    public void restart()
    {
        Hero.readDateByHeroName(ChoicManager.heroChoosed);
        BagManager.clear();
        Manager.clear();
        animator = Instantiate(doorClose).GetComponent<Animator>();
        //animator = Instantiate(doorClose).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            // 判断动画是否播放完成
            info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0f)
            {
                SceneManager.LoadScene("choic");
            }

        }
    }
}

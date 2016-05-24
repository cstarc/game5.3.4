using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    /*public GameObject doorClose;
    private Animator animator;
    AnimatorStateInfo info;*/
    // Use this for initialization
    void Start () {
        //animator = Instantiate(doorClose).GetComponent<Animator>();
    }

    public void menuStart()   //开始
    {
        Hero.readDateByHeroName(ChoicManager.heroChoosed);
        //animator = Instantiate(doorClose).GetComponent<Animator>();
        SceneManager.LoadScene("choic");
    }

    public void menuContinue()   //继续
    {
        //animator = Instantiate(doorClose).GetComponent<Animator>();
        Hero.readDate();
        SceneManager.LoadScene("scene");
    }
    public void menuExit()   //结束退出
    {
        //animator = Instantiate(doorClose).GetComponent<Animator>();
        Application.Quit();
    }

    // Update is called once per frame
    void Update () {
        /*if (animator != null)
        {
            // 判断动画是否播放完成
            info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0f)
            {
                SceneManager.LoadScene("scene");
            }

        }*/
    }
}

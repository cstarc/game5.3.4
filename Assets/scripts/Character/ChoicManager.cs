using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ChoicManager : MonoBehaviour {
    public ChooseCharacter[] hero;
    public Sprite choosed;
    public Sprite unChoosed;

    //doorClose
    public GameObject doorCLose;
    private Animator animator;
    AnimatorStateInfo info;

    public static string heroChoosed = "卡罗尔";
    string tempName;

    // Use this for initialization
    void Start () {
        animator = null;

    }

    public void chooseHero(string name)
    {
        tempName = name;


        //使被选中英雄显示选中状态
        for (int i = 0; i < hero.Length; i++)
        {
            if (string.Equals(hero[i].heroName, name))
            {
                Debug.Log("ok");
                hero[i].gameObject.GetComponent<SpriteRenderer>().sprite = choosed;
            }
            else
                hero[i].gameObject.GetComponent<SpriteRenderer>().sprite = unChoosed;
        }
    }

    public void clickOk()
    {
        heroChoosed = tempName;
        animator = Instantiate(doorCLose).GetComponent<Animator>();
       
    }

    public void ClickCancel()
    {
        animator = Instantiate(doorCLose).GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
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


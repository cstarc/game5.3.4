using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    public Hero hero;
    public string sceneName;
    public GameObject doorClose;
    private Animator animator;
    AnimatorStateInfo info;
    // Use this for initialization
    void Start () {
        animator = null;
    }
    void OnMouseDown()
    {
        animator=Instantiate(doorClose).GetComponent<Animator>();
        if(string.Equals(sceneName,"scene"))
            hero.saveDate();
       
    }
	// Update is called once per frame
	void Update () {
        if (animator!=null)
        {
            // 判断动画是否播放完成
            info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0f)
            {
                SceneManager.LoadScene(sceneName);
            }
           
        }
    }
}

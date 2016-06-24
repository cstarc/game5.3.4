using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnUp : MonoBehaviour
{

    //public Texture2D Fog;
    bool isblack;
    public int[] near;
    private int number = 7; //行列格子数
   // private GameObject smoke;
    // Use this for initialization
    void Awake()
    {
        isblack = false;
        calculateNear();//计算near
       // smoke = Resources.Load("smoke") as GameObject;
    }

    void calculateNear()   //计算并记录附近地板位置
    {
        int length = near.Length;
        int index = near[0];
        switch (length)
        {
            case 3:
                {
                    if (index == 0)
                    {
                        near[1] = 1;
                        near[2] = number + index;
                    }
                    else
                    {
                        if (index == (number - 1))
                        {

                            near[1] = index - 1;
                            near[2] = number + index;
                        }
                        else
                        {
                            if (index == (number * number - 1))
                            {
                                near[1] = index - number;
                                near[2] = index - 1;
                            }
                            else
                            {
                                near[1] = index - number;
                                near[2] = index + 1;
                            }
                        }

                    }

                }
                break;
            case 4:
                {
                    if (index < number)
                    {
                        near[1] = index - 1;
                        near[2] = index + 1;
                        near[3] = number + index;
                    }
                    else
                    {
                        if (index % number == 0)
                        {
                            near[1] = index - number;
                            near[2] = index + 1;
                            near[3] = number + index;
                        }
                        else
                        {
                            if (index % number == number-1)
                            {
                                near[1] = index - number;
                                near[2] = index - 1;
                                near[3] = number + index;
                            }
                            else
                            {
                                near[1] = index - number;
                                near[2] = index - 1;
                                near[3] = 1 + index;
                            }
                        }
                    }
                }
                break;
            case 5:
                {
                    near[1] = index - number;
                    near[2] = index - 1;
                    near[3] = 1 + index;
                    near[4] = index + number;
                }
                break;
        }

    }


    public void initFog()
    {
         Fog fog = gameObject.GetComponentInParent<Fog>();
        //Fog fog = GameObject.FindGameObjectWithTag("manager").GetComponentInChildren<Fog>();
        fog.deleteFog(near, isblack);
    }
    void OnMouseDown()
    {
        Fog fog = gameObject.GetComponentInParent<Fog>();
        if (!fog.canDeleteFog(near[0]))  //点在black or white
            return;

        //可探索
        // **判断是否不可前进即不可探索此块周边**
        //
        Manager manager = gameObject.GetComponentInParent<Manager>();
        GameObject obj = manager.randomObject(transform, near,ref isblack);

        fog.deleteFog(near, isblack);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

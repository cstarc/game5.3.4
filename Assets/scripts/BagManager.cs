using UnityEngine;
using System.Collections;

public class BagManager : MonoBehaviour {
    public SpriteRenderer spriteRenderer;   //用来打开关闭背包效果
    public Sprite[] sprite;         //打开关闭背包效果
    public SpriteRenderer [] cellSprites;       //储物格子
    public static bool[]cell= new bool[12];
    bool active;
	// Use this for initialization
	void AWake () {
      
        active = false;
        //Debug.Log(""+cell[1]);
    }


    /// <summary>
    /// 打开关闭背包
    /// </summary>
    public void openBag()
    {
        if (active == false)
        {
            gameObject.SetActive(true);
            active = true;
            spriteRenderer.sprite = sprite[0];
        }
        else
        {
            gameObject.SetActive(false);
            active = false;
            spriteRenderer.sprite = sprite[1];
        }
    }

    /// <summary>
    /// 获得装备
    /// </summary>
    /// <param name="sprite"> 装备图</param>
    public void addEquipment(Sprite sprite)
    {
       
        for (int i = 0; i < 12; i++)
        {
           
            if (cell[i] == false)
            {
               
                // GameObject.FindGameObjectWithTag("CELL").transform.Find("cell" + i).GetComponent<SpriteRenderer>().sprite = sprite;
                //Sprite s= transform.Find("cell" + i).gameObject.GetComponent<SpriteRenderer>().sprite;
                cellSprites[i].sprite = sprite;
                cell[i] = true;
                break;
            }

        }
    }


 
    
                // Update is called once per frame
                void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    int[] map;
    public static int level=1;
    public int rows;
    public int cols;
    public SpriteRenderer[] floor;
    public Sprite[] floorTex;
    public GameObject[] obj;
    public GameObject smoke;
    public GameObject entry;

    public GameObject[] monster;    
   // public GameObject[] elite;          //精英怪
   // public GameObject[] boss;           //boss 
   // public GameObject[] others;


    int num;    //the number of floors 
    int entryIdex; // 出口位置
   // public Camera c;
    // Use this for initialization
    void Start() {
        num = floorTex.Length;     
        initMap();
       // Debug.Log(" c:"+c.aspect);
    }

    //initilize map
    void initMap()
    {
        //设置floor纹理
        map = new int[rows * cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                map[i * cols + j] = Random.Range(0, num);
                floor[i * cols + j].sprite = floorTex[map[i * cols + j]];
            }

        //设定开始位置
        int first = Random.Range(0, floor.Length);   
        floor[first].GetComponent<TurnUp>().initFog();
        
        //确定出口位置
        for (;;)
        {
            entryIdex = Random.Range(0, floor.Length);
            if (entryIdex != first)
                break;
        }
        //Debug.Log("entryIdex" + entryIdex);
    }

    //实例化并返回随机物体如怪物，血药等  for Turnup
    public GameObject randomObject(Transform t,int[] near,ref bool isBlock)
    {
        int loc = near[0];
        GameObject others;
        //不同级别 不同怪物道具
        switch (level)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                {
                    //先判断是否为出口，不为在随机返回物体
                    if (loc == entryIdex)
                    {            
                        others = Instantiate(entry, t.position, t.rotation) as GameObject;
                        others.SetActive(false);
                        others.SetActive(true);
                        others.transform.parent = GameObject.FindGameObjectWithTag("manager").transform;  //挂在manager下
                        return others;
                    }
                    int r = Random.Range(1, 11);
                    if(r<=5)
                        return Instantiate(smoke, t.position, t.rotation) as GameObject;
                    else
                    {//划分怪物与物品等
                        if (r <= 7)  //为怪物
                        {
                            //isBlock = true;
                            others = Instantiate(monster[Random.Range(0, monster.Length)], t.position, t.rotation) as GameObject;
                            others.transform.parent = GameObject.FindGameObjectWithTag("manager").transform;  //挂在manager下，为了显示怪物属性

                            //创建阻挡区

                            return others;
                        }
                        else   //为物品
                        {
                            int index = Random.Range(0, obj.Length);
                            if(index==2)
                                isBlock = true;
                            others = Instantiate(obj[index], t.position, t.rotation) as GameObject;
                            others.transform.parent = GameObject.FindGameObjectWithTag("manager").transform;  //挂在manager下，为了显示怪物属性
                            return others;
                        }
                    }
                }
               
            default: return null;
        }
    }
   /* public GameObject randomObject(int loc)
    {
        //不同级别 不同怪物道具
        switch (level)   
        {
            case 1:
                {
                    if (loc == entryIdex)
                        return entry;
                    return obj[Random.Range(1, obj.Length)];
                }
            default: return null;
        }
    }*/

	// Update is called once per frame
	void Update () {
	
	}
}

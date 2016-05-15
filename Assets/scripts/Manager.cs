using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    int[] map;
    public int rows;
    public int cols;
    public SpriteRenderer[] floor;
    public Sprite[] floorTex;
    public GameObject[] obj;

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
    }


    public GameObject randomObject(int loc)
    {
        if (loc == entryIdex)
            return obj[0];
        return obj[Random.Range(1,obj.Length)];
    }

	// Update is called once per frame
	void Update () {
	
	}
}

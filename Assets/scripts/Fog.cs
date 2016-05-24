using UnityEngine;
using System.Collections;

public class Fog : MonoBehaviour {
    public Texture2D fog;
    Texture2D FogOfWar;
    public Material mater;
    Color[] fullColor;
    Color[] halfColor;
    Color[] color;
    int width;
    int height;
    int rows = 7;
    int cols = 7;
    int[] fogTable;  //mark fog state   0 black  1 halfClear  2 fullClear

    void Awake()
    {
        width = Mathf.FloorToInt(fog.width / cols);
        height = Mathf.FloorToInt(fog.height / rows);

        //初始化半雾无雾透明度
        fullColor = new Color[width * height];
        for (int index = 0; index < width * height; index++)
            fullColor[index] = new Color(1, 1, 1, 1);

        halfColor = new Color[width * height];
        for (int index = 0; index < width * height; index++)
            halfColor[index] = new Color(1, 1, 1, 0.5f);

        initalize();
    }
    // Use this for initialization
    void Start () {




    }

    public void initalize()
    {
        //实例化以防修改源资源 ???
        FogOfWar = Instantiate(fog);
        mater.SetTexture("_Mask", FogOfWar);


        fogTable = new int[rows * cols];
        for (int index = 0; index < fogTable.Length; index++)
            fogTable[index] = 0;

 
    }
    //判断是否可去雾
    public bool canDeleteFog(int p)
    {
        if (fogTable[p] == 1)  ////==1
            return true;
        return false;
    }

    //去雾
    public void deleteFog(int []pos,bool isBlock)   //isblock  是否有阻挡物体
    {

        int length = pos.Length;
        int x;
        int y;
        int col;
        int row;
        if (isBlock)
            length = 1;

        for (int index = 0; index < length; index++)
        {
            if (index == 0)     //点击位置去掉全部雾
            {
                color = fullColor;
                fogTable[pos[index]] = 2;     
            }
            else
            {
                if (fogTable[pos[index]] != 0)      //fog alreadly had been clear
                    continue;
                color = halfColor;
                fogTable[pos[index]] = 1;
            }

            row = (pos[index] / rows);
            col = pos[index] % rows;
            x = col * width;
            y = FogOfWar.height - (row + 1) * height;
            FogOfWar.SetPixels(x, y, width, height, color);

        }

        FogOfWar.Apply();

    }
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;

public class BagManager : MonoBehaviour {
    public SpriteRenderer spriteRenderer;   //用来打开关闭背包效果
    public Sprite[] sprite;         //打开关闭背包效果
    public SpriteRenderer[] cellSprites;       //储物格子
    public SpriteRenderer[] usedEquipmet;    //穿戴装备
    public static bool[] cell = new bool[12];     //储物格存放状态
    public static string[] names = new string[7];   //存放已装备物品名字
    public static Dictionary<int, string> information = new Dictionary<int, string>();  //储物格中装备信息
    public Text wearedProperty; //已装备属性
    public Text curProperty;   //待装备属性
    static bool isNew = true;
    bool active;   //背包是否打开
    int currentEquipment; //当前选中装备
    GameObject floor;
    static string[] wearLocation = { "weapon", "shield", "clothers", "hat", "glove", "ring", "shoe" };
    // Use this for initialization
    void Awake() {

        active = false;
        //Debug.Log(""+cell[1]);
        if (!isNew)
        {

            for (int index = 0; index < names.Length; index++)
            {//wear
                if (names[index] != null && !names[index].Equals(""))
                {

                    //usedEquipmet[index].sprite = Resources.Load<Sprite>("sprite/" + names[index]);// LoadAll<Sprite>("sprite/equipment");
                    Sprite[] equip = Resources.LoadAll<Sprite>("sprite/equipment");
                    foreach (Sprite s in equip)
                        if (s.name.Equals(names[index]))
                            usedEquipmet[index].sprite = s;
                    //readInformation
                    Debug.Log("usedEquipmet:" + index);
                    usedEquipmet[index].gameObject.GetComponent<Equipment>().readEquipmentInf();
                }
            }

            foreach (KeyValuePair<int, string> kvp in information)
            {//goods
                Debug.Log("isNew1");
                Sprite[] equip = Resources.LoadAll<Sprite>("sprite/equipment");
                foreach (Sprite s in equip)
                    if (s.name.Equals(kvp.Value))
                        cellSprites[kvp.Key].sprite = s;
                //cellSprites[kvp.Key].sprite = Resources.Load<Sprite>("sprite/" + kvp.Value);
                //readInformation
                cellSprites[kvp.Key].gameObject.GetComponent<Equipment>().readEquipmentInf();
            }
        }
    }


    public static void clear()
    {
        cell= new bool[12];
        names=new string[7];
        information.Clear();
        isNew = true;
    }
    void Start()
    {
        Debug.Log("start");

    }

    public void dropEquipment()
    {
        if (currentEquipment >= 0)
        {
            cellSprites[currentEquipment].sprite = null;
            cell[currentEquipment] = false;
            information.Remove(currentEquipment);
            cellSprites[currentEquipment].gameObject.GetComponent<Equipment>().clear();
        }
        else
        {
            int index = currentEquipment * (-1) - 1;
            usedEquipmet[index].sprite = null;
            names[index] = "";
            usedEquipmet[index].gameObject.GetComponent<Equipment>().clear();

        }
        hideProperty();
    }
    /// <summary>
    /// 隐藏装备属性面板
    /// </summary>
    public void hideProperty()
    {
        wearedProperty.gameObject.transform.parent.gameObject.SetActive(false);
        curProperty.gameObject.transform.parent.gameObject.SetActive(false);
    }
    /// <summary>
    /// 点击已装备的装备显示此装备信息
    /// </summary>
    /// <param name="dic"></param>
    public void showProperty(Dictionary<string, int> dic,int location)
    {
        currentEquipment = location;  ////储物当前显示属性的装备位置
        hideProperty();
        wearedProperty.gameObject.transform.parent.gameObject.SetActive(true);
        string text="";
        foreach (KeyValuePair<string, int> kvp in dic)
        {
            if(kvp.Value!=0)
               text += (kvp.Key + ":" + kvp.Value+"\n");
        }
        wearedProperty.text = text;
    }


  
    /// <summary>
    /// 点击储物格装备显示已装备信息与当前装备信息
    /// </summary>
    /// <param name="wear"></param>
    /// <param name="cur"></param>
    public void showProperty(Dictionary<string, int> wear, Dictionary<string, int> cur,int location)
    {
        currentEquipment = location;  //储物当前显示属性的装备位置
        hideProperty();
        string text = "";
        if (wear.Count > 0)
        {
            wearedProperty.gameObject.transform.parent.gameObject.SetActive(true);
            foreach (KeyValuePair<string, int> kvp in wear)
            {
                if (kvp.Value != 0)
                    text += (kvp.Key + ":" + kvp.Value + "\n");
            }
            wearedProperty.text = text;
        }
        curProperty.gameObject.transform.parent.gameObject.SetActive(true);

 

        text = "";
        foreach (KeyValuePair<string, int> kvp in cur)
        {
            if (kvp.Value != 0)
                text += (kvp.Key + ":" + kvp.Value + "\n");
        }
        curProperty.text = text;
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
            floor = GameObject.FindGameObjectWithTag("manager");
            if(floor!=null)
              floor.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            active = false;
            spriteRenderer.sprite = sprite[1];
            if (floor != null)
                floor.SetActive(true);
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

                //设置图片并获取装备属性等信息
                cellSprites[i].sprite = sprite;
                cellSprites[i].gameObject.GetComponent<Equipment>().readEquipmentInf();

                cell[i] = true; //表储物格被使用

                information.Add(i, sprite.name);
                break;
            }

        }
    }


    public static void save()
    {
        //
        string appDBPath = Application.dataPath + "/mainDate.db";

        //--------------------------
        isNew = false;

        DbAccess db = new DbAccess(@"Data Source=" + appDBPath);

        string[] usedName = new string[7];
        for (int index = 0; index < names.Length; index++)
        {
            if (names[index]==null)
                usedName[index] ="''" ;
            else
                usedName[index] = "'" + names[index] + "'";
        }
        //保存已穿装备信息
        db.UpdateInto("usedEquipment", new string[] { "weapon", "shield", "clothers", "hat", "glove", "ring", "shoe" },
            new string[] { usedName[0], usedName[1], usedName[2],usedName[3],usedName[4],usedName[5],usedName[6]}, "rowid", "1");
        //保存物品栏中装备信息
        db.DeleteContents("unusedEquipment");
        foreach (KeyValuePair<int, string> kvp in information)
        {
            db.InsertInto("unusedEquipment", new string[] { kvp.Key.ToString(), "'" + kvp.Value + "'" });
        }

        db.CloseSqlConnection();

    }


    public static void read()
    {

        string appDBPath = Application.dataPath + "/mainDate.db";
        //--------------------------

        isNew = false;
        
        DbAccess db = new DbAccess("Data Source=" + appDBPath);

        //
        using (SqliteDataReader sqReader = db.ExecuteQuery("SELECT * FROM usedEquipment WHERE rowid=1"))
        {
            while (sqReader.Read())
            {
                for (int index = 0; index < names.Length; index++)
                {
                    names[index] = sqReader.GetString(sqReader.GetOrdinal(wearLocation[index]));

                    //string name = sqReader.GetString(sqReader.GetOrdinal(wearLocation[index]));
                    /* if (name != null)
                     *{
                      *   usedEquipmet[index].sprite = Resources.Load<Sprite>("sprite/" + name);// LoadAll<Sprite>("sprite/equipment");

                      *   //readInformation
                       *  usedEquipmet[index].gameObject.GetComponent<Equipment>().readEquipmentInf();
                       */

                }

            }


        }
        using (SqliteDataReader sqReader = db.ExecuteQuery("SELECT * FROM unusedEquipment"))
        {
            while (sqReader.Read())
            {

                int loc= sqReader.GetInt32(sqReader.GetOrdinal("location"));
                string name= sqReader.GetString(sqReader.GetOrdinal("name"));
                cell[loc] = true;
                information.Add(loc, name);
                //cellSprites[loc].sprite = Resources.Load<Sprite>("sprite/" + name);
                //readInformation
                // cellSprites[loc].gameObject.GetComponent<Equipment>().readEquipmentInf();

            }
        }
        db.CloseSqlConnection();
    }
    // Update is called once per frame
    void Update () {
	
	}
}

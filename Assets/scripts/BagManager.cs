using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class BagManager : MonoBehaviour {
    public SpriteRenderer spriteRenderer;   //用来打开关闭背包效果
    public Sprite[] sprite;         //打开关闭背包效果
    public SpriteRenderer[] cellSprites;       //储物格子
    public SpriteRenderer[] usedEquipmet;    //穿戴装备
    public static bool[] cell = new bool[12];     //储物格存放状态
    public static string[] names = new string[7];   //存放已装备物品名字
    public static Dictionary<int, string> information=new Dictionary<int, string>();  //储物格中装备信息
    static bool isNew = true; 
    bool active;
    GameObject floor;
    static string[] wearLocation = { "weapon", "shield", "clothers", "hat", "glove", "ring", "shoe" };
    // Use this for initialization
    void Awake() {

        active = false;
        //Debug.Log(""+cell[1]);
        Debug.Log("isNew00");
        if (!isNew)
        {
          
            Debug.Log("isNew0");
            for (int index = 0; index < names.Length; index++)
            {//wear
                if (names[index]!= null&& !names[index].Equals(""))
                {
                    Debug.Log("isNew");
                    //usedEquipmet[index].sprite = Resources.Load<Sprite>("sprite/" + names[index]);// LoadAll<Sprite>("sprite/equipment");
                    Sprite[] equip=Resources.LoadAll<Sprite>("sprite/equipment");
                    foreach (Sprite s in equip)
                        if (s.name.Equals(names[index]))
                            usedEquipmet[index].sprite = s;
                    //readInformation
                    Debug.Log(":"+index);
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

    void Start()
    {
        Debug.Log("start");

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
            floor.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            active = false;
            spriteRenderer.sprite = sprite[1];
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
                Debug.Log("cell " + i);
                //设置图片并获取装备属性等信息
                cellSprites[i].sprite = sprite;
                cellSprites[i].gameObject.GetComponent<Equipment>().readEquipmentInf();
                Debug.Log("" + i);
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

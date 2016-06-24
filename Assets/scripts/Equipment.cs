///
//** 用来实现在背包中鼠标拖动装备，挂在每个可放格子的gameobject上，
///


using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class Equipment : MonoBehaviour {

    public Transform[] able; //可放置位置
    public Transform wear; //穿戴位置   swap
    //public Transform[] wears; //可放置位置
    public int location; //格子位置
    float minDistance;
    Vector3 orignPosition;   //初始位置
    Vector3[] postion;     //存放可放置装备的格子位置

    public Vector3 wearPosition;   //wear位置  swap
    string equipmentName;  //
    public Dictionary<string, int> information=new Dictionary<string, int>();    //存放装备属性  swap
    //int type;  //装备类型编号 swap
    
    Hero hero;
    // Use this for initialization
    void Awake()
    {

    }
    void Start () {

        hero = GameObject.FindGameObjectWithTag("hero").GetComponent<Hero>();
        if (gameObject.GetComponent<SpriteRenderer>().sprite!=null)
            equipmentName = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        minDistance = sqrt(able[0].position, able[1].position) * 0.5f;
        orignPosition = transform.position;
        postion = new Vector3[able.Length+1];   //加wear
        for (int i = 0; i < able.Length; i++)
            postion[i] = able[i].position;

       // information = new Dictionary<string, int>();
    }


    public void clear()
    {
        wear = null;
        equipmentName = null;
        hero.updateProperty(information, false);
        information.Clear();
    }
    /// <summary>
    /// 计算平方根
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    float sqrt(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow((a.x - b.x), 2) + Mathf.Pow((a.y - b.y), 2));
    }

    /// <summary>
    /// 使用协程实现拖拽效果
    /// </summary>
    /// <returns></returns>
    IEnumerator OnMouseDown()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == null)
        {
            Debug.Log("null");
            GameObject.FindGameObjectWithTag("BagManager").GetComponent<BagManager>().hideProperty();
            yield break;
        }
        var camera = Camera.main;
        if (camera)
        {
            //转换对象到当前屏幕位置
            Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);

            //鼠标屏幕坐标
            Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPosition.z);

            //获得鼠标和对象之间的偏移量,拖拽时相机应该保持不动
            Vector3 offset = transform.position - camera.ScreenToWorldPoint(mScreenPosition);
            //offset.z = offset.z-30;  //for clearly see

            //若鼠标左键一直按着则循环继续  ,拖拽
            while (Input.GetMouseButton(0))
            {
                //鼠标屏幕上新位置
                mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

                // 对象新坐标 
                transform.position = offset + camera.ScreenToWorldPoint(mScreenPosition);            
                //协同，等待下一帧继续

                yield return new WaitForEndOfFrame();

            }
            //transform.position.= 30;
            //判断是否只是点击

            if (transform.position == orignPosition)
                if(location<0) //点击的是未装备的还是已装备的
                    GameObject.FindGameObjectWithTag("BagManager").GetComponent<BagManager>().showProperty(information,location);
                else
                    GameObject.FindGameObjectWithTag("BagManager").GetComponent<BagManager>().showProperty(
                        wear.gameObject.GetComponent<Equipment>().information, information, location);
            else
                dragMoveing();
        }

    }

    void showProperty()
    {

    }

    /// <summary>
    /// 拖拽完处理，通过交换数据来替换，或许可通过交换gameobject
    /// </summary>
    void dragMoveing()
    {
        int swapIndex=0;    //交换位置下标
        //计算与物品最近的格子，并移动
        postion[able.Length] = wearPosition;
        float min = 20000;
        for (int index = 0; index < postion.Length; index++)
        {

            float f = sqrt(transform.position, postion[index]);
            if (f < min)
            {
                min = f;
                swapIndex = index;
            }


        }


        if (min < minDistance)
        {

            //判断是穿戴装备还是交换存储位置
            Sprite temp;
            //Vector3 tempPostion;
            if (swapIndex != able.Length)
            {
                //判断物品是否移动
                if (able[swapIndex].position == orignPosition)
                    return;


                //交换位置
               // tempPostion = able[swapIndex].position;
                //able[swapIndex].position = orignPosition;
                //transform.position = tempPostion;


                
                //swap(able[swapIndex].GetComponent<SpriteRenderer>().sprite, ref able[swapIndex].GetComponent<Equipment>().information);
                //交换
                temp = able[swapIndex].GetComponent<SpriteRenderer>().sprite;
                able[swapIndex].GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                gameObject.GetComponent<SpriteRenderer>().sprite = temp;

               
                Dictionary<string, int> tempDic = information;
                information = able[swapIndex].GetComponent<Equipment>().information;
                able[swapIndex].GetComponent<Equipment>().information = tempDic;
                swapTransform(able[swapIndex]);
                //Transform tempTransform = wear;
               // wear = able[swapIndex].GetComponent<Equipment>().wear;
                //able[swapIndex].GetComponent<Equipment>().wear=tempTransform;


                //改变背包状态
           
                //脱下装备或只是交换存储位置
                if (location >= 0)
                {
                    BagManager.cell[location] = false;
                    BagManager.information.Remove(location);

                }
                else
                {
                    //脱下装备
                    BagManager.names[(location * (-1) - 1)] = null;
                    hero.updateProperty(tempDic, false);    //降低英雄属性
                }
                // BagManager.cell[location] = false;
                BagManager.cell[swapIndex] = true;
                //BagManager.information.Remove(location);
                BagManager.information.Add(swapIndex, able[swapIndex].GetComponent<SpriteRenderer>().sprite.name);


            }
            else
            {
                //判断物品是否移动
                if (wear.position == orignPosition)
                    return;

                if (location >= 0)
                {
                    //swap(wear.GetComponent<SpriteRenderer>().sprite, wear.GetComponent<Equipment>().information);
                    //穿上装备
                    temp = wear.GetComponent<SpriteRenderer>().sprite;
                    wear.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    gameObject.GetComponent<SpriteRenderer>().sprite = temp;

                    Dictionary<string, int> tempDic = information;
                    information = wear.GetComponent<Equipment>().information;
                    wear.GetComponent<Equipment>().information = tempDic;
                    //
                    //Transform tempTransform = wear;
                    //wear = wear.GetComponent<Equipment>().wear;
                    //wear.GetComponent<Equipment>().wear = tempTransform;
                    //改变背包状态
                    Debug.Log("" + location);
                    BagManager.cell[location] = false;
                    BagManager.information.Remove(location);
                    int wearLocation = wear.GetComponent<Equipment>().location;
                    Debug.Log(wear.GetComponent<SpriteRenderer>().sprite.name);
                    BagManager.names[(wearLocation * (-1) - 1)] = wear.GetComponent<SpriteRenderer>().sprite.name;
                    if (temp != null)
                        hero.updateProperty(information, false);     //降低英雄属性
                    hero.updateProperty(tempDic);     //增强英雄属性

                    swapTransform(wear);  //交换wear；
                }
            }

        }
        transform.position = orignPosition;
        Debug.Log(orignPosition.ToString());
    }
    /*void swap(ref Sprite s, ref Dictionary<string, int> dic)
    {
        //交换
        Sprite temp = s;
        s = gameObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = temp;

        Dictionary<string, int> tempDic = information;
        information = dic;
        dic = tempDic;
    }*/

    /// <summary>
    /// 根据装备名读取装备数据
    /// </summary>
    /// 
    public void readEquipmentInf()
    {
        if (information.Count > 0)
            return;
        string appDBPath = Application.dataPath + "/mainDate.db";
        //--------------------------

        equipmentName = gameObject.GetComponent<SpriteRenderer>().sprite.name;

        
        string[] attributes = {"attack","health","armor","hitValue","dodgeValue"};
        DbAccess db = new DbAccess("Data Source=" + appDBPath);

        string typeName;  //装备类型名称 
        using (SqliteDataReader sqReader = db.ExecuteQuery("SELECT * FROM equipment WHERE name='" + equipmentName + "'"))
        {
            while (sqReader.Read())
            {
      
                //装备类型
                typeName = sqReader.GetString(sqReader.GetOrdinal("type"));

                wear = GameObject.FindGameObjectWithTag("HUD").transform.Find("bagInterface/equipment/wear/"+ typeName+"base/"+ typeName); //+
               

                wearPosition = wear.position;//+

                foreach (string attribute in attributes)
                {
                    information.Add(attribute, sqReader.GetInt32(sqReader.GetOrdinal(attribute)));
                }
                //name = sqReader.GetString(sqReader.GetOrdinal("hero"));
            }
        }
        db.CloseSqlConnection();
    }
    public void swapTransform(Transform dstT)
    {
        Transform tempTransform = wear;
        wear = dstT.GetComponent<Equipment>().wear;
        dstT.GetComponent<Equipment>().wear = tempTransform;
        Vector3 tempPosition = wearPosition;
        wearPosition = dstT.GetComponent<Equipment>().wearPosition;
        dstT.GetComponent<Equipment>().wearPosition = tempPosition;
    }
    Transform GetTransform(Transform check,string name)
    {
    foreach (Transform t in check.GetComponentsInChildren<Transform>())
    {
    if(t.name==name){return t;}
    GetTransform(t,name);
    }
    return null;
    }

    // Update is called once per frame
    void Update () {
      
    }
}

//每过一关重新加载scene
//通过static 保存状态信息，在scene重载后保留状态
//关闭游戏后继续开始，从当前状态信息表中读取记录当前状态，从新开始，使用初始static值
//若重新开始，根据英雄名从初始状态信息表中读取记录初始状态计入static
using UnityEngine;
//using System.Collections;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hero : MonoBehaviour {
    static string hName;
    public static int attackValue=10;       //攻击力
    public static int maxHealthValue=100;       //生命上限
    public static int maxMagicValue=100;        //魔力上限
    public static int maxGhostValue=100;        //灵魂上限
    public static int armorValue=0;        //护甲值
    public static int dodgeValue=10;        //闪避值
    public static int hitValue=80;          //命中值
                                         //public 
    public static int healthValue=100;       //生命值
    public static int magicValue=100;        //魔力值
    public static int ghostValue=0;        //灵魂值
    public static bool ignoreArmor = false;
    //private int strength;         //力量
    //private int agility;          //敏捷
    //private int intelligence;     //智力
   // private int endurance;        //耐力

    //private GameObject weapon;    //武器
   // private GameObject armor;     //防具

    public SpriteBar HBar;           //血条
    public SpriteBar MBar;           //蓝条
    public SpriteBar GBar;           //魂条
    public SpriteRenderer heroPicture;
    public Sprite[] herosPicture;
    string curHeroName;
    public Text attack;    //显示攻击力
    public Text armor;    //显示护甲
    public GameObject end;
    // Use this for initialization
    void Start () {
        // Hbar=gameObject.getGetComponentsInChildren<Hbar>
        //healthValue = maxHealthValue;
        //magicValue = maxMagicValue;
        //ghostValue = maxGhostValue;
        hName = "";

        // readDate();
        initHero();
  
    }

    void initHero()
    {


        if (!string.Equals(hName, ""))  //可去除
            curHeroName = hName;
        else
            curHeroName = ChoicManager.heroChoosed;
        //设置hero图片
        for (int i = 0; i < herosPicture.Length; i++)
        {
            if (string.Equals(herosPicture[i].name, curHeroName))
            {
                heroPicture.sprite = herosPicture[i];
                break;
            }
        }
        updatePropertyValue();
        //HBar.updateBar(1 - (healthValue * 1.0f / maxHealthValue), healthValue);
        //MBar.updateBar((magicValue * 1.0f / maxMagicValue), magicValue);  //加上崩溃
        //GBar.updateBar((ghostValue * 1.0f / maxGhostValue), ghostValue);
    }



    public void updateProperty(Dictionary<string, int> dic,bool isAdd=true)
    {
        int sign = 1;

        if (!isAdd)
        { //脱下装备
            sign = -1;  
        }

        //变更属性
        foreach (KeyValuePair<string, int> kvp in dic)
        {
            switch (kvp.Key)
            {
                case "attack": attackValue += kvp.Value * sign; break;
                case "health":
                    {
                        if(healthValue== maxHealthValue)
                            healthValue += kvp.Value * sign;
                        maxHealthValue += kvp.Value * sign;                        
                    }
                    break;
                case "armor": armorValue += kvp.Value * sign; break;
                case "hitValue": dodgeValue += kvp.Value * sign; break;
                case "dodgeValue": hitValue += kvp.Value * sign; break;
                default: break;
            }

        }

        updatePropertyValue();

    }
    /// <summary>
    /// 在继续游戏时读取保存数据
    /// </summary>
    public static void readDate()
    {
        //
        string appDBPath = Application.dataPath + "/mainDate.db";

        //--------------------------

        DbAccess db = new DbAccess("Data Source=" + appDBPath);
        Debug.Log("db");
        //path = appDBPath;

        //读取当前状态或初始状态
        string table;
        table = "status";

        using (SqliteDataReader sqReader = db.ReadFullTable(table))    
        {

            while (sqReader.Read())
            {
                //if(sqReader.GetString(sqReader.GetOrdinal("hero"))==name)    //start

               
                maxHealthValue = sqReader.GetInt32(sqReader.GetOrdinal("maxHealth"));
                maxMagicValue = sqReader.GetInt32(sqReader.GetOrdinal("maxMagic"));
                maxGhostValue = sqReader.GetInt32(sqReader.GetOrdinal("maxGhost"));
                healthValue = sqReader.GetInt32(sqReader.GetOrdinal("curHealth"));
                magicValue = sqReader.GetInt32(sqReader.GetOrdinal("curMagic"));
                ghostValue = sqReader.GetInt32(sqReader.GetOrdinal("curGhost"));
                armorValue = sqReader.GetInt32(sqReader.GetOrdinal("armorValue"));
                dodgeValue = sqReader.GetInt32(sqReader.GetOrdinal("dodgeValue"));
                hitValue = sqReader.GetInt32(sqReader.GetOrdinal("hitValue"));
                Manager.level= sqReader.GetInt32(sqReader.GetOrdinal("level"));

                //name = sqReader.GetString(sqReader.GetOrdinal("hero"));
            }
        }
        db.CloseSqlConnection();
    }

    /// <summary>
    /// 从新开始时读取角色初始数据
    /// </summary>
    /// <param name="name"></param>
    public static void readDateByHeroName(string name)
    {
        string appDBPath = Application.dataPath + "/mainDate.db";
        //--------------------------

        DbAccess db = new DbAccess("Data Source=" + appDBPath);

        //读取当前状态或初始状态
        using (SqliteDataReader sqReader = db.ExecuteQuery("SELECT * FROM initStatus WHERE hero='" + name+"'"))
        {           
            while (sqReader.Read())
            {
                //if(sqReader.GetString(sqReader.GetOrdinal("hero"))==name)    //start
                maxHealthValue = sqReader.GetInt32(sqReader.GetOrdinal("maxHealth"));
                maxMagicValue = sqReader.GetInt32(sqReader.GetOrdinal("maxMagic"));
                maxGhostValue = sqReader.GetInt32(sqReader.GetOrdinal("maxGhost"));
                healthValue = sqReader.GetInt32(sqReader.GetOrdinal("curHealth"));
                magicValue = sqReader.GetInt32(sqReader.GetOrdinal("curMagic"));
                ghostValue = sqReader.GetInt32(sqReader.GetOrdinal("curGhost"));
                armorValue = sqReader.GetInt32(sqReader.GetOrdinal("armorValue"));
                dodgeValue = sqReader.GetInt32(sqReader.GetOrdinal("dodgeValue"));
                hitValue = sqReader.GetInt32(sqReader.GetOrdinal("hitValue"));
                hName = sqReader.GetString((sqReader.GetOrdinal("hero")));
                //name = sqReader.GetString(sqReader.GetOrdinal("hero"));
            }
        }
        db.CloseSqlConnection();
    }

    /// <summary>
    /// 保存角色数据，在NextScene调用
    /// </summary>
    public  void saveDate()
    {
        //
        string appDBPath = Application.dataPath + "/mainDate.db";

        //--------------------------

        DbAccess db = new DbAccess(@"Data Source=" + appDBPath);
       
        //path = appDBPath;

        db.UpdateInto("status", new string[] { "maxHealth", "maxMagic", "maxGhost", "curHealth", "curMagic",
            "curGhost","hero" , "armorValue", "dodgeValue", "hitValue" ,"level"},
            new string[] { maxHealthValue.ToString(), maxMagicValue.ToString(), maxGhostValue.ToString(),
                healthValue.ToString(), magicValue.ToString(),ghostValue.ToString(),"'"+curHeroName+"'",
                armorValue.ToString(),dodgeValue.ToString(),hitValue.ToString(),Manager.level.ToString()},"rowid","1");

        db.CloseSqlConnection();
    }


    public int getAttackValue()
    {
        return attackValue;
    }
    public int getHitValue()
    {
        return hitValue;
    }

    public void addHP(int hp)
    {
        //若生命满，增加上限，若不满，最多只能加到满
        if (healthValue == maxHealthValue)   
        {
            maxHealthValue += hp;
            healthValue = maxHealthValue;
        }
        else
        {
            healthValue += hp;
            if (healthValue > maxHealthValue)
                healthValue= maxHealthValue;
        }
        HBar.updateBar(1 - (healthValue * 1.0f / maxHealthValue), healthValue);
       
    }
    public bool getDamagedByMonster(int att,int h)  //怪物攻击及命中
    {
        /*闪避判断*/
        int hit = (h * (100-dodgeValue)) / 100;   //(h * (100-dodgeValue)) / 10000  因要与随机数比 *100
        int random=Random.Range(1,101);
        if (random > hit)
            return false;

        /*伤害计算*/

        //护甲会随攻击减少
        int damage = armorValue - att;
        if (damage > 0)
        {
            armorValue = damage;
            //更新护甲
        }
        else
        {
            armorValue = 0;
            healthValue += damage;
            ///检测并处理死亡
            Dead();

            //不死更新血量
            HBar.updateBar(1-(healthValue*1.0f/ maxHealthValue), healthValue);
        }
        /*  护甲不会减
        int damage = att-armorValue;
        if (damage >0)
            healthValue -= damage; //  dont consider defence*/
        return true;
    }

    /// <summary>
    /// 更新属性
    /// </summary>
    void updatePropertyValue()
    {
        attack.text = attackValue.ToString();
        armor.text = armorValue.ToString();
        HBar.updateBar(1 - (healthValue * 1.0f / maxHealthValue), healthValue);
        MBar.updateBar((magicValue * 1.0f / maxMagicValue), magicValue);  
        GBar.updateBar((ghostValue * 1.0f / maxGhostValue), ghostValue);
    }

    void Dead()
    {
        if (healthValue < 0)
        {
            healthValue = 0;
            end.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update () {



    }
}

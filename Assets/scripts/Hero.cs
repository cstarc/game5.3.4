using UnityEngine;
//using System.Collections;
using Mono.Data.Sqlite;
using UnityEngine.UI;
public class Hero : MonoBehaviour {
    static string hName;
    public static int attackValue=10;       //攻击力
    public static int maxHealthValue=100;       //生命上限
    public static int maxMagicValue=100;        //魔力上限
    public static int maxGhostValue=100;        //灵魂上限
    public static int armorValue=0;        //护甲值
    public static int dodgeValue=0;        //闪避值
    public static int hitValue=100;          //命中值
                                         //public 
    public static int healthValue=100;       //生命值
    public static int magicValue=100;        //魔力值
    public static int ghostValue=0;        //灵魂值
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
    // Use this for initialization
    void Start () {
        // Hbar=gameObject.getGetComponentsInChildren<Hbar>
        //healthValue = maxHealthValue;
        //magicValue = maxMagicValue;
        //ghostValue = maxGhostValue;
        Debug.Log(""+ healthValue+ magicValue+ maxMagicValue);
        hName = "";

        // readDate();
        initHero();
        HBar.updateBar(1 - (healthValue * 1.0f / maxHealthValue));
        MBar.updateBar((magicValue * 1.0f / maxMagicValue));  //加上崩溃
        GBar.updateBar((ghostValue * 1.0f / maxGhostValue));
    }

    void initHero()
    {


        if (!string.Equals(hName, ""))
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
     
    }

    public static void readDate()
    {
        //
        string appDBPath = Application.dataPath + "/mainDate.db";

        //--------------------------

        DbAccess db = new DbAccess("Data Source=" + appDBPath);
        Debug.Log("db");
        //path = appDBPath;

        using (SqliteDataReader sqReader = db.ReadFullTable("status"))    
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
                hName = sqReader.GetString((sqReader.GetOrdinal("hero")));
                //name = sqReader.GetString(sqReader.GetOrdinal("hero"));
            }
        }
        db.CloseSqlConnection();
    }

    public void saveDate()
    {
        //
        string appDBPath = Application.dataPath + "/mainDate.db";

        //--------------------------

        DbAccess db = new DbAccess(@"Data Source=" + appDBPath);
        Debug.Log("db");
        //path = appDBPath;

        db.UpdateInto("status", new string[] { "maxHealth", "maxMagic", "maxGhost", "curHealth", "curMagic", "curGhost","hero" },
            new string[] { maxHealthValue.ToString(), maxMagicValue.ToString(), maxGhostValue.ToString(), healthValue.ToString(),
            magicValue.ToString(),ghostValue.ToString(),"'"+curHeroName+"'"},"rowid","1");

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
        HBar.updateBar(1 - (healthValue * 1.0f / maxHealthValue));
       
    }
    public bool getDamagedByMonster(int att,int h)  //怪物攻击及命中
    {
        /*闪避判断*/
        int hit = (h * (100-dodgeValue)) / 100;   //(h * (100-dodgeValue)) / 10000  因要与随机数比 *100
        int random=Random.Range(1,101);
        Debug.Log(""+ random);
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
            HBar.updateBar(1-(healthValue*1.0f/ maxHealthValue));
        }
        /*  护甲不会减
        int damage = att-armorValue;
        if (damage >0)
            healthValue -= damage; //  dont consider defence*/
        return true;
    }
    void updateBar(Sprite bar)
    {
        ;
    }

    void Dead()
    {
        if (healthValue < 0)
            ;
    }
    // Update is called once per frame
    void Update () {
	

	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Monster : MonoBehaviour {
    public int attackValue;                    //攻击力 都为物理攻击
    public int healthValue;                    //生命值
    public int armorValue;                     //护甲值
    public int dodgeRate;                     //闪避值
    public int hitRate;                       //命中值
    //public int MagicResistanceValue;           //魔法抗性 
    public Text health;
    public Text attack;
    public Text armor;
    public bool ignoreArmor;                  //穿透护甲    
    public GameObject treasure;
    bool hasArmor;   
    public bool isBoss;
    public GameObject  hitMonster;
    public GameObject unHit;       
    // Use this for initialization
    void Start () {
        hasArmor = true;
        //attack.text = ""+attackValue;
        //health.text = "" + healthValue;
        updateStatus();
        
    }

    /// <summary>
    /// 更新怪物状态
    /// </summary>
    void updateStatus()
    {
        updateHealth();
        updateAttack();
        updateArmor();

    }

    /// <summary>
    /// 显示各属性
    /// </summary>
    void updateHealth()
    {
        health.text = "" + healthValue;
    }
    void updateArmor()
    {
        if (armorValue == 0)
        {
            armor.gameObject.transform.parent.gameObject.SetActive(false);
            hasArmor = false;
            return;
        }
        if (!hasArmor)
            if (armorValue > 0)
            {
                armor.gameObject.transform.parent.gameObject.SetActive(true);
                armor.text = "" + armorValue;
            }

    }      
    void updateAttack()
    {
        attack.text = "" + attackValue;
    }

    /// <summary>
    /// 根据当前关卡等级设置怪物属性
    /// </summary>
    /// <param name="level"></param>
    public void setLevel(int level)
    {
        attackValue +=Random.Range(level,level*3);                    
        healthValue += Random.Range(level, level * 3);                    
        armorValue += Random.Range(level, level * 3);
        updateStatus();
    }

    void OnMouseDown()
    {
        Hero hero= GameObject.Find("heroPanel").GetComponent<Hero>();
        int h = hero.getHitValue();   //获取命中值
        /*闪避判断*/
        int hit = (h * (100 - dodgeRate)) / 100;   //(h * (100-dodgeValue)) / 10000  因要与随机数比 *100
        int random = Random.Range(1, 101);
        if (random > hit)
        {
            if (unHit != null)
            {
                GameObject go = Instantiate(unHit, transform.position, transform.rotation) as GameObject;
                Destroy(go, 0.2f);
            }
            //dont hit
            return;
        }
        if (hitMonster != null)
        {//伤害效果显示
            GameObject go = Instantiate(hitMonster, transform.position, transform.rotation) as GameObject;
            Destroy(go, 0.2f);
        }

        /*自身受到伤害  无抗性处理，无护甲穿透*/

        //当为物理攻击不穿透护甲，护甲会随攻击减少
        if (!Hero.ignoreArmor&&armorValue != 0)
        {
            int damage = armorValue - hero.getAttackValue();
            if (damage > 0)
            {
                armorValue = damage;
                //更新护甲
            }
            else
            {
                armorValue = 0;
                healthValue += damage; 
            }
            updateArmor(); 
        }
        else
        {
            healthValue -= hero.getAttackValue();
        }

        ///检测并处理死亡
        Dead();

        //不死更新血量
        updateHealth();

        /* 护甲不会减

        int damage = hero.getAttackValue() - armorValue;
         Debug.Log("damage" + damage);
         if (damage > 0)
         {
             healthValue -= damage;
             ///检测并处理死亡
             Dead();

             //不死更新血量
             updateHealth();



         }*/

        /*对英雄造成伤害*/
        hero.getDamagedByMonster(attackValue, hitRate);
       
    }
    void Dead()
    {
        if (healthValue < 0)
        {
            if (Random.Range(0, 11) < 11 && !isBoss)
            {//获得奖励
                GameObject obj = Instantiate(treasure, transform.position, transform.rotation) as GameObject;
                obj.transform.parent = GameObject.FindGameObjectWithTag("manager").transform;  //挂在manager下       
            }
            else
            {
                treasure.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
	
	}
}

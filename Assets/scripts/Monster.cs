using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Monster : MonoBehaviour {
    public int attackValue;                    //攻击力
    public int healthValue;                    //生命值
    public int armorValue;                     //护甲值
    public int dodgeRate;                     //闪避值
    public int hitRate;                       //命中值
    public int MagicResistanceValue;           //魔法抗性 
    public Text health;
    public Text attack;                       
    // Use this for initialization
    void Start () {
        //attack.text = ""+attackValue;
        //health.text = "" + healthValue;
        updateHealth();
        updateAttack();
    }
    void updateHealth()
    {
        health.text = "" + healthValue;
    }
    void updateAttack()
    {
        attack.text = "" + attackValue;
    }
    void OnMouseDown()
    {
        Hero hero= GameObject.Find("heroPanel").GetComponent<Hero>();
        int h = hero.getHitValue();   //获取命中值
        /*闪避判断*/
        int hit = (h * (100 - dodgeRate)) / 100;   //(h * (100-dodgeValue)) / 10000  因要与随机数比 *100
        int random = Random.Range(1, 101);
        Debug.Log("怪物" + random);
        if (random > hit)
        {
            //dont hit
            return;
        }

        /*自身受到伤害  无抗性处理，无护甲穿透*/

        //护甲会随攻击减少
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
            ///检测并处理死亡
            Dead();

            //不死更新血量
            updateHealth();
        }
  
        

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
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
	
	}
}

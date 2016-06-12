using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class treasureMansger : MonoBehaviour {

    public SpriteRenderer equipment;
    public Text description;
    public BagManager bagManager;
    static GameObject currentTreasure;  //当前打开宝箱 
    // Use this for initialization
    void Start () {
	
	}
    public static void setCurTreasure(GameObject g)
    {
        currentTreasure = g;
    }
    /// <summary>
    /// 当激活时随机获得装备
    /// </summary>
    void OnEnable()
    {
        Sprite[] equipments = Resources.LoadAll<Sprite>("sprite/equipment");
        equipment.sprite = equipments[Random.Range(0,equipments.Length)];
        description.text = equipment.sprite.name;

    }
    
    /// <summary>
    /// 响应面板中的cancel键
    /// </summary>
    public void cancel()
    {
        
        gameObject.SetActive(false);
        Destroy(currentTreasure);
       // GameObject.FindGameObjectWithTag("manager").transform.Find("grayMask").gameObject.SetActive(false);
    }
    /// <summary>
    /// 响应面板中的拾取键
    /// </summary>
    public void ok()
    {
        
        gameObject.SetActive(false);
        //GameObject.FindGameObjectWithTag("manager").transform.Find("grayMask").gameObject.SetActive(false);
        //存入背包
        bagManager.addEquipment(equipment.sprite);
        Destroy(currentTreasure);
    }
    // Update is called once per frame
    void Update () {
	
	}
}

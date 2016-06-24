using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

    public GameObject treasurePlane;
    bool hasOpened = false;
    Sprite treasure;
    // Use this for initialization
    void Start () {
	
	}


    void OnMouseDown()
    {
        if (!hasOpened)
        {
            Sprite[] equipments = Resources.LoadAll<Sprite>("sprite/equipment");
            treasure = equipments[Random.Range(0, equipments.Length)];
            hasOpened = true;
        }
        // GameObject.FindGameObjectWithTag("manager").transform.Find("grayMask").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("HUD").transform.Find("treasure").gameObject.GetComponent<treasureMansger>().show(treasure);
        
        treasureMansger.setCurTreasure(gameObject);
        //transform.FindGameObjectWithTag("treasure").SetActive(true);
    }

    // Update is called once per frame
    void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour {
    public string heroName;
    public Text description;
    public ChoicManager cm;



	// Use this for initialization
	void Start () {
	
	}
	void OnMouseDown()
    {
        description.text = heroName;
 
        cm.chooseHero(heroName);   
            
    }

	// Update is called once per frame
	void Update () {
	
	}
}

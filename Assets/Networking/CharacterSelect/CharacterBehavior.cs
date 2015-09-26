using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour {
	
	[SerializeField]
	private string label;

	private string firstName, lastName;
	private string helpTextLookup;

	[SerializeField]
	private int HP, MoveStat, GunStat, CQCStat;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initCharacter(){
		firstName = txtLookup.updateString (label+"_firstname");
		lastName = txtLookup.updateString (label+"_lastname");
		helpTextLookup = txtLookup.updateString (label+"_helptext");
	}


	public string getFirstName(){return firstName;}
	public string getLastName(){return lastName;}
	public string getHelpText(){return helpTextLookup;}

	public int getHP(){return HP;}
	public int getMoveStat(){return MoveStat;}
	public int getGunStat(){return GunStat;}
	public int getCQCStat(){return CQCStat;}

}

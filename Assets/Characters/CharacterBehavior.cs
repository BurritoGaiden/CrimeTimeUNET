using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour {
	
	[SerializeField]
	private string label;
    public string Label
    {
        get { return label; }
    }

    private string firstName, lastName;

    [SerializeField]
    private Alliance team;
    public Alliance Team
    {
        get { return team; }
    }

	[SerializeField]
	private int hp, moveStat, gunStat, cqcStat;

    public int HP
    {
        get { return hp; }
    }
    public int MoveStat
    {
        get { return moveStat; }
    }
    public int GunStat
    {
        get { return gunStat; }
    }
    public int CQCStat
    {
        get { return cqcStat; }
    }


	private bool finishedMoving = false;
	public bool FinishedMoving
	{
		get {return finishedMoving; }
		set {finishedMoving = value;}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
	public void initCharacter(){
		firstName = txtLookup.updateString (label+"_firstname");
		lastName = txtLookup.updateString (label+"_lastname");
		helpTextLookup = txtLookup.updateString (label+"_helptext");
	}
    */

}

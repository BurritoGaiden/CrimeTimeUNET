using UnityEngine;
using System.Collections;

public class CharacterBehavior : MapActor {
	
	[SerializeField]
	private string label;
    public string Label
    {
        get { return label; }
    }

    private string owner;
    public string Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    private string firstName, lastName;

    [SerializeField]
    private Alliance team;
    public Alliance Team
    {
        get { return team; }
    }

    [SerializeField]
    private Stats stats;
    public Stats Stats
    {
        get { return stats; }
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

    public override IJSON ToJSON()
    {
        CharacterJSON json = new CharacterJSON();
        json.coords = Coords;
        json.username = owner;
        json.team = team;
        json.name = label;
        json.stats = stats;

        return json;
    }

}

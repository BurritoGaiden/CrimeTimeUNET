using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommandPanel : MonoBehaviour, IJSONable {

    private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value.Trim(); }
    }

    private bool connected = false;
    public bool IsConnected
    {
        get { return connected; }
        set
        {
            if(value != connected)
            {
                connected = value;
                if (connected)
                {
                    SendMessageUpwards("OnPlayerHasReconnected", playerName, SendMessageOptions.RequireReceiver);
                }
            }
          
        }
    }

    private bool ready = false;
    public bool IsReady
    {
        get { return ready; }
        set { ready = value; }
    }

    [SerializeField]
    private float timerMax = 15.0f;
    private float timerCurrent = 0.0f;

    // the chosen character for this controller. null indicates no character selected yet
    [SerializeField]
    private CharacterBehavior character = null;
    public CharacterBehavior Character
    {
        get { return character; }
        set
        {
            character = value;
            if(character!= null)
                team = character.Team;
        }
    }

    [SerializeField]
    private CharacterBehavior unit;
    public CharacterBehavior CurrentUnit
    {
        get { return unit; }
        set
        {
            unit = value;
            Reset();
        }
    }

    private Alliance team;
    public Alliance Team
    {
        get { return team; }
    }


    private bool moveSelectEnabled;
		public bool MoveSelectEnabled
	{
		get {return moveSelectEnabled; }
		set {moveSelectEnabled = value;}
	}

    private List<TileBehavior> queuedPath = new List<TileBehavior>();
    public TileBehavior[] QueuedPath
    {
        get { return queuedPath.ToArray(); }
    }

    // Use this for initialization
    void Start () {

        //this.SelectedUnit = FindObjectOfType<CharacterBehavior> ().gameObject;
        //this.SelectedUnit = unit;
        MoveSelectEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        TickDown();
	}

    // call this on the GET heartbeat rule
    public void Pulse()
    {
        IsConnected = true;
        timerCurrent = timerMax;
        Debug.Log(PlayerName + " has a pulse!");
    }

    public IJSON ToJSON()
    {
        PlayerJSON json = new PlayerJSON();
        json.username = playerName;
        json.connected = IsConnected;

        return json;
    }

    void TickDown()
    {
        if (timerCurrent >= 0)
        {
            timerCurrent -= Time.deltaTime;
            if (timerCurrent < 0)
            {
                connected = false;
                try
                {
                    SendMessageUpwards("OnPlayerHasDisconnected", playerName, SendMessageOptions.RequireReceiver);
                }
                catch
                {
                    Debug.Log("CATASTROPHIC ERROR: Server missing???");
                }
                Debug.Log(playerName + " has disconnected");
            }
        }
    }

	void Reset()
    {
        queuedPath.Clear();
	}

    // TODO: Add functionality!
    public void SpawnPlayerCharacter(TileBehavior spawnPos)
    {
        GameObject pc;
        if (character != null)
        {
            pc = GameObject.Instantiate(character.gameObject);
            pc.transform.position = new Vector3(spawnPos.transform.position.x, 0.125f, spawnPos.transform.position.z);
            pc.GetComponent<CharacterBehavior>().Coords = spawnPos.Coords;
            CurrentUnit = pc.GetComponent<CharacterBehavior>();
            character = CurrentUnit;
        }
          
    }

    public void PathSelection(TileBehavior tile)
    {
        if (unit != null && tile.IsPathable)
            if (moveSelectEnabled)
            {
                // if the list is empty, set the start at where you're at, idiot
                if (queuedPath.Count == 0)
                {
                    RaycastHit hit = new RaycastHit();
                    Physics.Raycast(unit.transform.position, Vector3.down, out hit, 10f);
                    GameObject tileBelow = hit.collider.gameObject;
                    if (tileBelow.GetComponent<TileBehavior>() != null)
                    {
                        //Debug.Log("Below you is " + tileBelow.GetComponent<TileBehavior>().X + "," + tileBelow.GetComponent<TileBehavior>().Z);
                        TileBehavior tb = tileBelow.GetComponent<TileBehavior>();
                        queuedPath.Add(tb);
                    }
                }
                int currentPos = queuedPath.Count;
                // checks the distance between the selected tile indexes (moves are only valid if this number is one)
                Coordinate distance = queuedPath[currentPos - 1].Coords - tile.Coords;
                int dx = Mathf.Abs(distance.X);
                int dz = Mathf.Abs(distance.Z);
                //if you click on an already-existant tile, remove everything past there
                if (queuedPath.Contains(tile))
                {
                    int index = queuedPath.IndexOf(tile);
                    Debug.Log("Duplicate tile found at " + index + "/" + currentPos);
                    for (int i = queuedPath.Count - 1; i > index; i--)
                    {
                        Debug.Log("Removing index " + i + ":" + queuedPath[i].Coords.ToString());
                        queuedPath.RemoveAt(i);
                    }
                }
                else if ((((dx == 1 && dz == 0) || (dx == 0 && dz == 1))
                          && (dx + dz <= 1))
                          && queuedPath.Count - 1 < 4)
                {
                    queuedPath.Add(tile);
                }

                foreach (TileBehavior tb in queuedPath)
                {
                    Debug.Log("Next Step: " + tb.Coords.ToString());
                }

                UpdateMovesLeft();
            }
    }

    public void CommitToMove(){

		Movement move = new Movement (unit, queuedPath, true);
		StartCoroutine(Execute (move));
	}

	void UpdateMovesLeft(){
//		movesLeft.text = (unitBehavior.getMoveStat() - (queuedPath.Count-1)).ToString();
	}


    // will need to rewrite soon
	public void TargetAttack(MapActor target){
		float dx = Mathf.Abs(unit.transform.position.x - target.transform.position.x);
		float dz = Mathf.Abs(unit.transform.position.z - target.transform.position.z);
		Debug.Log ("Targeting: " + dx + " + " + dz);
		Vector3 distanceBetween = target.transform.position - unit.transform.position;
		RaycastHit hit = new RaycastHit();
		if ((dx + dz) <= 7.0f && 
		    Physics.Raycast(unit.transform.position, (distanceBetween).normalized, out hit, distanceBetween.magnitude)) {
			if(hit.collider.gameObject.Equals(target)){
				Debug.DrawLine(unit.transform.position, target.transform.position,Color.red);
				Debug.Log ("HIT!");
				CommitToAttack(target);
			}
			else{
				CommitToAttack(hit.collider.gameObject.GetComponent<CharacterBehavior>());
			}
		}
	}

	public void CommitToAttack(MapActor target){
		Attack attack = new Attack (unit, target, true);
		StartCoroutine(Execute (attack));
	}

	public IEnumerator Execute(Action action){
		Debug.Log ("Executing...");
		FindObjectOfType<FieldReporter> ().addActionToTurn(action);
		yield return StartCoroutine(action.Execute());
		Reset ();
        yield return new WaitForSeconds(0.5f);
		FindObjectOfType<FieldReporter> ().checkToIncrememt ();
	}


}

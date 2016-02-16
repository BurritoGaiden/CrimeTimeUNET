using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FieldReporter : MonoBehaviour {

	//The innermost list is a list of all things that happen per turn, 
	//The outermost list is a list each turn
	private List<List<Action>> Record = new List<List<Action>>();
	private float timescale = 1f;

	private int turnNumber = 0;
	private int turnMax;
	[SerializeField]
	private Text[] turnDisplay; 
	[SerializeField]
	private Text scaleDisplay; 
	[SerializeField]
	private Button replayButton; 
	[SerializeField]
	private Button pauseButton;  
	private bool pauseToggle = true;
    [SerializeField]
    private RectTransform intertitle;

    private bool thievesTurn = true;
    [SerializeField]
    private Text teamDisplay;

    // Use this for initialization
    void Start () {
		incrementTurn ();
		pauseButton.interactable = false;
		scaleDisplay.text = Time.timeScale.ToString("0.00") + "x";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void incrementTurn(){
		Record.Add (new List<Action> ());
        turnNumber = Record.Count;
		updateTurnDisplay(turnNumber);
	}

	void updateTurnDisplay(int turn){
        StartCoroutine(Transition(turn, 1f));
    }

	public void addActionToTurn(Action a){
		Record [turnNumber-1].Add (a);
	}

	public void adjustReplayTimescale (float t){
		timescale = Mathf.Pow (2f, t);
		Debug.Log (timescale);
		Time.timeScale = timescale;
		scaleDisplay.text = Time.timeScale.ToString("0.00") + "x";
	}

	public void pause(){
		if (pauseToggle) {
			pauseButton.GetComponentInChildren<Text>().text = "Resume";
			pauseToggle = false;
			Time.timeScale = 0.0f;
		} else {
			pauseButton.GetComponentInChildren<Text>().text = "Pause";
			pauseToggle = true;
			Time.timeScale = timescale;
		}
		scaleDisplay.text = Time.timeScale.ToString("0.00") + "x";
	}

	public void startReplay(){
		StartCoroutine (crunchTurns ());
	}

	public IEnumerator crunchTurns(){
		pauseButton.interactable = true;
		int i = 0;
		int j = 0;
		replayButton.interactable = false;
		for (j = 0; j < Record.Count; j++) {
			updateTurnDisplay (j+1);
			for (i = 0; i < Record[j].Count; i++) {
				yield return StartCoroutine (Record [j] [i].Execute ());
			}
		}
		replayButton.interactable = true;
		pauseButton.interactable = false;
	}

	public void checkToIncrememt(){
		incrementTurn ();
	}

    // Tear out the lerp subroutine and make that a single external thing instead of repeating code
    public IEnumerator Transition(int turn, float seconds)
    {
        if (thievesTurn)
            teamDisplay.text = "The Gang's\nTurn";
        else
            teamDisplay.text = "The Cop's\nTurn";

        intertitle.gameObject.SetActive(false);
        CanvasGroup cr = intertitle.GetComponentInParent<CanvasGroup>();

        yield return StartCoroutine(AlphaLerp(cr, 0.0f, 1.0f, 0.75f * seconds));

        foreach (Text t in turnDisplay)
            t.text = "Turn: " + turn;

        intertitle.position = new Vector3(-Screen.width, Screen.height / 2.0f, 0);
        intertitle.gameObject.SetActive(true);

        yield return StartCoroutine(EaseLerp(intertitle, new Vector3(Screen.width / 2.0f, -Screen.height, 0), new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0), seconds));
        yield return new WaitForSeconds(1.25f*seconds);
        yield return StartCoroutine(EaseLerp(intertitle, new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0), new Vector3(Screen.width / 2.0f, 2* Screen.height, 0), seconds));

        intertitle.gameObject.SetActive(false);

        yield return StartCoroutine(AlphaLerp(cr, 1.0f, 0.0f, 0.75f * seconds));

        thievesTurn = !thievesTurn;
    }

    // Helper Coroutines

    IEnumerator EaseLerp(RectTransform rect, Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            rect.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t)))));
            yield return null;
        }
    }

    IEnumerator AlphaLerp(CanvasGroup cr, float from, float to, float seconds)
    {
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            cr.alpha = Mathf.Lerp(from, to, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FieldReporter : MonoBehaviour
{

    //The innermost list is a list of all things that happen per turn, 
    //The outermost list is a list each turn
    private List<List<Action>> Record = new List<List<Action>>();
    private float timescale = 1f;

    private int turnNumber = 0;
    private int turnMax;
    [SerializeField]
    private Text turnDisplay;
    [SerializeField]
    private Text scaleDisplay;
    [SerializeField]
    private Button replayButton;
    [SerializeField]
    private Button pauseButton;
    private bool pauseToggle = true;

    private bool thievesTurn = true;

    // Use this for initialization
    void Start()
    {
        incrementTurn();
        pauseButton.interactable = false;
        scaleDisplay.text = Time.timeScale.ToString("0.00") + "x";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementTurn()
    {
        Record.Add(new List<Action>());
        turnNumber = Record.Count;
        updateTurnDisplay(turnNumber);
    }

    void updateTurnDisplay(int turn)
    {
        StartCoroutine(Transition(turn, 1.25f));
    }

    public void addActionToTurn(Action a)
    {
        Record[turnNumber - 1].Add(a);
    }

    public void adjustReplayTimescale(float t)
    {
        timescale = Mathf.Pow(2f, t);
        Debug.Log(timescale);
        Time.timeScale = timescale;
        scaleDisplay.text = Time.timeScale.ToString("0.00") + "x";
    }

    public void pause()
    {
        if (pauseToggle)
        {
            pauseButton.GetComponentInChildren<Text>().text = "Resume";
            pauseToggle = false;
            Time.timeScale = 0.0f;
        }
        else
        {
            pauseButton.GetComponentInChildren<Text>().text = "Pause";
            pauseToggle = true;
            Time.timeScale = timescale;
        }
        scaleDisplay.text = Time.timeScale.ToString("0.00") + "x";
    }

    public void startReplay()
    {
        StartCoroutine(crunchTurns());
    }

    public IEnumerator crunchTurns()
    {
        pauseButton.interactable = true;
        int i = 0;
        int j = 0;
        replayButton.interactable = false;
        for (j = 0; j < Record.Count; j++)
        {
            updateTurnDisplay(j + 1);
            for (i = 0; i < Record[j].Count; i++)
            {
                yield return StartCoroutine(Record[j][i].Execute());
            }
        }
        replayButton.interactable = true;
        pauseButton.interactable = false;
    }

    public void checkToIncrememt()
    {
        incrementTurn();
    }

    // Tear out the lerp subroutine and make that a single external thing instead of repeating code
    public IEnumerator Transition(int turn, float seconds)
    {
        Intertitle.Instance.TextMode();
        if (thievesTurn)
        {
            GameStateManager.Instance.GameState = GameState.GangTurn;
            Intertitle.Instance.Subtitle.text = "The Gang's\nTurn";

        }
        else
        {
            GameStateManager.Instance.GameState = GameState.CopsTurn;
            Intertitle.Instance.Subtitle.text = "The Cop's\nTurn";
        }

            turnDisplay.text = "Turn: " + turn;

        yield return Intertitle.Instance.StartCoroutine(Intertitle.Instance.FromBottomToCenter(seconds, true));
        yield return new WaitForSeconds(1.25f * seconds);
        yield return Intertitle.Instance.StartCoroutine(Intertitle.Instance.FromCenterToTop(seconds, true));

        thievesTurn = !thievesTurn;
    }

}

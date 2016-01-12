using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameState state = GameState.MainMenu;
    public GameState GameState
    {
        get { return state; }
        set
        {

            if (value != state)
            {
                state = value;
                BroadcastMessage("OnGameStateHasChanged", state, SendMessageOptions.RequireReceiver);
            }
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


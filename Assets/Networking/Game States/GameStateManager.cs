using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : Singleton<GameStateManager>
{

    protected GameStateManager() {}
    [SerializeField]
    private GameObject webServer;

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
                Debug.Log(state);
                foreach(GameObject go in FindObjectsOfType<GameObject>())
                go.SendMessage("OnGameStateHasChanged", state, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    // Use this for initialization
    void Awake()
    {
        /*
        webServer = (GameObject)Resources.Load("QuickWeb/WebPlayerServer");
        GameObject ws = GameObject.Instantiate(webServer);
        ws.transform.parent = this.gameObject.transform;
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGameStateHasChanged(GameState newState)
    {
        
    }
}


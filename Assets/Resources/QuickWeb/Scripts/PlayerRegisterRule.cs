using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;   
using System;
using System.Text;
using System.Net;
using System.IO;

public class PlayerRegisterRule : WebServerRule
{

    [Serializable]
    public struct HtmlKeyValue
    {
        [SerializeField]
        private string key;
        public string Key
        {
            get { return key; }
        }

        [SerializeField]
        private string value;
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public HtmlKeyValue(string key, string value)
        {
            this.key = key;
            this.value = value;
        }


    }

#if UNITY_STANDALONE || UNITY_EDITOR

    protected virtual string ModifyHtml(string html)
    {
        return html;
    }

    protected override IEnumerator OnRequest(HttpListenerContext context)
    {
        string dataString = "";

        HttpListenerRequest request = context.Request;
        StreamReader reader = new StreamReader(request.InputStream);
        string username = reader.ReadToEnd();
        username = username.Trim();
        //JSONWrapper j = new JSONWrapper(username);

        if (playerRegister.ContainsKey(username) && playerRegister[username].IsConnected == false)
        {
            dataString = "Player found!";
        }
        else if (!playerRegister.ContainsKey(username) && playerRegister.Count < 5 && allowNewJoins)
        {

            if (!firstPlayerJoined)
            {
               GameStateManager.Instance.GameState = GameState.CharacterSelect;
            }
            //accept the request to join and create a controller instance 
            GameObject newController = Instantiate<GameObject>(controllerPrefab);
            newController.transform.parent = this.gameObject.transform;
            newController.name = username + "'s Controller";

            CommandPanel newControllerCP = newController.GetComponent<CommandPanel>();
            newControllerCP.PlayerName = username;
            newControllerCP.Pulse();

            playerRegister.Add(username, newControllerCP);
            dataString = "Player not found; space available, adding " + username + " to the register!";

            firstPlayerJoined = true;
        }
        else
        {
            //reject the request to join
            dataString = "Maximum player count reached or player still connected!";
        }

        Debug.Log(dataString);

        byte[] data = Encoding.ASCII.GetBytes(dataString);

        yield return null;

        HttpListenerResponse response = context.Response;

        response.ContentType = "text/plain";

        Stream responseStream = response.OutputStream;

        int count = data.Length;
        int i = 0;
        while (i < count)
        {
            if (i != 0)
                yield return null;

            int writeLength = Math.Min((int)writeStaggerCount, count - i);
            responseStream.Write(data, i, writeLength);
            i += writeLength;
        }
    }

    // called when a player controller sends the message upward that it has disconnected
    void OnPlayerHasDisconnected(string dcName)
    {
       if(deleteOnDisconnect)
        {
            if(GetComponent<ControllerInputRule>().CharSelectManager != null)
            {
                GetComponent<ControllerInputRule>().CharSelectManager.DeselectPlayerCharacter(PlayerRegister[dcName]);
            }
            Destroy(PlayerRegister[dcName].gameObject);
            PlayerRegister.Remove(dcName);
            Debug.Log(dcName + " has disconnected during a non-gameplay game state, and was therefore removed from the registry");
        }
    }

    // changes parameter of how this rule functions depending on the game state
    void OnGameStateHasChanged(GameState newState)
    {
        switch (newState) {
            case GameState.CharacterSelect:
                deleteOnDisconnect = true;
                break;

            case GameState.GameBegin:
                deleteOnDisconnect = false;
                break;

            default:
                break;
        }
    }

#endif

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private int writeStaggerCount = 4096;

    [Header("Player Controller Prefabs")]
    [SerializeField]
    private GameObject controllerPrefab;

    private static Dictionary<String, CommandPanel> playerRegister = new Dictionary<String, CommandPanel>();
    public static Dictionary<String, CommandPanel> PlayerRegister
    {
        get { return playerRegister; }
    }
    // if true, delete entries from the player registry when they disconnect (for example, during pre-game)
    private bool deleteOnDisconnect = true;

    // if true, allow new players to join. otherwise, only allow re-connects
    private bool allowNewJoins = true;

    // if true, a player has joined at some point during this application's launch session
    private bool firstPlayerJoined = false;
}

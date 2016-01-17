using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.IO;

public class ControllerInputRule : WebServerRule
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

        HttpListenerRequest request = context.Request;
        StreamReader reader = new StreamReader(request.InputStream);
        string command = reader.ReadToEnd();

        JSONWrapper j = new JSONWrapper(command);
        string givenCommand = "";
        string user = "";
            try
            {
                givenCommand = j["command"];
                user = j["username"]; 
                Debug.Log("Receiving command " + givenCommand + " from " + user);
        }
        catch(Exception e)
            {
                Debug.Log(e.Message);
            }   
        if (PlayerRegisterRule.PlayerRegister.ContainsKey(user))
        {
            CommandPanel cp = PlayerRegisterRule.PlayerRegister[user];
            switch (givenCommand)
            {
                // for use during charcter select
                case "ChooseCharacter":
                    if(GetComponentInParent<GameStateManager>().GameState == GameState.CharacterSelect)
                    {
                        string charSelected = "";
                        try
                        {
                            charSelected = j["character"];
                        } catch(Exception e)
                        {
                            Debug.Log(e.Message);
                        }

                        // if the character was able to be assigned
                        if(charSelected != "" && charSelectManager != null)
                        {
                            try
                            {
                                //TODO: Make and implement the character selector
                               charSelectManager.SelectCharForPlayer(charSelected, cp);
                            } catch(Exception e)
                            {
                                Debug.Log(e.Message);
                            }
                        }
                    }
                    break;

                // commit their movement path and execute a move
                case "CommitMove":
                    cp.CommitToMove();
                    break;

                // select a tile to add to the movement path
                case "SelectTile":
                    if (Map != null)
                    {
                        try
                        {
                            cp.PathSelection(Map.TileArray[int.Parse(j["x"]), int.Parse(j["z"])]);
                            Debug.Log("Processing tile data: {x:" + j["x"] + "," + "z:" + j["z"]+"}");
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                        }
                    }
                    break;
                
                // none of the above
                default:
                    Debug.Log("Command either not parsed or not valid. Given command was: " + givenCommand);
                    break;
            }
        }

        yield return null;
    }

    void OnLevelWasLoaded(int level)
    {
        Map = FindObjectOfType<TileGen>();
        charSelectManager = FindObjectOfType<CharacterSelectManager>();
    }

#endif

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private int writeStaggerCount = 4096;

    [Header("Gameplay Objects to Interface")]
    [SerializeField]
    private TileGen Map;
    [SerializeField]
    private CharacterSelectManager charSelectManager;
}

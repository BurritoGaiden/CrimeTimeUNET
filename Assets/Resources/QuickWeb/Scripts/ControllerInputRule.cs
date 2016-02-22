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
        string dataString = "";

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
            Debug.Log(user + " found, entering command switch");
            CommandPanel cp = PlayerRegisterRule.PlayerRegister[user];
            switch (givenCommand)
            {
                // for getting the size (and more if needed) of the map, to generate a grid at runtime
                case "GetMapData":
                    if (selectedMap != null)
                    {
                        dataString = JsonUtility.ToJson(selectedMap.ToJSON());
                    }
                    break;

                // for use during charcter select
                case "ChooseCharacter":
                    if(GameStateManager.Instance.GameState == GameState.CharacterSelect)
                    {
                        string charSelected = "";
                        try
                        {
                            charSelected = j["character"];
                            Debug.Log(charSelected);
                        } catch(Exception e)
                        {
                            Debug.Log(e.Message);
                        }

                        // if the character was able to be assigned
                        if(charSelected != "" && charSelectManager != null)
                        {
                           
                               charSelectManager.SelectCharForPlayer(charSelected, cp);
                            
                        }
                    }
                    break;
                case "ToggleReady":
                    if (charSelectManager != null)
                    {
                        cp.IsReady = !cp.IsReady;
                        List<bool> readies = new List<bool>();
                        foreach (CommandPanel r in PlayerRegisterRule.PlayerRegister.Values)
                        {
                            readies.Add(r.IsReady);
                        }
                        if (!readies.Contains(false) && !CharSelectManager.InCountdown)
                        {
                            charSelectManager.StartCoroutine("Countdown");
                        }
                        else if(readies.Contains(false))
                        {
                            charSelectManager.StartCoroutine(charSelectManager.StopCountdown());
                        }
                    }
                    break;

                // commit their movement path and execute a move
                case "CommitMove":
                    cp.CommitToMove();
                    break;

                // select a tile to add to the movement path
                case "SelectTile":
                    if (mapGenerator != null)
                    {
                        try
                        {
                            cp.PathSelection(mapGenerator.TileArray[int.Parse(j["x"]), int.Parse(j["z"])]);
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

    // auto-hook to gameplay managers when they show up
    void OnLevelWasLoaded(int level)
    {
        mapGenerator = FindObjectOfType<TileGen>();
        if (mapGenerator != null)
        {
            //mapGenerator.generateFromMap(selectedMap);
        }

        charSelectManager = FindObjectOfType<CharacterSelectManager>();
        reporter = FindObjectOfType<FieldReporter>();
    }

#endif

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private int writeStaggerCount = 4096;

    [Header("Gameplay Objects to Interface")]
    [SerializeField]
    private TileGen mapGenerator;
    public TileGen MapGenerator
    {
        get { return mapGenerator; }
    }

    [SerializeField]
    private Map selectedMap;
    public Map SelectedMap
    {
        get { return selectedMap; }
    }

    [SerializeField]
    private CharacterSelectManager charSelectManager;
    public CharacterSelectManager CharSelectManager
    {
        get { return charSelectManager; }
    }

    [SerializeField]
    private FieldReporter reporter;
    public FieldReporter Reporter
    {
        get { return reporter; }
    }
}

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
            try
            {
                givenCommand = j["command"];
            }catch(Exception e)
            {
                Debug.Log(e.Message);
            }   
        if (PlayerRegisterRule.PlayerRegister.ContainsKey(j["username"]))
        {
            CommandPanel cp = PlayerRegisterRule.PlayerRegister[j["username"]];
            Debug.Log(j["command"]);
            switch (givenCommand)
            {
                // commit their movement path and execute a move
                case "CommitMove":
                    cp.CommitToMove();
                    break;

                // select a tile to add to the movement path
                case "SelectTile":
                    Debug.Log("Processing tile data");
                    // TODO: rewrite this because holy shit it uses a public accessor
                    cp.PathSelection(FindObjectOfType<TileGen>().tileArray[int.Parse(j["x"]), int.Parse(j["z"])]);
                    Debug.Log("x:" + j["x"] + "," + "z:" + j["z"]);
                    break;
                
                // none of the above
                default:
                    Debug.Log("Command either not parsed or not valid. Given command was: " + givenCommand);
                    break;
            }
        }

        yield return null;
    }

#endif

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private int writeStaggerCount = 4096;
}

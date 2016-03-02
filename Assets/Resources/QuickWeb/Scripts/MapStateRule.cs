using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;

public class MapStateRule : WebServerRule
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
        string s = reader.ReadToEnd();

        JSONWrapper j = new JSONWrapper(s);
        CommandPanel cp;
        try {

            cp = PlayerRegisterRule.PlayerRegister[j["username"]];
            cp.Pulse();
            Alliance playerTeam = cp.Team;

            // List of other players
            List<PlayerJSON> pj = new List<PlayerJSON>();
            foreach (CommandPanel c in PlayerRegisterRule.PlayerRegister.Values)
            {
                pj.Add((PlayerJSON)c.ToJSON());
            }

            // List of characters
            List<CharacterJSON> cj = new List<CharacterJSON>();
            foreach (CharacterBehavior c in FindObjectsOfType<CharacterBehavior>())
            {
                // if you're allies OR you're made visible
                if(c.Team == cp.Team || c.IsVisible)
                    cj.Add((CharacterJSON)c.ToJSON());
            }
            Heartbeat h = new Heartbeat();
            h.state = GameStateManager.Instance.GameState;
            h.players = pj.ToArray();
            if (cp.Character != null)
                h.myCharacter = (CharacterJSON) cp.Character.ToJSON();
            h.characters = cj.ToArray();

            dataString = JsonUtility.ToJson(h);

        }
        catch (Exception e)
        {
            // Put code here to refresh the web page
            Debug.Log(e.Message);
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
                try
                {
                    int writeLength = Math.Min((int)writeStaggerCount, count - i);
                    responseStream.Write(data, i, writeLength);
                    i += writeLength;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
       }

#endif

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private uint writeStaggerCount = 4096;
}

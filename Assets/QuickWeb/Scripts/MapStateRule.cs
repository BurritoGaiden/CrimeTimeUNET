using UnityEngine;
using System.Collections;
using System.Collections.Generic;   
using System;
using System.Text;
using System.Net;
using System.IO;

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
        string dataString = "Ping";

        HttpListenerRequest request = context.Request;
        StreamReader reader = new StreamReader(request.InputStream);
        string s = reader.ReadToEnd();

        JSONWrapper j = new JSONWrapper(s);
        CommandPanel cp;
        try {
            cp = PlayerRegisterRule.PlayerRegister[j["username"]];
            cp.Pulse();
            Alliance playerTeam = cp.Team;
            Debug.Log(cp.PlayerName + " has a pulse!");

            MapActor[] actorList = FindObjectsOfType<MapActor>();
            foreach (MapActor actor in actorList)
            {
                Type actorType = actor.GetType();
                Debug.Log(actorType);
            }


        }
        catch (Exception e)
        {
            // Put code here to refresh the web page
            Debug.Log(e.Message);
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

    void OnGameStateHasChanged(GameState newState)
    {
        currentState = newState;
    }

    private string ConstructHeartbeat(string username)
    {
        string heartbeatJSON = "";

        return heartbeatJSON;
    }


#endif

    private GameState currentState;

    private List<String> queuedPosts = new List<String>();

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private uint writeStaggerCount = 4096;
}

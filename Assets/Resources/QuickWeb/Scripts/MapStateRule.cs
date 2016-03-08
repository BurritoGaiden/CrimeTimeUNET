using UnityEngine;
using System.Collections;
using System.Collections.Generic;   
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

public class MapStateRule : WebServerRule
{

    protected class Job
    {
        public Heartbeat heartbeat;
        public HttpListenerResponse response;
    }

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


    protected virtual string ModifyHtml(string html)
    {
        return html;
    }

    void Awake()
    {
        Thread t = new Thread(ConsumerThread);
        t.Start();
    }

    protected override IEnumerator OnRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        StreamReader reader = new StreamReader(request.InputStream);
        string s = reader.ReadToEnd();

        JSONWrapper j = new JSONWrapper(s);
        CommandPanel cp;
        Heartbeat h = new Heartbeat();
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
            h.state = GameStateManager.Instance.GameState;
            h.players = pj.ToArray();
            if (cp.Character != null)
                h.myCharacter = (CharacterJSON) cp.Character.ToJSON();
            h.characters = cj.ToArray();

        }
        catch (Exception e)
        {
            // Put code here to refresh the web page
            Debug.Log(e.Message);
        }

        // TODO: Merge Job and Heartbeat in to a single class maybe?

        Job newJob = new Job();
        newJob.heartbeat = h;
        newJob.response = context.Response;
        m_jobs.Enqueue(newJob);

        yield return null;
   }

    void WriteResponse(Job job)
    {
        string dataString = JsonUtility.ToJson(job.heartbeat);

        byte[] data = Encoding.ASCII.GetBytes(dataString);

        HttpListenerResponse response = job.response;

        response.ContentType = "text/plain";

        Stream responseStream = response.OutputStream;
        
        responseStream.Write(data, 0, data.Length);

        //Debug.Log(dataString);
    }

    void ConsumerThread()
    {
        while (true)
        {
            while (m_jobs.Count > 0)
            {
                Job job = m_jobs.Dequeue();
                WriteResponse(job);
                //Debug.Log("Heartbeat Complete");
            }
            Thread.Sleep(2);
        }
    }

    private Queue<Job> m_jobs = new Queue<Job>();

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private uint writeStaggerCount = 4096;
}

using UnityEngine;
using System.Collections;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System;

public class WebServer : MonoBehaviour
{

#if UNITY_STANDALONE || UNITY_EDITOR

	// Use this for initialization
	void Start ()
    {
        listenerThread = new Thread(ListenThread);
        listenerThread.Start();

        StartCoroutine(HandleRequests());
	}

    void OnDestroy()
    {
        listenerThread.Abort();
        listener.Close();
    }

    private void ListenThread()
    {
        try
        {
            listener = new HttpListener();

            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }

            listener.Start();

            while (true)
            {
                HttpListenerContext context = listener.GetContext();

                //Debug.LogFormat("Recieved request from {0}.", context.Request.RemoteEndPoint.ToString());

                context.Response.StatusCode = 200;

                lock (waitingContexts)
                {
                    waitingContexts.AddLast(context);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogErrorFormat("Web server error at {0}.", e.Source);
            Debug.LogError(e.Message, this);
        }
    }

    private IEnumerator HandleRequests()
    {
        while(true)
        {
            HttpListenerContext nextContext = null;
            lock(waitingContexts)
            {
                if(waitingContexts.Count > 0)
                {
                    nextContext = waitingContexts.First.Value;
                    waitingContexts.RemoveFirst();
                }
            }

            if(nextContext != null)
            {
                //Debug.LogFormat("Processing request for {0}.", nextContext.Request.RemoteEndPoint.ToString());

                WebServerRule[] rules = GetComponents<WebServerRule>();
                foreach(WebServerRule rule in rules)
                {
                    bool isMatch = false;
                    IEnumerator e = rule.ProcessRequest(nextContext, x => isMatch = x);

                    do
                    {
                        yield return null;
                    }
                    while (e.MoveNext());

                    if (isMatch && rule.BlockOnMatch)
                        break;
                }

                Thread thread = new Thread(new ParameterizedThreadStart(FinishRequest));
                thread.Start(nextContext);
            }
            else
                yield return null;
        }
    }

    private void FinishRequest(object arg)
    {
        HttpListenerContext context = (HttpListenerContext)arg;

        //Debug.LogFormat("Request for {0} finished.", context.Request.RemoteEndPoint.ToString());

        context.Response.Close();
    }

#endif

    [SerializeField]
    private string[] prefixes = new string[] { "http://*:8080/QuickServer/" };

    private long workerThreadIdGenerator;

    private HttpListener listener;

    private LinkedList<HttpListenerContext> waitingContexts = new LinkedList<HttpListenerContext>();

    private Thread listenerThread;
}

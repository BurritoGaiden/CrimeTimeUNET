using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.IO;

public class HtmlRule : WebServerRule
{
    [Serializable]
    public struct HtmlKeyValue
    {
        public string Key
        {
            get { return key; }
        }

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

        [SerializeField]
        private string key;
        [SerializeField]
        private string value;
    }

	#if UNITY_STANDALONE || UNITY_EDITOR

    protected virtual string ModifyHtml(string html)
    {
        return html;
    }

    protected override IEnumerator OnRequest(HttpListenerContext context)
    {
		Debug.Log("Hello:" + context.Request.UserHostAddress);

        string html = htmlPage.text;

        foreach(HtmlKeyValue pair in substitutions)
        {
            html = html.Replace(string.Format("${0}$", pair.Key), pair.Value);
        }

        html = ModifyHtml(html);

        byte[] data = Encoding.ASCII.GetBytes(html);

        yield return null;

        HttpListenerResponse response = context.Response;

        response.ContentType = "text/html";

        Stream responseStream = response.OutputStream;

        int count = data.Length;
        int i = 0;
        while(i < count)
        {
            if (i != 0)
                yield return null;

            int writeLength = Math.Min((int)writeStaggerCount, count - i);
            responseStream.Write(data, i, writeLength);
            i += writeLength;
        }
    }

#endif

    [SerializeField]
    private TextAsset htmlPage;

    [SerializeField]
    private HtmlKeyValue[] substitutions;

    [SerializeField]
    [Tooltip("How many bytes to write before waiting a frame to continue.")]
    private uint writeStaggerCount = 4096;
}

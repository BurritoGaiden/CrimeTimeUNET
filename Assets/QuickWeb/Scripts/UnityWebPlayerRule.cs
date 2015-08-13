using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System;

[RequireComponent(typeof(HtmlRule))]
public class UnityWebPlayerRule : WebServerRule
{
#if UNITY_STANDALONE || UNITY_EDITOR

    protected override IEnumerator OnRequest(HttpListenerContext context)
    {
		Debug.Log ("Some Random String");
        HttpListenerResponse response = context.Response;
        Stream stream = response.OutputStream;

        response.ContentType = "application/octet-stream";

        byte[] data = unityWebPackage.bytes;

        int i = 0;
        int count = data.Length;

        while(i < count)
        {
            if (i != 0)
                yield return null;

            int writeLength = Math.Min((int)writeStaggerCount, count - i);

            stream.Write(data, i, writeLength);

            i += writeLength;
        }
    }

#endif

    [SerializeField]
    [Tooltip("The unity web file to transfer to the client.")]
    private TextAsset unityWebPackage;

    [SerializeField]
    [Tooltip("Number of bytes to write before waiting a frame.")]
    private uint writeStaggerCount = 1024 * 1024;
}

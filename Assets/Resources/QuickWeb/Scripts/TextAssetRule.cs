using UnityEngine;
using System.Collections;
using System.Net;
using System;

public class TextAssetRule : WebServerRule
{
#if UNITY_STANDALONE || UNITY_EDITOR

    protected override IEnumerator OnRequest(HttpListenerContext context)
    {
        HttpListenerResponse response = context.Response;

        response.ContentType = assetType;

        int index = 0;
        while(index < asset.bytes.Length)
        {
            int bytesLeft = asset.bytes.Length - index;

            response.OutputStream.Write(asset.bytes, index, Math.Min((int)writeStaggerCount, bytesLeft));
            index += Math.Min((int)writeStaggerCount, bytesLeft);

            if (index >= asset.bytes.Length)
                break;

            yield return null;
        }

        yield break;
    }

#endif

    [SerializeField]
    private TextAsset asset;

    [SerializeField]
    private string assetType = "text/html";

    [SerializeField]
    private uint writeStaggerCount = 4096;
}

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EasyQRCode : MonoBehaviour {
	
	public string textToEncode;
    public Color darkColor = Color.black;
    public Color lightColor = Color.white;
    public Text textOutput;

    void Start()
    {
		#if UNITY_STANDALONE || UNITY_EDITOR
        // Example usage of QR Generator
        // The text can be any string, link or other QR Code supported string

		textToEncode = "http://"+(Network.player.ipAddress)+":8080/HeistNight/start";
        Texture2D qrTexture = QRGenerator.EncodeString(textToEncode, darkColor, lightColor);

        // Set the generated texture as a new sqrite to use on a UI Image element 
		GetComponent<Image> ().sprite = Sprite.Create(qrTexture,  new Rect(0, 0, qrTexture.width, qrTexture.height), new Vector2(0.5f, 0.5f));

        if (textOutput != null)
        {
            textOutput.text = textToEncode;
        }

    #endif
    }
}

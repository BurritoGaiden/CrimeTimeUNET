using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RenderTextureToSprite : MonoBehaviour {

	[SerializeField]
	private RenderTexture renTex;

	// Use this for initialization
	void Start () {
		Texture2D myTexture2D = new Texture2D (renTex.width, renTex.height, TextureFormat.RGB24, false);
		myTexture2D.ReadPixels(new Rect(0, 0, renTex.width, renTex.height), 0, 0);
		myTexture2D.Apply();

		GetComponent<Image> ().sprite = Sprite.Create(myTexture2D,  new Rect(0, 0, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f));

	}


}

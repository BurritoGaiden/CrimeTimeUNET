using UnityEngine;
using System.Collections;

public class RenderToPNG : MonoBehaviour {

	[SerializeField]
	private Camera virtuCamera;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MakeSquarePngFromOurVirtualThingy()
	{
		// capture the virtuCam and save it as a square PNG.
		
		int sqr = 512;
		
		virtuCamera.GetComponent<Camera>().aspect = 1.0f;
		// recall that the height is now the "actual" size from now on
		
		RenderTexture tempRT = new RenderTexture(sqr,sqr, 24 );
		// the 24 can be 0,16,24, formats like
		// RenderTextureFormat.Default, ARGB32 etc.
		
		virtuCamera.GetComponent<Camera>().targetTexture = tempRT;
		virtuCamera.GetComponent<Camera>().Render();
		
		RenderTexture.active = tempRT;
		Texture2D virtualPhoto =
			new Texture2D(sqr,sqr, TextureFormat.RGB24, false);
		// false, meaning no need for mipmaps
		virtualPhoto.ReadPixels( new Rect(0, 0, sqr,sqr), 0, 0);
		
		RenderTexture.active = null; //can help avoid errors 
		virtuCamera.GetComponent<Camera>().targetTexture = null;
		// consider ... Destroy(tempRT);
		
		byte[] bytes;
		bytes = virtualPhoto.EncodeToPNG();
		
		System.IO.File.WriteAllBytes(
			OurTempSquareImageLocation(), bytes );
		// virtualCam.SetActive(false); ... no great need for this.
		
		// now use the image, 
	}
	
	private string OurTempSquareImageLocation()
	{
		string r = "C:\\Users\\Tom\\Documents\\GitHub\\CrimeTimeUNET\\WebTemp" + "/p.png";
		return r;
	}


}

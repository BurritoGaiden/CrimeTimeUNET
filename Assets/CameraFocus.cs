using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {

    [SerializeField]
    private Camera isometicCamera;
    public Camera IsometricCamera
    {
        get { return isometicCamera; }
    }

	// Use this for initialization
	void Start () {
    }

    void Update()
    {
        //transform.RotateAround(transform.position, Vector3.up, Time.deltaTime);
    }

    public void SetCameraForMap(float mapDimX, float mapDimZ, float tileSize)
    {
        transform.position = new Vector3(mapDimX / 2.0f, 0, mapDimZ / 2.0f);
            float newSize = ((mapDimX + mapDimZ)*tileSize*0.5f)/ 2.0f;
            //newSize = (mapDimX) * (Mathf.Sin(Mathf.PI/4f) * Mathf.Cos(Mathf.PI/6));
            newSize += (newSize * 0.07f);
        IsometricCamera.orthographicSize = newSize;
    }



}

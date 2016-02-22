using UnityEngine;
using System.Collections;

public class PersistentQR : MonoBehaviour {

    private Canvas canvas;
    public Canvas Canvas
    {
        get { return canvas; }
    }

    [SerializeField]
    private RectTransform rect;

    private bool isUp = false;

	// Use this for initialization
	void Awake () {
        canvas = this.GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Enable(bool toDo)
    {
        if (toDo && !isUp)
        {
            isUp = true;
            StartCoroutine(EaseLerp(new Vector3(0, -Screen.height / 4.0f, 0), new Vector3(0, 0, 0), 1.0f));
        }
        else if(!toDo && isUp)
        {
            StartCoroutine(EaseLerp(new Vector3(0, 0, 0), new Vector3(0, -Screen.height / 4.0f, 0), 1.0f));
            isUp = false;
        }

    }

    public IEnumerator EaseLerp(Vector3 startpos, Vector3 endpos, float seconds)
    {
        Debug.Log("LERPING AND SCRATCHING");
        rect.position = startpos;
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            rect.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t))));
            yield return null;
        }
        rect.position = endpos;
    }

    public void OnGameStateHasChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                canvas.enabled = false;
                break;
            case GameState.CharacterSelect:
                canvas.enabled = true;
                this.Enable(true);
                break;
            case GameState.GameBegin:
                this.Enable(false);
                break;
        }
    }
}

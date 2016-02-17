using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Intertitle : Singleton<Intertitle> {

    [SerializeField]
    private RectTransform card;
    private CanvasGroup cr;
    [SerializeField]
    private Text title;
    public Text Title
    {
        get { return title; }
    }
    [SerializeField]
    private Text subtitle;
    public Text Subtitle
    {
        get { return subtitle; }
    }
    [SerializeField]
    private Image QR;

    private RectTransform content;

    private bool inUse = false;


    // Use this for initialization
    void Awake () {
        cr = GetComponent<CanvasGroup>();
        card.gameObject.SetActive(false);
        cr.alpha = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void QRMode()
    {
        QR.enabled = true;
        subtitle.enabled = false;
    }

    public void TextMode()
    {
        QR.enabled = false;
        subtitle.enabled = true;
    }

    // Helper Coroutines

    public IEnumerator EaseLerp(Vector3 startpos, Vector3 endpos, float seconds)
    {
        while (inUse)
            yield return new WaitForEndOfFrame();
        inUse = true;
        card.position = startpos;
        card.gameObject.SetActive(true);
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            card.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t))));
            yield return null;
        }
        card.position = endpos;
        inUse = false;
    }

    public IEnumerator FromBottomToCenter(float seconds)
    {
        yield return StartCoroutine(AlphaLerp(0.0f, 1.0f, 0.75f * seconds));
        yield return StartCoroutine(EaseLerp(new Vector3(Screen.width / 2.0f, -Screen.height, 0), new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0), seconds));
    }
    public IEnumerator FromCenterToTop(float seconds)
    {
        yield return StartCoroutine(EaseLerp(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0), new Vector3(Screen.width / 2.0f, 2 * Screen.height, 0), seconds));
        yield return StartCoroutine(AlphaLerp(1.0f, 0.0f, 0.75f * seconds));

    }

    public IEnumerator AlphaLerp(float from, float to, float seconds)
    {
        while (inUse)
            yield return new WaitForEndOfFrame();
        inUse = true;
        cr.alpha = from;
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            cr.alpha = Mathf.Lerp(from, to, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
        cr.alpha = to;
        inUse = false;
    }
}

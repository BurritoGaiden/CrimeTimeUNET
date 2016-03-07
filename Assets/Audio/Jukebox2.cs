using UnityEngine;
using System.Collections;

public class Jukebox2 : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] drumFills;
    [SerializeField]
    private AudioClip[] bassFills;

    [SerializeField]
    private AudioClip charSelectMusic;

    private AudioSource[] sources;

    private bool onTitleMenu = true;

    void Awake()
    {
        sources = GetComponents<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(warmUp());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator warmUp()
    {
        sources[0].clip = bassFills[Random.Range(0, bassFills.Length)];
        sources[1].clip = drumFills[Random.Range(0, drumFills.Length)];
        sources[0].Play();
        sources[1].Play();
        yield return new WaitForSeconds(sources[0].clip.length - 0.075f);
        if (onTitleMenu)
            StartCoroutine(warmUp());
        else
        {
            sources[0].clip = charSelectMusic;
            sources[0].Play();
            sources[0].loop = true;
        }

    }

    public void OnGameStateHasChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.CharacterSelect:
                onTitleMenu = false;
                break;
        }
    }
}
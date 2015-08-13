using UnityEngine;
using System.Collections;

public class Jukebox : MonoBehaviour {

	[SerializeField]
	private AudioClip[] trackList;

	[SerializeField]
	private AudioSource[] decks = new AudioSource[2];

	private bool deck1InUse = true;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}

	public void ChangeTrack(int trackNumber)
	{
		StartCoroutine(ChangeMusic(trackList[trackNumber]));
	}

	private IEnumerator ChangeMusic(AudioClip nextTrack)
	{
		float fTimeCounter = 0f;

		if (deck1InUse) {
			decks[1].clip = nextTrack;
			decks [1].UnPause ();
			while (!(Mathf.Approximately(fTimeCounter, 1f))) {
				fTimeCounter = Mathf.Clamp01 (fTimeCounter + Time.deltaTime);
				decks [0].volume = 1f - fTimeCounter;
				decks [1].volume = fTimeCounter;
				yield return new WaitForSeconds (0.02f);
			}
		} else {
			decks[0].clip = nextTrack;
			decks [0].UnPause ();
			while (!(Mathf.Approximately(fTimeCounter, 1f))) {
				fTimeCounter = Mathf.Clamp01 (fTimeCounter + Time.deltaTime);
				decks [1].volume = 1f - fTimeCounter;
				decks [0].volume = fTimeCounter;
				yield return new WaitForSeconds (0.02f);
			}
		}
		deck1InUse = !deck1InUse; 
	}

}

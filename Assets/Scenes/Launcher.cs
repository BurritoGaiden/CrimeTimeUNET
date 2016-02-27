using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameStateManager.Instance.GameState = GameState.MainMenu;
    }
}

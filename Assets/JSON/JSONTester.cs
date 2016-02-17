using UnityEngine;
using System.Collections.Generic;

public class JSONTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        /*

        List<PlayerJSON> pj = new List<PlayerJSON>();
        foreach (CommandPanel cp in PlayerRegisterRule.PlayerRegister.Values)
        {
            pj.Add((PlayerJSON)cp.ToJSON());
        }

        List<CharacterJSON> cj = new List<CharacterJSON>();
        foreach (CharacterBehavior cb in FindObjectsOfType<CharacterBehavior>())
        {
            cj.Add((CharacterJSON)cb.ToJSON());
        }
        Heartbeat h = new Heartbeat();
        h.state = GameState.GameBegin;
        h.players = pj.ToArray();
        h.characters = cj.ToArray();

        Debug.Log(JsonUtility.ToJson(h));

        */
    }
}

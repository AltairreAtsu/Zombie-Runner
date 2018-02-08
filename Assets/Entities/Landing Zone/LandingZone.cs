using UnityEngine;

public class LandingZone : MonoBehaviour
{

	public delegate void OnVictory();
	public OnVictory OnVictoryObservers;

    public bool winMode = false;
    private bool playerTriggering = false;

	void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && winMode || playerTriggering && winMode)
            OnVictoryObservers();
        else if (coll.tag == "Player")
            playerTriggering = true;
	}

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
            playerTriggering = false;
    }
}

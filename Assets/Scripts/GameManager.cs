using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private TourBuilderScriptable tour;
    [SerializeField]
    private GameObject player;

    public static GameManager instance = null;

    private IDictionary<int, NodeBehaviour> nodes;

    private NodeBehaviour start;

    // Use this for initialization
    void Awake () {
		if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        //nodes = new Dictionary<int, NodeBehaviour>();
        //tour.SetupGM(instance);
	}
	
    /*
	public void Setup (IDictionary<int, NodeBehaviour> n, int startId) {
        nodes = n;
        Debug.Log(nodes.Count);
        start = nodes[startId];
        TeleportToNode(start);
	}
    */

    public void TeleportToNode(NodeBehaviour n)
    {
        player.transform.SetPositionAndRotation(n.transform.position, n.transform.rotation);
    }
}

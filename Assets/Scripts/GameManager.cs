using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTour.Serialize;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public IDictionary<int, NodeBehaviour> nodes;
    

    [SerializeField]
    private GameObject tourPrefab;
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private GameObject answerPrefab;

    private GameObject tourObj;
    private NodeBehaviour start;
    private TourBehaviour tour;

    // Use this for initialization
    void Awake () {
		if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if(nodes == null)
            nodes = new Dictionary<int, NodeBehaviour>();
	}
	
	
    public void BuildTour(Tour t)
    {
        if (nodes == null)
            nodes = new Dictionary<int, NodeBehaviour>();
        StartCoroutine(SetupTour(t));
    }

    private IEnumerator SetupTour(Tour t)
    {
        tourObj = Instantiate(tourPrefab);
        tour = tourObj.GetComponent<TourBehaviour>();
        tour.Setup(t);
        nodes = new Dictionary<int, NodeBehaviour>();
        start = BuildNode(t.startPoint);
        yield return null;
    }

    public NodeBehaviour BuildNode(Node toBuild)
    {
        GameObject nodeObj = Instantiate(nodePrefab, tourObj.transform);
        NodeBehaviour toReturn = nodeObj.GetComponent<NodeBehaviour>();
        //Constructs the node based on the deserialized node in toBuild and adds it to nodes
        toReturn.Setup(toBuild);
        return toReturn;

    }

    public AnswerBehaviour BuildAnswer(Destination d, RectTransform t)
    {
        GameObject ansObj = Instantiate(answerPrefab, t);
        AnswerBehaviour toReturn = ansObj.GetComponent<AnswerBehaviour>();
        toReturn.Setup(d);

        return toReturn;

    }


}

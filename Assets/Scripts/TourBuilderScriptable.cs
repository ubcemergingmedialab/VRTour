using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRTour.Serialize;

[CreateAssetMenu(fileName = "Tour", menuName = "Build Tour", order = 1)]
public class TourBuilderScriptable : ScriptableObject
{
    public GameObject tourPrefab;
    public GameObject nodePrefab;
    public GameObject answerPrefab;

    public IDictionary<int, NodeBehaviour> nodes;

    public Tour toBuild;

    private GameObject tourObj;
    private NodeBehaviour start;
    private TourBehaviour tour;

    public void LoadTour(Tour t)
    {
        toBuild = t;
    }

    public void BuildTour()
    {
        nodes = new Dictionary<int, NodeBehaviour>();
        SetupTour(toBuild);
    }


    private void SetupTour(Tour t)
    {
        tourObj = Instantiate(tourPrefab);
        tour = tourObj.GetComponent<TourBehaviour>();
        tour.Setup(t, this);
        nodes = new Dictionary<int, NodeBehaviour>();
        start = BuildNode(t.startPoint);
    }

    public NodeBehaviour BuildNode(Node toBuild)
    {
        GameObject nodeObj = Instantiate(nodePrefab, tourObj.transform);
        NodeBehaviour toReturn = nodeObj.GetComponent<NodeBehaviour>();
        //Constructs the node based on the deserialized node in toBuild and adds it to nodes
        toReturn.Setup(toBuild, this);
        return toReturn;

    }

    public AnswerBehaviour BuildAnswer(Destination d, RectTransform t)
    {
        GameObject ansObj = Instantiate(answerPrefab, t);
        AnswerBehaviour toReturn = ansObj.GetComponent<AnswerBehaviour>();
        toReturn.Setup(d, this);

        return toReturn;

    }
}

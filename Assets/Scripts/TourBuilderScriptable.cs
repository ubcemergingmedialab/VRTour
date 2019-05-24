using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRTour.Serialize;

namespace VRTour
{
    /// <summary>
    /// Scriptable object for building a tour. Needs a prefab for tour, node and answers, and loads a set tour
    /// </summary>
    [CreateAssetMenu(fileName = "Tour", menuName = "Build Tour", order = 1)]
    public class TourBuilderScriptable : ScriptableObject
    {
        public GameObject tourPrefab;
        public GameObject nodePrefab;
        public GameObject answerPrefab;

        public IDictionary<int, NodeBehaviour> nodes;

        public Tour toBuild;

        private GameObject tourObj;

        /// <summary>
        /// Resets to empty tour
        /// </summary>
        public void ResetTour()
        {
            toBuild = new Tour();
        }

        /// <summary>
        /// Loads the given tour
        /// </summary>
        /// <param name="t">Tour to be build</param>
        public void LoadTour(Tour t)
        {
            toBuild = t;
        }

        /// <summary>
        /// Builds the tour in the scene
        /// </summary>
        public void BuildTour()
        {
            SetupTour();

        }

        /// <summary>
        /// Recursively builds each node in the tour, under the parent TourPrefab object
        /// </summary>
        private void SetupTour()
        {
            tourObj = Instantiate(tourPrefab);
            TourBehaviour tour = tourObj.GetComponent<TourBehaviour>();
            tour.Setup(toBuild, this);
            nodes = new Dictionary<int, NodeBehaviour>();

            BuildNodes(Utility.BuildDictionaryFromArray(toBuild.nodes));
        }

        private void BuildNodes(Dictionary<int, Node> nodesToBuild)
        {
            foreach(Node n in nodesToBuild.Values)
            {
                nodes[n.nodeId] = BuildNode(n);
            }
        }

        /// <summary>
        /// Takes a deserialized node and sets it up in the scene
        /// </summary>
        /// <param name="toBuild">The Deserialized node structure to setup</param>
        /// <returns>the fully setup and built node object</returns>
        public NodeBehaviour BuildNode(Node toBuild)
        {
            GameObject nodeObj = Instantiate(nodePrefab, tourObj.transform);
            NodeBehaviour toReturn = nodeObj.GetComponent<NodeBehaviour>();
            //Constructs the node based on the deserialized node in toBuild and adds it to nodes
            toReturn.Setup(toBuild, this);
            return toReturn;

        }

        /// <summary>
        /// Takes a deserialized destination and sets up an answer button for it
        /// </summary>
        /// <param name="d">Deserialized destnation to setup</param>
        /// <param name="t">The transform to build the answer into</param>
        /// <returns></returns>
        public AnswerBehaviour BuildAnswer(Destination d, RectTransform t)
        {
            GameObject ansObj = Instantiate(answerPrefab, t);
            AnswerBehaviour toReturn = ansObj.GetComponent<AnswerBehaviour>();
            toReturn.Setup(d, this);

            return toReturn;

        }
        /*
        public void SetupGM(GameManager toSetup)
        {
            toSetup.Setup(nodes, toBuild.startPoint.nodeId);
        }
        */
    }
}

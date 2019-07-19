using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

namespace VRTour
{
    public class NodeBehaviour : MonoBehaviour
    {

        [SerializeField]
        private Text questionText;
        [SerializeField]
        private RectTransform destinationPanel;
        [SerializeField]
        private GameObject sphere;

        private Material defaultmat;
        [SerializeField]
        public Texture texture;
        private TourBuilderScriptable instance;
        private Node node;

        private void Start()
        {
            defaultmat = sphere.GetComponent<MeshRenderer>().material;
            defaultmat.SetTexture("_MainTex", texture);
        }

        /// <summary>
        /// Setup the node prefab with specific deserialized object n
        /// </summary>
        /// <param name="n">Deserialized Node object to setup</param>
        /// <param name="tb">Tour Builder that is building this node</param>
        public void Setup(Node n, TourBuilderScriptable tb)
        {
            instance = tb;
            node = n;
            questionText.text = node.label;

            transform.SetPositionAndRotation(node.position, Quaternion.Euler(node.rotation));
            instance.nodes[node.nodeId] = this;
            
            SetupNode();
        }

        /// <summary>
        /// Build out each destination object. 
        /// </summary>
        private void SetupNode()
        {
            int numAns = node.answers.Length;
            float offset = destinationPanel.sizeDelta.y / numAns;
            float curOffset = 0;
            foreach (Destination d in node.answers)
            {
                AnswerBehaviour ans = instance.BuildAnswer(d, destinationPanel);
                RectTransform curAns = (RectTransform)ans.transform;
                curAns.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, curOffset, curAns.sizeDelta.y);
                curOffset += offset;
            }
        }
    }
}

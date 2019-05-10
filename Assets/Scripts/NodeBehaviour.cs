using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

public class NodeBehaviour : MonoBehaviour {

    [SerializeField]
    private Text questionText;
    [SerializeField]
    private RectTransform destinationPanel;

    private Node node;

    /// <summary>
    /// Setup the node prefab with specific deserialized object n
    /// </summary>
    /// <param name="n">Deserialized Node object to setup</param>
    public void Setup(Node n)
    {
        node = n;
        questionText.text = node.label;

        transform.SetPositionAndRotation(node.position, Quaternion.Euler(node.rotation));
        GameManager.instance.nodes[node.nodeId] = this;

        StartCoroutine(SetupNode());
    }

    /// <summary>
    /// Build out each destination object. This is a coroutine because its recursive, so it could take a long time for each destination. 
    /// </summary>
    /// <returns>Null</returns>
    private IEnumerator SetupNode()
    {
        int numAns = node.answers.Length;
        float offset = destinationPanel.sizeDelta.y / numAns;
        float curOffset = 0;
        foreach (Destination d in node.answers)
        {
            AnswerBehaviour ans = GameManager.instance.BuildAnswer(d, destinationPanel);
            RectTransform curAns = (RectTransform)ans.transform;
            curAns.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, curOffset, curAns.sizeDelta.y);
            curOffset += offset;
            yield return null;
        }
    }
}

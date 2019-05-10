using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

[RequireComponent(typeof(Button))]
public class AnswerBehaviour : MonoBehaviour {

    [SerializeField]
    private NodeBehaviour target;
    [SerializeField]
    private Text ansText;

    private Button b;

    #region PRIVATE_MEMBER_VARIABLES
    private Destination dest;
    #endregion //PRIVATE_MEMBER_VARIABLES

    private void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log("test");
       //Some kind of teleport action goes here
    }

    public void Setup(Destination d)
    {
        dest = d;
        ansText.text = dest.label;
        StartCoroutine(SetupAnswer());
    }

    private IEnumerator SetupAnswer()
    {
        NodeBehaviour value;
        if (GameManager.instance.nodes.TryGetValue(dest.dest.nodeId, out value))
        {
            yield return null;
        }
        else
        {
            value = GameManager.instance.BuildNode(dest.dest);
            yield return null;
        }

        target = value;
    }
}

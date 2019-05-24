using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;
namespace VRTour
{
    [RequireComponent(typeof(Button))]
    public class AnswerBehaviour : MonoBehaviour
    {

        [SerializeField]
        private NodeBehaviour target;
        [SerializeField]
        private Text ansText;

        public TourBuilderScriptable instance;
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
            GameManager.instance.TeleportToNode(target);
        }

        public void Setup(Destination d, TourBuilderScriptable tb)
        {
            instance = tb;
            dest = d;
            ansText.text = dest.label;
            target = tb.nodes[d.dest];
        }
    }
}

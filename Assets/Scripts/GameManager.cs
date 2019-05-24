using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRTour
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject player;

        public static GameManager instance = null;


        // Use this for initialization
        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        public void TeleportToNode(NodeBehaviour n)
        {
            player.transform.SetPositionAndRotation(n.transform.position, n.transform.rotation);
        }
    }
}

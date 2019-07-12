using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRTour
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject player;
        public CanvasGroup cg;
        public static GameManager instance = null;
        public float lerpTime;
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
            Quaternion noderotation = n.transform.rotation;
            cg.gameObject.SetActive(true);
            StartCoroutine(FadeCamera(n.transform.position, Quaternion.Euler(noderotation.eulerAngles.x, noderotation.eulerAngles.z, player.transform.eulerAngles.y)));
           
            
        }

        private IEnumerator FadeCamera(Vector3 position, Quaternion rot)
        {

            yield return FadeCanvasGroup(0,1,lerpTime);
            player.transform.SetPositionAndRotation(position, rot);
            yield return FadeCanvasGroup(1,0,lerpTime);
            cg.gameObject.SetActive(false);
        }

        private IEnumerator FadeCanvasGroup(float start, float end, float lerpTime = 0.5f)
        {
            float _timeStartedLerping = Time.time;
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / lerpTime;

            while(true)
            {
                timeSinceStarted = Time.time - _timeStartedLerping;
                percentageComplete = timeSinceStarted / lerpTime;
                float currentValue = Mathf.Lerp(start, end, percentageComplete);
                cg.alpha = currentValue;
                if (percentageComplete >= 1) break;
                yield return new WaitForFixedUpdate();
                
            }
            yield return null;
        }
    }
}

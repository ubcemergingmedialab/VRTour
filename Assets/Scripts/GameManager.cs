using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRTour
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject player;
        public CanvasGroup uiElement;
        public GameObject Node0;

        public static GameManager instance = null;

        // Use this for initialization
        private void Start()
        {
            player.transform.SetPositionAndRotation(Node0.transform.position, Node0.transform.rotation);
        }
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
            StartCoroutine(MyCoroutine(n.transform.position,n.transform.rotation));
        }

        IEnumerator MyCoroutine(Vector3 pos, Quaternion rot)
        {
            yield return FadeIn();
            player.transform.SetPositionAndRotation(pos, rot);
            yield return FadeOut();

        }

        IEnumerator FadeIn()
        {
            yield return FadeCanvasGroup(uiElement, uiElement.alpha, 1,  0.5f);
        }

        IEnumerator FadeOut()
        {
            yield return FadeCanvasGroup(uiElement, uiElement.alpha, 0, 0.5f);
        }

        public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
        {
            float _timeStartedLerping = Time.time;
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / lerpTime;

            while (true)
            {
                timeSinceStarted = Time.time - _timeStartedLerping;
                percentageComplete = timeSinceStarted / lerpTime;

                float currentValue = Mathf.Lerp(start, end, percentageComplete);

                cg.alpha = currentValue;

                if (percentageComplete >= 1) break;

                //

                yield return new WaitForFixedUpdate();
            }

            print("done");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class Transferer : MonoBehaviour
    {
        public void TransferResourceToInventory(Resources.Resource res, System.Action onTransferCompleted)
        {
            StartCoroutine(lerpObjectToInventory(res.transform, () => {
                res.gameObject.SetActive(false);
                onTransferCompleted?.Invoke();
            }));
        }

        private IEnumerator lerpObjectToInventory(Transform resource, System.Action onEnd)
        {
            float lerpTime = 0.4f;
            float t = 0f;
            Vector3 startPosition = resource.position;
            Vector3 startScale = resource.localScale;

            while(t < lerpTime)
            {
                t += Time.deltaTime;

                resource.position = Vector3.Lerp(startPosition, transform.position, EasingFunction.EaseInOutCirc(0f, 1f, t / lerpTime));
                resource.localScale = Vector3.Lerp(startScale, Vector3.zero, EasingFunction.EaseInOutCirc(0f, 1f, t / lerpTime));

                yield return null;
            }

            onEnd?.Invoke();
        }
    }
}

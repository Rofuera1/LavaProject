using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcePlaces
{
    public class Producer : MonoBehaviour
    {
        [SerializeField]
        private Resources.Resource _producingResource;
        [SerializeField]
        private Settings.ProducerSettings _settings;
        [SerializeField]
        private Transform _groundTransform;
        [SerializeField]
        private Transform _producerNozzleAtGroundTransform;

        private int leftToProduce;
        public bool CanProduce => leftToProduce > 0;
        public int ProduceCapacity => _settings.ProduceCapacity;
        public int ProduceAtOnce => _settings.ProduceAtOnce;
        public int LeftToProduce => leftToProduce;

        public UnityEngine.Events.UnityEvent OnProduced { get; set; } = new UnityEngine.Events.UnityEvent();
        public UnityEngine.Events.UnityEvent OnRestored { get; set; } = new UnityEngine.Events.UnityEvent();
        public UnityEngine.Events.UnityEvent OnNoAvalilibleResources { get; set; } = new UnityEngine.Events.UnityEvent();
        public UnityEngine.Events.UnityEvent OnAvailbleToProduce { get; set; } = new UnityEngine.Events.UnityEvent();

        private bool isRestoringResources;

        private void Awake()
        {
            leftToProduce = _settings.ProduceCapacity;
        }

        public void Produce()
        {
            int produceNow = Mathf.Min(leftToProduce, _settings.ProduceAtOnce);

            leftToProduce -= produceNow;

            for(int i = 0; i < produceNow; i++)
                createResource();

            OnProduced?.Invoke();

            if(!isRestoringResources)
                StartCoroutine(restoreResource());

            if (leftToProduce == 0)
                OnNoAvalilibleResources?.Invoke();
        }

        private IEnumerator restoreResource()
        {
            isRestoringResources = true;

            while(leftToProduce < ProduceCapacity)
            {
                yield return new WaitForSeconds(_settings.CountdownForOne);

                if (leftToProduce == 0)
                    OnAvailbleToProduce?.Invoke();

                leftToProduce++;
                OnRestored?.Invoke();
            }

            isRestoringResources = false;
        }

        private void createResource()
        {
            Resources.Resource res = Instantiate(_producingResource);
            res.transform.position = _producerNozzleAtGroundTransform.position;
            res.Created(this);

            Vector3 groundPosition = findPointToMoveResource();
            StartCoroutine(lerpResourceToGround(res.transform, groundPosition, () => { res.PlacedOnGround(); }));
        }

        private Vector3 findPointToMoveResource()
        {
            float radius = _settings.RadiusForResourceEmission;
            float xPos = Random.Range(-radius, radius);
            float zPos = Mathf.Sqrt(radius * radius - xPos * xPos) * (Random.Range(0, 2) == 0 ? 1 : -1);

            return _groundTransform.TransformPoint(new Vector3(xPos, 0, zPos));
        }

        private IEnumerator lerpResourceToGround(Transform res, Vector3 endPosition, System.Action onEndAction)
        {
            float time = 0.82f;
            float lieOnGroundTime = 0.15f;
            float t = 0f;
            Vector3 startPos = res.transform.position;

            float xDelta = endPosition.x - startPos.x;
            float zDelta = endPosition.z - startPos.z;
            float distance = Mathf.Sqrt(xDelta * xDelta + zDelta * zDelta);
            float groundYPos = _groundTransform.position.y;

            while(t < time)
            {
                t += Time.deltaTime;

                float distanceCurrent = Mathf.Lerp(0f, distance, EasingFunction.Linear(0f, 1f, t / time));
                float xCurrent = Mathf.Lerp(startPos.x, endPosition.x, distanceCurrent / distance);
                float zCurrent = Mathf.Lerp(startPos.z, endPosition.z, distanceCurrent / distance);
                float yCurrent = -Mathf.Pow((distanceCurrent - distance * 0.5f), 2f) + Mathf.Pow((distance * 0.5f), 2f) + groundYPos;
                res.transform.position = new Vector3(xCurrent, yCurrent, zCurrent);

                yield return null;
            }

            yield return new WaitForSeconds(lieOnGroundTime);

            onEndAction?.Invoke();
        }
    }
}

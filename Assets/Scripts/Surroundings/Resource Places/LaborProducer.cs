using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcePlaces
{
    public class LaborProducer : MonoBehaviour
    {
        [SerializeField]
        private Producer _producer;
        [SerializeField]
        private Settings.LaborSettings _settings;

        private float timeOnLabor;
        private float timeForProducing;
        private IEnumerator laborCoroutine;

        public UnityEngine.Events.UnityEvent OnChangedProgress { get; set; } = new UnityEngine.Events.UnityEvent();
        public UnityEngine.Events.UnityEvent OnStartedLabor { get; set; } = new UnityEngine.Events.UnityEvent();
        public UnityEngine.Events.UnityEvent OnQuitLabor { get; set; } = new UnityEngine.Events.UnityEvent();
        public float LaborProgress => timeOnLabor / timeForProducing;

        private void Awake()
        {
            timeOnLabor = 0f;
            timeForProducing = _settings.SecondsToGiveOne;
        }

        public void OnStartLabor()
        {
            OnStartedLabor?.Invoke();

            laborCoroutine = laboring();
            StartCoroutine(laborCoroutine);
        }

        private IEnumerator laboring()
        {
            while(true)
            {
                if(_producer.CanProduce)
                {
                    timeOnLabor += Time.deltaTime;
                    OnChangedProgress?.Invoke();
                }

                if(timeOnLabor >= timeForProducing)
                {
                    timeOnLabor = 0f;
                    produceResource();
                }

                yield return null;
            }
        }

        private void produceResource()
        {
            if (_producer.CanProduce)
                _producer.Produce();
        }

        public void OnStopLabor()
        {
            if (laborCoroutine != null) StopCoroutine(laborCoroutine);
            OnQuitLabor?.Invoke();
        }
    }
}

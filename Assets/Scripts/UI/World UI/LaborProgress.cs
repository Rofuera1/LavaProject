using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LaborProgress : MonoBehaviour
    {
        [SerializeField]
        private ResourcePlaces.LaborProducer _laborProducer;
        [SerializeField]
        private ResourcePlaces.Producer _producer;
        [SerializeField]
        private Image _sliderProgress;
        [SerializeField]
        private TMPro.TMP_Text _textProgress;

        private int produceCapacity;
        private int produceAtOnce;
        private Tweener sliderTween;

        [Space]
        [SerializeField]
        private CanvasGroup _canvasG;
        private float startYPosition;
        private Tweener showingTweener;
        

        private void Awake()
        {
            produceCapacity = _producer.ProduceCapacity;
            produceAtOnce = _producer.ProduceAtOnce;

            _laborProducer.OnChangedProgress.AddListener(updateSlider);
            _producer.OnProduced.AddListener(updateText);
            _producer.OnRestored.AddListener(updateText);

            startYPosition = transform.localPosition.y;
            _laborProducer.OnStartedLabor.AddListener(startedLabor);
            _laborProducer.OnQuitLabor.AddListener(stoppedLabor);

            stoppedLabor();

            loadInfoAtStart();
        }

        private void loadInfoAtStart()
        {
            _textProgress.text = "x" + produceAtOnce.ToString() + "/" + produceCapacity.ToString();
        }

        private void updateSlider()
        {
            if (sliderTween != null)
                sliderTween.Kill();

            sliderTween = _sliderProgress.DOFillAmount(_laborProducer.LaborProgress, Mathf.Abs(_sliderProgress.fillAmount - _laborProducer.LaborProgress));
        }

        private void updateText()
        {
            produceCapacity = _producer.LeftToProduce;
            _textProgress.text = "x" + produceAtOnce.ToString() + "/" + produceCapacity.ToString();
        }

        private void startedLabor()
        {
            if (showingTweener != null) showingTweener.Kill();

            float timeDutarion = 0.5f;

            showingTweener = _canvasG.DOFade(1f, timeDutarion);
        }

        private void stoppedLabor()
        {
            if (showingTweener != null) showingTweener.Kill();

            float timeDutarion = 0.5f;

            showingTweener = _canvasG.DOFade(0f, timeDutarion);
        }
    }
}

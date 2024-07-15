using System.Collections;
using Enemy.Health;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Battle
{
    public class HealthProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;
        [SerializeField] private float _defaultSpeed = 2f;
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private UnityEvent<float> _onProgress;
        [SerializeField] private UnityEvent _onCompleted;
        [SerializeField] private Health _health;
        private Coroutine _animationCoroutine;

        private void Start()
        {
            if (_progressImage.type == Image.Type.Filled) return;
            enabled = false;
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(gameObject);
#endif
        }

        private void OnEnable()
        {
            _health.Changed += SetProgress;
        }

        private void OnDisable()
        {
            _health.Changed -= SetProgress;
        }

        public void SetProgress(float progress)
        {
            Debug.Log("SetProgress" + progress);
            SetProgress(progress, _defaultSpeed);
        }

        private void SetProgress(float progress, float speed)
        {
            if (progress is < 0 or > 1)
            {
                Debug.LogWarning(
                    $"Invalid progress passed, expected value is between 0 and 1, got {progress}. Clamping.");
                progress = Mathf.Clamp01(progress);
            }

            //    if (!(progress - _progressImage.fillAmount > 0.001)) return;
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
        }

        private IEnumerator AnimateProgress(float progress, float speed)
        {
            Debug.Log("progress" + progress);
            Debug.Log("speed" + speed);
            var time = 0f;
            var initialProgress = _progressImage.fillAmount;

            while (time < 1)
            {
                _progressImage.fillAmount = Mathf.Lerp(initialProgress, progress, time);
                time += Time.deltaTime * speed;

                _progressImage.color = _colorGradient.Evaluate(1 - _progressImage.fillAmount);

                _onProgress?.Invoke(_progressImage.fillAmount);
                yield return null;
            }

            _progressImage.fillAmount = progress;
            _progressImage.color = _colorGradient.Evaluate(1 - _progressImage.fillAmount);

            _onProgress?.Invoke(progress);
            _onCompleted?.Invoke();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Enemy.Health.View
{
    public class HealthView: MonoBehaviour
    {
        [SerializeField] private float _smoothDecreaseDuration = 0.5f;
        [SerializeField] private Health _health;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Color _damageHeathColor;
        [SerializeField] private AnimationCurve _colorBehaviour;
        [SerializeField] private Animator _heartAnimator;
        [SerializeField] private AnimationClip _heartPulseAnimation;

        private Color _originalHealthColor;


        private void Start()
        {
            _originalHealthColor = _healthText.color;
            _healthText.text = _health.MaxHealth.ToString("");
        }

        private void OnEnable()
        {
            _health.Changed += TakeDamage;
        }

        private void OnDisable()
        {
            _health.Changed -= TakeDamage;
        }

        private IEnumerator DecreaseHealthSmoothly(float target)
        {
            var elapsedTime = 0f;
            var previousValue = float.Parse(_healthText.text);

            while (elapsedTime < _smoothDecreaseDuration)
            {
                elapsedTime += Time.deltaTime;
                var normalizedPosition = elapsedTime / _smoothDecreaseDuration;
                var intermediateValue = Mathf.Lerp(previousValue, target, normalizedPosition);
                _healthText.text = intermediateValue.ToString("");
                _healthText.color = Color.Lerp(_originalHealthColor, _damageHeathColor,
                    _colorBehaviour.Evaluate(normalizedPosition));
                yield return null;
            }
        }

        private void TakeDamage(float currentHealth)
        {
            _heartAnimator.Play(_heartPulseAnimation.name);
            StartCoroutine(DecreaseHealthSmoothly(currentHealth));
        }
    }
}
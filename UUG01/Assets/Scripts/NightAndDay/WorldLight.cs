using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

namespace WorldTime
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
        private Light2D _light;

        [SerializeField]
        private WorldTime _worldTime;

        [SerializeField]
        private Gradient _gradient;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            _light.color = _gradient.Evaluate(PercentOfDay(newTime));
        }

        private float PercentOfDay(TimeSpan timeSpan)
        {
            return (float)timeSpan.TotalMinutes % WorldTimeConstants.MinutesInDay / WorldTimeConstants.MinutesInDay;
        }
    }
}
// Source: https://www.youtube.com/watch?v=0nq1ZFxuEJY

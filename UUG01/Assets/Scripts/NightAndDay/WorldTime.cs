using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;

        [SerializeField] private float _dayLength; // in seconds

        private TimeSpan _currentTime;

        public TimeSpan CurrentTime
        {
            get { return _currentTime; }
            private set
            {
                _currentTime = value;
                WorldTimeChanged?.Invoke(this, _currentTime);
            }
        }

        private float _minuteLength => _dayLength / WorldTimeConstants.MinutesInDay;

        private void Start()
        {
            StartCoroutine(AddMinute());
        }

        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLength);
            StartCoroutine(AddMinute());
        }
    }
}
// Source: https://www.youtube.com/watch?v=0nq1ZFxuEJY

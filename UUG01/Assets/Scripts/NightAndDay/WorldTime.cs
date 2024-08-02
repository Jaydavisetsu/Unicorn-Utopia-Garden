using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour, IDataPersistence // WORLDLIGHT.CS IS THE ONLY THING USING WORLDTIME.CS.
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;

        [SerializeField] private float _dayLength; // in seconds

        public TimeSpan _currentTime = TimeSpan.FromHours(06);
        private float _minuteLength => _dayLength / WorldTimeConstants.MinutesInDay;

        public void LoadData(GameData data) //Method from IDataPersistence.
        {
            this._dayLength = data.DayLength;
            this._currentTime = data.CurrentTime;
        }

        public void SaveData(GameData data) //Method from IDataPersistence.
        {
            data.DayLength = this._dayLength;
            data.CurrentTime = this._currentTime;
        }

        private void Start()
        {
            StartCoroutine(AddMinute());
        }

        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLength);
            //Debug.Log("WorldTime cs - " + "Hour : " + _currentTime.Hours + " Minute: " + _currentTime.Minutes);
            StartCoroutine(AddMinute());
        }
    }
}
// Source: https://www.youtube.com/watch?v=0nq1ZFxuEJY

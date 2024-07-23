using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace WorldTime
{
    public class WorldTimeWatcher : MonoBehaviour
    {
        [SerializeField] private WorldTime worldTime;

        [SerializeField] private List<Schedule> schedule;

        private void Start()
        {
            worldTime.WorldTimeChanged += CheckSchedule;
        }

        private void OnDestroy()
        {
            worldTime.WorldTimeChanged -= CheckSchedule;
        }

        private void CheckSchedule(object sender, TimeSpan newTime)
        {
            var _schedule =
                schedule.FirstOrDefault(s =>
                    s.Hour == newTime.Hours &&
                    s.Minute == newTime.Minutes);
            _schedule?.Action?.Invoke();
        }

        [Serializable]
        private class Schedule
        {
            public int Hour;
            public int Minute;
            public UnityEvent Action;
        }
    }
}
// Source: https://www.youtube.com/watch?v=0nq1ZFxuEJY

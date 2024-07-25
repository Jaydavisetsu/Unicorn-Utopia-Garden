using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static WorldTime.WorldTimeWatcher;

namespace WorldTime
{
    public class WorldTimeWatcher : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private WorldTime worldTime;
        [SerializeField] public Image imageComponent; // Assign in Inspector
        [SerializeField] public List<Schedule> schedule;

        private void Awake()
        {
            imageComponent = GetComponent<Image>();
            worldTime.WorldTimeChanged += CheckSchedule; // Subscribe to the WorldTimeChanged event

        }
        private void Start()
        {
            //worldTime.WorldTimeChanged += CheckSchedule; // Subscribe to the WorldTimeChanged event
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
        public class Schedule
        {
            public int Hour;
            public int Minute;
            public UnityEvent Action;
        }
        public void LoadData(GameData data) //Method from IDataPersistence.
        {
            if (schedule == null)
            {
                schedule = new List<Schedule>();
            }
            else if (schedule != null)
            {
                data.Schedule.Clear();

            }

            foreach (var kvp in data.Schedule)
            {
                //CurrencyAmounts.Add(kvp.Key, kvp.Value);
                schedule.Add(kvp);
            }
        }

        public void SaveData(GameData data) //Method from IDataPersistence.
        {
            data.Schedule = new List<Schedule>();

            foreach (var kvp in schedule)
            {
                data.Schedule.Add(kvp);
            }
        }
    }
}
// Source: https://www.youtube.com/watch?v=0nq1ZFxuEJY
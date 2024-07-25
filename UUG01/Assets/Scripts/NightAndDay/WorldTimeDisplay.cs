using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace WorldTime // THIS CLASS IS NOT NEEDED ANYMORE. TIME IS HANDLED BY GAMETIMESTAMP.CS. WORLDLIGHT.CS IS THE ONLY THING USING WORLDTIME.CS.
{
    /*[RequireComponent(typeof(TMP_Text))]
    public class WorldTimeDisplay : MonoBehaviour
    {
        [SerializeField] private WorldTime _worldTime;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            _text.SetText(newTime.ToString(@"hh\:mm"));
        }
    }*/
}
// Source: https://www.youtube.com/watch?v=0nq1ZFxuEJY

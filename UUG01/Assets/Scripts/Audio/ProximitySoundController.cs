using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
    public class ProximitySoundController : MonoBehaviour // This class is for areas the player will only walk along.
    {
        public static ProximitySoundController Instance; //Creating a static instance of this code to easily access if from anywhere.

        [Tooltip("Cinemachine Path to Follow")]
        public CinemachinePathBase m_Path;

        [Tooltip("Character to Track")]
        public GameObject Player;

        [Tooltip("Activation Distance")]
        public float ActivationDistance; // Distance at which sound should activate.

        public float FadeDuration = 2.0f; // Duration of fade in and fade out.
        public string SoundName; // The name of the river sound is set in the inspector.
        float m_Position; // The position along the path to set the cart to in path units.
        public float targetVolume; // Target volume for the river sound.


        private CinemachinePathBase.PositionUnits m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;

        private bool IsPlaying = false; //.Play() and .Stop() methods would be called continuously every frame while the player is within or outside the activation distance, which is unnecessary and can be computationally expensive.

        void Update()
        {
            // Find closest point to the player along the path.
            SetCartPosition(m_Path.FindClosestPoint(Player.transform.position, 0, -1, 10));
            SoundProximity();
        }

        void SetCartPosition(float distanceAlongPath) // Set cart's position to closest point.
        {
            m_Position = m_Path.StandardizeUnit(distanceAlongPath, m_PositionUnits);
            transform.position = m_Path.EvaluatePositionAtUnit(m_Position, m_PositionUnits);
            transform.rotation = m_Path.EvaluateOrientationAtUnit(m_Position, m_PositionUnits);
        }

        void SoundProximity()
        {
            if (Player != null)
            {
                float distance = Vector3.Distance(transform.position, Player.transform.position);

                if (distance <= ActivationDistance && !IsPlaying)
                {
                    AudioManager.Instance.FadeInSFX(SoundName, FadeDuration, targetVolume);
                    IsPlaying = true;
                }
                else if (distance > ActivationDistance && IsPlaying)
                {
                    AudioManager.Instance.FadeOutSFX(FadeDuration);
                    IsPlaying = false;
                }
            }
        }
    }
}

// Source: https://www.youtube.com/watch?v=5lEzchlQYKk
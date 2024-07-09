using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFunctions : MonoBehaviour
{
    public void UIClickSound() // This script is used among all UI buttons.
    {
        AudioManager.Instance.PlaySFX("Selecting");
    }
}

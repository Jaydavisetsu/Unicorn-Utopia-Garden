using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// CURRENTLY NOT BEING USED. WILL KEEP INCASE OF FUTURE NEED.
public class UI_Player : MonoBehaviour {

    private TextMeshProUGUI goldText;

    private void Awake() {
        goldText = transform.Find("goldText").GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        UpdateText();

        SPlayer.Instance.OnGoldAmountChanged += Instance_OnGoldAmountChanged;
    }

    private void Instance_OnHealthPotionAmountChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void Instance_OnGoldAmountChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void UpdateText() {
        goldText.text = SPlayer.Instance.GetGoldAmount().ToString();
        //healthPotionText.text = SPlayer.Instance.GetHealthPotionAmount().ToString();
    }

}

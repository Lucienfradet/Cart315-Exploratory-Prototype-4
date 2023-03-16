using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DecoyClick : MonoBehaviour {
    public Button decoyButton;

    private void Start() {
        decoyButton.onClick.AddListener(() => {
            SubmarineMovements.sendDecoy();
        });
    }
}
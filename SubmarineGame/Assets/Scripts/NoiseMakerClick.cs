using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoiseMakerClick : MonoBehaviour
{
    public Button noisemakerButton;

    private void Start() {
        noisemakerButton.onClick.AddListener(()=> {
            SubmarineMovements.sendNoisemaker();
        });
    }
}

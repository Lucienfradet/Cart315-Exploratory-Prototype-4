using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [Header("Submarine Info")]
    public TextMeshProUGUI heading;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI depth;

    [Header("Contact")]
    private float test;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed.text = SubmarineMovements.speed.ToString("0") + " KNTS";
    }
}

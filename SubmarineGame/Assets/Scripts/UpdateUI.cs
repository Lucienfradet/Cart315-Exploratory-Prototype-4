using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    [Header("Submarine Info")]
    public TextMeshProUGUI heading;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI depth;

    [Header("Contact")]
    public TextMeshProUGUI bearing;
    public TextMeshProUGUI range;

    [Header("GameLost")]
    public Image fill;
    public TextMeshProUGUI gameOverText;
    public static bool gameLostFlag = false;
    private float timeSinceGameOver = 0F;

    // Start is called before the first frame update
    void Start()
    {
        fill.enabled = false;
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = SubmarineMovements.speed.ToString("0") + " KNTS";
        bearing.text = SubmarineMovements.bearing.ToString("0") + " DEG";
        range.text = SubmarineMovements.range.ToString("0") + " FT.";

        if (gameLostFlag) {
            gameLost();
        }
    }

    public void gameLost() {
        fill.enabled = true;
        gameOverText.enabled = true;
        if (timeSinceGameOver == 0) {
            timeSinceGameOver = Time.time;
        }
        if (Time.time > timeSinceGameOver + 1) {
            if (Input.anyKey) {
                Application.LoadLevel("Instructions");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CanvasManager : MonoBehaviour
{
    //canvas text objects
    public GameObject scoreTxtObj;
    public GameObject healthTxtObj;

    //references to TMpro component of the text GOs
    TextMeshProUGUI scoreTxt;
    TextMeshProUGUI healthTxt;

    //game stats scriptable types
    public IntVariable Score;
    public IntVariable Health;

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt = scoreTxtObj.GetComponent<TextMeshProUGUI>();
        healthTxt = healthTxtObj.GetComponent<TextMeshProUGUI>();

        Debug.Log(scoreTxt.ToString());

        //initialize the game stats
        Score.value = 0;
        Health.value = 100;

        UpdateGameStats();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameStats();
    }

    // update game stats
    void UpdateGameStats()
    {
        scoreTxt.text = "Score:  " + Score.value.ToString();
        healthTxt.text = "Health: " + Health.value.ToString();
    }


}

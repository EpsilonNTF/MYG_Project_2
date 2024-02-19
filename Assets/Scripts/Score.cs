using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text textescore;

    private int point = 0;


    //Refresh du system de score
    public int Point
    {
        get { return point; }

        set 
        {
            point = value;
            textescore.text = "Score : " + point;
        }
    }
}

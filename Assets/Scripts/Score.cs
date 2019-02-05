using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel0;
    private float score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //score = FindObjectOfType<DartThrow>().ThrowSpeed;
        scoreLabel0.text = score.ToString();

    }
}

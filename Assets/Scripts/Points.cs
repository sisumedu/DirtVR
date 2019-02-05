using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel0;

    int[] scores = { 20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5 };

    public GameObject Score, Board, Dart;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Dart = GameObject.Find("Dart");

        scoreLabel0.text = points.ToString();
    }


    public void AddPoints()
    {
        points += Dart.GetComponent<DartThrow>().CountPoints();
        //Score.GetComponent<GUIText>().text = points.ToString();
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dubugforce :MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text scoreLabel2;
    public UnityEngine.UI.Text scoreLabel3;
    public DartThrow dt;
    private float th;
    private float ds;
    private float rs;

    // Start is called before the first frame update
    void Start()
    {
       
    }


    
    public void Update()
    {
        th = FindObjectOfType<DartThrow>().ThrowSpeed;
        ds = FindObjectOfType<DartThrow>().DragSpeed;
        rs = FindObjectOfType<DartThrow>().RotationSpeed;

        float count = Input.GetAxisRaw("Mouse Y");
        scoreLabel.text = ds.ToString();

        float nou = Input.GetAxisRaw("Mouse X");
        scoreLabel2.text = rs.ToString();


        scoreLabel3.text = th.ToString();
        Debug.Log("th = " +th);
        Debug.Log("Mouse X = " + Input.GetAxisRaw("Mouse X"));
        Debug.Log("Mouse Y = " + Input.GetAxisRaw("Mouse Y"));
    }
}


    



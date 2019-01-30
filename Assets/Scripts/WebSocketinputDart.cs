using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketinputDart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //onece addforce
    bool OneX;
    bool OneZ;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PHONE_attack02_dart()
    {

        Debug.Log("OneXZ: " + OneX + OneZ);
        //if (0.4 > SmartPhoneInput.PadGradX ) { OneX = true; }

        //if (0.5 > SmartPhoneInput.PadGradZ && SmartPhoneInput.PadGradZ > -0.5) { OneZ = true; }

        if (SmartPhoneInput.PadGradX > 0.4 )
        {
            //transform.position += cameraForward * movespeed;
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * SmartPhoneInput.PadGradX, ForceMode.Impulse);
            OneX = false;
            //rigidbody.AddForce(cameraForward * 100, ForceMode.Force);
            //z += 1.0f;
            // animatorh.SetBool("iswalk", true); 
            Debug.Log("up: " + SmartPhoneInput.PadGradX);
        }

      
    }






}

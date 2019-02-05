﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // launch variables
    [SerializeField] private Transform TargetObjectTF;
    [Range(1.0f, 15.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;
    [Range(0.0f, 10.0f)] public float TargetHeightOffsetFromGround;
    public bool RandomizeHeightOffset;

    // state
    private bool bTargetReady;
    private bool bTouchingGround;

    // cache
    private Rigidbody rigid;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    // returns the distance between the red dot and the TargetObject's y-position
    // this is a very little offset considered the ranges in this demo so it shouldn't make a big difference.
    // however, if this code is tested on smaller values, the lack of this offset might introduce errors.
    // to be technically accurate, consider using this offset together with the target platform's y-position.
    float GetPlatformOffset()
    {
        float platformOffset = 0.0f;
        // 
        //          (SIDE VIEW OF THE PLATFORM)
        //
        //                   +------------------------- Mark (Sprite)
        //                   v
        //                  ___                                          -+-
        //    +-------------   ------------+         <- Platform (Cube)   |  platformOffset
        // ---|--------------X-------------|-----    <- TargetObject     -+-
        //    +----------------------------+
        //

        // we're iterating through Mark (Sprite) and Platform (Cube) Transforms. 
        foreach (Transform childTransform in TargetObjectTF.GetComponentsInChildren<Transform>())
        {
            // take into account the y-offset of the Mark gameobject, which essentially
            // is (y-offset + y-scale/2) of the Platform as we've set earlier through the editor.
            if (childTransform.name == "Mark")
            {
                platformOffset = childTransform.localPosition.y;
                break;
            }
        }
        return platformOffset;
    }
    // launches the object towards the TargetObject with a given LaunchAngle
    void Launch()
    {
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(TargetObjectTF.position.x, 0.0f, TargetObjectTF.position.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = (TargetObjectTF.position.y + GetPlatformOffset()) - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rigid.velocity = globalVelocity;
        bTargetReady = false;
    }

}

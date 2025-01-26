using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmController : MonoBehaviour
{
    public enum ArmType
    {
        LeftArm,
        RightArm
    }
    [SerializeField] private ArmType armType;
    
    [SerializeField] private HingeJoint2D armJoint;
    [SerializeField] private HingeJoint2D wristJoint;
    [SerializeField] private HingeJoint2D fingerAJoint;
    [SerializeField] private HingeJoint2D fingerBJoint;

    private KeyMap.ArmKeys _armKeys;
    
    // Start is called before the first frame update
    void Start()
    {
        _armKeys = KeyMap.GetKeysForArm(armType);
    }

    // Update is called once per frame
    void Update()
    {
        armJoint.useMotor = Input.GetKey(_armKeys.ArmKey);
        wristJoint.useMotor = Input.GetKey(_armKeys.WristKey);
        fingerAJoint.useMotor = Input.GetKey(_armKeys.FingerAKey);
        fingerBJoint.useMotor = Input.GetKey(_armKeys.FingerBKey);
        
        // Debug.Log($"Q:{Input.GetKey(KeyCode.Q)} W:{Input.GetKey(KeyCode.W)} O:{Input.GetKey(KeyCode.O)} P:{Input.GetKey(KeyCode.P)}");
    }
}

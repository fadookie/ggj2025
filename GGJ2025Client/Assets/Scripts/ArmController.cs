using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmController : MonoBehaviour
{
    private enum ArmType
    {
        LeftArm,
        RightArm
    }
    [SerializeField] private ArmType armType;
    
    [SerializeField] private HingeJoint2D armJoint;
    [SerializeField] private HingeJoint2D wristJoint;
    [SerializeField] private HingeJoint2D fingerAJoint;
    [SerializeField] private HingeJoint2D fingerBJoint;

    private KeyCode ArmKey => armType switch
    {
        ArmType.LeftArm => KeyCode.Q,
        ArmType.RightArm => KeyCode.P,
        _ => throw new ArgumentException()
    };
    
    private KeyCode WristKey => armType switch
    {
        ArmType.LeftArm => KeyCode.W,
        ArmType.RightArm => KeyCode.O,
        _ => throw new ArgumentException()
    };
    
    private KeyCode FingerAKey => armType switch
    {
        ArmType.LeftArm => KeyCode.E,
        ArmType.RightArm => KeyCode.I,
        _ => throw new ArgumentException()
    };
    
    private KeyCode FingerBKey => armType switch
    {
        ArmType.LeftArm => KeyCode.R,
        ArmType.RightArm => KeyCode.U,
        _ => throw new ArgumentException()
    };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        armJoint.useMotor = Input.GetKey(ArmKey);
        wristJoint.useMotor = Input.GetKey(WristKey);
        fingerAJoint.useMotor = Input.GetKey(FingerAKey);
        fingerBJoint.useMotor = Input.GetKey(FingerBKey);
        
        // Debug.Log($"Q:{Input.GetKey(KeyCode.Q)} W:{Input.GetKey(KeyCode.W)} O:{Input.GetKey(KeyCode.O)} P:{Input.GetKey(KeyCode.P)}");
    }
}

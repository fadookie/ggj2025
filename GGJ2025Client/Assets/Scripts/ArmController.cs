using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmController : MonoBehaviour
{
    public enum ArmType
    {
        LeftArm,
        RightArm
    }

    public static ArmController LeftArmControllerInstance { get; private set;  }
    public static ArmController RightArmControllerInstance { get; private set; }

    void Awake()
    {
        // Singleton setup
        switch (armType)
        {
            case ArmType.LeftArm:
            {
                if (LeftArmControllerInstance != null) {
                    Destroy(gameObject);
                    return;
                }
                LeftArmControllerInstance = this;
                break;
            }
            case ArmType.RightArm:
            {
                if (RightArmControllerInstance != null) {
                    Destroy(gameObject);
                    return;
                }
                RightArmControllerInstance = this;
                break;
            }
            default:
                throw new ArgumentException();
        }
        DontDestroyOnLoad(this);
    }
    
    [SerializeField] private ArmType armType;
    
    [SerializeField] private HingeJoint2D armJoint;
    [SerializeField] private HingeJoint2D wristJoint;
    [SerializeField] private HingeJoint2D fingerAJoint;
    [SerializeField] private HingeJoint2D fingerBJoint;
    
    private KeyMap.ArmKeys _armKeys;

    public IEnumerable<Collider2D> HostileTargets =>
        new[] { fingerAJoint, fingerBJoint }.Select(x => x.GetComponent<Collider2D>());
    
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

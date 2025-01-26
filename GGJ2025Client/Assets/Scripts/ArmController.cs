using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private HingeJoint2D _armJoint;
    [SerializeField] private HingeJoint2D _wristJoint;
    [SerializeField] private HingeJoint2D _fingerAJoint;
    [SerializeField] private HingeJoint2D _fingerBJoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _armJoint.useMotor = Input.GetKey(KeyCode.Q);
        _wristJoint.useMotor = Input.GetKey(KeyCode.W);
        _fingerAJoint.useMotor = Input.GetKey(KeyCode.O);
        _fingerBJoint.useMotor = Input.GetKey(KeyCode.P);
        
        // Debug.Log($"Q:{Input.GetKey(KeyCode.Q)} W:{Input.GetKey(KeyCode.W)} O:{Input.GetKey(KeyCode.O)} P:{Input.GetKey(KeyCode.P)}");
    }
}

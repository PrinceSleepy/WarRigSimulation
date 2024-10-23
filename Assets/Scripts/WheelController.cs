using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Rigidbody trailerRB;
    [SerializeField] PlayerCombat player;
    [SerializeField] WheelCollider frontRightWheel;
    [SerializeField] WheelCollider frontLeftWheel;
    [SerializeField] WheelCollider midRightWheel;
    [SerializeField] WheelCollider midLeftWheel;
    [SerializeField] WheelCollider rearRightWheel;
    [SerializeField] WheelCollider rearLeftWheel;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform midRightTransform;
    [SerializeField] Transform midLeftTransform;
    [SerializeField] Transform rearRightTransform;
    [SerializeField] Transform rearLeftTransform;

    [SerializeField] AudioSource engineSource;

    public float acceleration = 500f;
    public float breakingforce = 300f;
    public float maxTurnAngle = 15f;
    public double milesPerHour = 0f;

    private float currAcceleration = 0f;
    private float currBreakingforce = 0f;
    private float currTurnAngle = 0;

    private void FixedUpdate()
    {
        if (player.isAlive)
        {
            //Get forward/reverse acceleration from the vertical axis (W and S keys)
            currAcceleration = acceleration * Input.GetAxis("Vertical");
            //If pressing space bar give currBreakingForce a value
            if (Input.GetButton("EmergencyBrake"))
                currBreakingforce = breakingforce;
            else
                currBreakingforce = 0f;
            //Apply acceleration to rear wheels
            rearRightWheel.motorTorque = currAcceleration;
            rearLeftWheel.motorTorque = currAcceleration;
            //Apply acceleration to mid wheels later!!


            frontRightWheel.brakeTorque = currBreakingforce;
            frontLeftWheel.brakeTorque = currBreakingforce;
            midRightWheel.brakeTorque = currBreakingforce;
            midLeftWheel.brakeTorque = currBreakingforce;
            rearRightWheel.brakeTorque = currBreakingforce;
            rearLeftWheel.brakeTorque = currBreakingforce;

            //Steering
            currTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
          
            frontLeftWheel.steerAngle = currTurnAngle;
            frontRightWheel.steerAngle = currTurnAngle;

            //update wheel meshes
            UpdateWheel(frontRightWheel, frontRightTransform);
            UpdateWheel(frontLeftWheel, frontLeftTransform);
            UpdateWheel(midLeftWheel, midLeftTransform);
            UpdateWheel(midRightWheel, midRightTransform);
            UpdateWheel(rearLeftWheel, rearLeftTransform);
            UpdateWheel(rearRightWheel, rearRightTransform);
        }
        else 
        {
            currTurnAngle = maxTurnAngle;

            frontLeftWheel.steerAngle = currTurnAngle;
            frontRightWheel.steerAngle = currTurnAngle;
        }
        //print(Input.GetAxis("Vertical"));
    }
    void UpdateWheel(WheelCollider col, Transform trans)
    {
        //get wheel collider state
        Vector3 position = trans.position;
        Quaternion rotation = trans.rotation;
        col.GetWorldPose(out position, out rotation);
        //rotation = rotation * Quaternion.Euler(new Vector3(0,90,0));
        //Set wheel transform state
        trans.position = position;
        trans.rotation = rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlatformController.singleton.Init("COM7", 115200);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        PlatformController.singleton.Roll = Mathf.DeltaAngle(transform.eulerAngles.z, 0);
        PlatformController.singleton.Pitch = -Mathf.DeltaAngle(transform.eulerAngles.x, 0);
        PlatformController.singleton.Yaw = Input.GetAxis("Horizontal") * 5;
        Vector3 v = transform.InverseTransformDirection(rb.velocity);

        float pct = (v.z * 2.237f / 30f);
        engineSource.pitch = 0.65f + pct * 0.5f;
    }
}

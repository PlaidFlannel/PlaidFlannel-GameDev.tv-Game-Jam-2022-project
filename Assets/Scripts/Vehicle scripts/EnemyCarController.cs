using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    public float speed = 3.0f;
    private float speedModifier = 100.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private float currentSteerAngle;
    private bool crashed = false;

    [SerializeField] GameObject crashedIndictor;
    [SerializeField] GameObject targetObject;

    private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        //player = GameObject.Find("car");
        player = targetObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (!crashed)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            UpdateWheels();
            HandleSteering();

            //Debug.Log(lookDirection);
            enemyRb.rotation = Quaternion.LookRotation(lookDirection);
            enemyRb.AddForce(lookDirection * speed * speedModifier);
            HandleSteering();
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
    }
    void HandleSteering()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        currentSteerAngle = maxSteerAngle * lookDirection.x;
        frontLeftWheelCollider.steerAngle = -currentSteerAngle;
        frontRightWheelCollider.steerAngle = -currentSteerAngle;
    }
    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    private void ApplyBraking(float brakeForce)
    {
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }
    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("crashed");
            ApplyBraking(500.0f);
            crashed = true;
            StartCoroutine(CrashedCountdownRoutine());
            crashedIndictor.gameObject.SetActive(true);
        }
    }
    IEnumerator CrashedCountdownRoutine()
    {
        yield return new WaitForSeconds(8);
        //Debug.Log("uncrashed");
        crashed = false;
        ApplyBraking(0.0f);
        crashedIndictor.gameObject.SetActive(false);
    }
}


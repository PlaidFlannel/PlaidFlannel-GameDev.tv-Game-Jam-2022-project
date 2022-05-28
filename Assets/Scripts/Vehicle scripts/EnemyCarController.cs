using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    public float speed = 3.0f;
    private float speedModifier = 1000.0f;
    private float crashCooldownTime = 5.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    public Transform target;
    private float currentSteerAngle;
    private bool crashed = false;

    [SerializeField] GameObject crashedIndictor;
    [SerializeField] GameObject targetObject;

    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    public bool mirrorModeActive = false;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = targetObject;
        
    }
    void Update()
    {
        if (mirrorModeActive)
        {
            MirrorObjectMovement();
        }
        else if (!mirrorModeActive & !crashed)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            UpdateWheels();
            HandleSteering();
            enemyRb.rotation = Quaternion.LookRotation(lookDirection);
            enemyRb.AddForce(lookDirection * speed * speedModifier * Time.deltaTime);
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
    void ApplyBraking(float brakeForce)
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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyBraking(500.0f);
            crashed = true;
            StartCoroutine(CrashedCountdownRoutine());
            crashedIndictor.gameObject.SetActive(true);
        }
    }
    IEnumerator CrashedCountdownRoutine()
    {
        yield return new WaitForSeconds(crashCooldownTime);
        crashed = false;
        ApplyBraking(0.0f);
        crashedIndictor.gameObject.SetActive(false);
    }

    private void MirrorObjectMovement()
    {
        float targetX = target.position.x;
        float targetZ = target.position.z;
        transform.position = new Vector3(-targetX, 0.054f, targetZ);
        var euler = target.rotation.eulerAngles;   //get target's rotation
        var rot = Quaternion.Euler(0, -euler.y, 0); //transpose values
        transform.rotation = rot;                  //set my rotation
    }
}


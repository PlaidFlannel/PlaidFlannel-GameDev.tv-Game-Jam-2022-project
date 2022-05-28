using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbrakeForce;
    private float multiplier = 1f;

    private bool isBraking;
    private bool hasPowerup = false;

    [SerializeField] float powerupDuration = 3;
    [SerializeField] GameObject powerupIndicator;
    [SerializeField] float maxSpeedMultiplier = 150f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] AudioClip engineSound;
    [SerializeField] GameObject nextLevelIndicator;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    public Vector3 com; //center of mass
    public Rigidbody rb;
    private float targetPitch = 1.8f; //engine audio pitch
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
        audioSource = GetComponent<AudioSource>();
        nextLevelIndicator.SetActive(false);
        audioSource.volume = 0.25f;
    }
    private void FixedUpdate()
    {
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, .1f * Time.deltaTime);
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        if (transform.position.y < -30)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        if (verticalInput > 0.1 | verticalInput < 0)
        {
            targetPitch = 1.5f;
        }
        else
        {
            targetPitch = 1f;
        }
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, .1f * Time.deltaTime);
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {
        if (hasPowerup) 
        { 
            multiplier = maxSpeedMultiplier;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSound);
        }
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce * multiplier;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce * multiplier;
        currentbrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }
    void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = currentbrakeForce;
        frontRightWheelCollider.brakeTorque = currentbrakeForce;
        rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearRightWheelCollider.brakeTorque = currentbrakeForce;
    }
    void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    void UpdateWheels()
    {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
        if (other.CompareTag("Sensor1"))
        {
            nextLevelIndicator.SetActive(true);
            
            audioSource.volume = 0.15f;
            Invoke("LoadNextLevel", levelLoadDelay);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        hasPowerup = false;
        multiplier = 1f;
        powerupIndicator.gameObject.SetActive(false);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

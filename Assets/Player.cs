using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
  [Tooltip("In ms^-1")] [SerializeField] float Speed = 15f;
  [Tooltip("In m")] [SerializeField] float xRange = 5f;
  [Tooltip("In m")] [SerializeField] float yRange = 3f;
  [SerializeField] float posPitchFactor = -5f;
  [SerializeField] float ctrlPitchFactor = -15f;
  [SerializeField] float posYawFactor = 5f;
  [SerializeField] float ctrlRollFactor = -25f;

  float xThrow, yThrow;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    ProcessTranslation();
    ProcessRotation();

  }

  private void ProcessRotation()
  {
    float pitch = transform.localPosition.y * posPitchFactor + yThrow * ctrlPitchFactor;
    float yaw = transform.localPosition.x * posYawFactor;
    float roll = xThrow * ctrlRollFactor;
    transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
  }

  private void ProcessTranslation()
  {
    xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    yThrow = CrossPlatformInputManager.GetAxis("Vertical");

    float xOffset = xThrow * Time.deltaTime * Speed;
    float yOffset = yThrow * Time.deltaTime * Speed;

    float xRawPos = transform.localPosition.x + xOffset;
    float yRawPos = transform.localPosition.y + yOffset;

    float xPos = Mathf.Clamp(xRawPos, -xRange, xRange);
    float yPos = Mathf.Clamp(yRawPos, -yRange, yRange);

    transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
  }
}

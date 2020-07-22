using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
  [Header("General")]
  [Tooltip("In ms^-1")] [SerializeField] float ControlSpeed = 15f;
  [Tooltip("In m")] [SerializeField] float xRange = 5f;
  [Tooltip("In m")] [SerializeField] float yRange = 3f;
  [SerializeField] GameObject[] guns;

  [Header("Screen-position Based")]
  [SerializeField] float posPitchFactor = -5f;
  [SerializeField] float posYawFactor = 5f;

  [Header("Control-throw Based")]
  [SerializeField] float ctrlPitchFactor = -15f;
  [SerializeField] float ctrlRollFactor = -25f;

  float xThrow, yThrow;
  bool isControlEnabled = true;
  

  // Update is called once per frame
  void Update()
  {
    if(isControlEnabled)
    {
      ProcessTranslation();
      ProcessRotation();
      ProcessFiring();
    }

  }

  private void ProcessFiring()
  {
    if(CrossPlatformInputManager.GetButton("Fire"))
    {
      ActivateGuns();
    }
    else
    {
      DeactivateGuns();
    }
  }

  private void DeactivateGuns()
  {
    foreach (GameObject gun in guns)
    {
      gun.SetActive(false);
    }
  }

  private void ActivateGuns()
  {
    foreach (GameObject gun in guns)
    {
      gun.SetActive(true);
    }
  }

  void OnPlayerDeath() // called by string reference
  {
    print("Controls Frozen");
    isControlEnabled = false;
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

    float xOffset = xThrow * Time.deltaTime * ControlSpeed;
    float yOffset = yThrow * Time.deltaTime * ControlSpeed;

    float xRawPos = transform.localPosition.x + xOffset;
    float yRawPos = transform.localPosition.y + yOffset;

    float xPos = Mathf.Clamp(xRawPos, -xRange, xRange);
    float yPos = Mathf.Clamp(yRawPos, -yRange, yRange);

    transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
  }
}

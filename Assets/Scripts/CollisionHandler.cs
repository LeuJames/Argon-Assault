using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok as long as this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour
{
  [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
  [Tooltip("fx prefab on player")][SerializeField] GameObject deathFX;
  private void OnTriggerEnter(Collider other)
  {
    StartDeathSequence();
    deathFX.SetActive(true);
    Invoke("ReloadLevel", levelLoadDelay);
  }

  private void ReloadLevel()//string reference
  {
    SceneManager.LoadScene(1);
  }

  private void StartDeathSequence()
  {
    SendMessage("OnPlayerDeath");
  }
}

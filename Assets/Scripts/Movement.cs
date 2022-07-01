using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 100;
  [SerializeField] float rotateThrust = 100;
  [SerializeField] AudioClip mainEngine;

  [SerializeField] ParticleSystem mainEngineParticles;
  [SerializeField] ParticleSystem leftThrusterParticles;
  [SerializeField] ParticleSystem rightThrusterParticles;


  Rigidbody rb;
  AudioSource audioSource;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      StartThrusting();
    }
    else
    {
      StopThrusting();
    }
  }


  void ProcessRotation()
   {
    if (Input.GetKey(KeyCode.A))
    {
      RotateLeft();
    }
    else if (Input.GetKey(KeyCode.D))
    {
      RotateRight();
    }
    else
    {
      StopRotating();
    }
  }

  private void StopRotating()
  {
    rightThrusterParticles.Stop();
    leftThrusterParticles.Stop();
  }

  private void RotateRight()
  {
    ApplyRotation(-rotateThrust);
    if (!leftThrusterParticles.isPlaying)
    {
      leftThrusterParticles.Play();
    }
  }

  private void RotateLeft()
  {
    ApplyRotation(rotateThrust);
    if (!rightThrusterParticles.isPlaying)
    {
      rightThrusterParticles.Play();
    }
  }

  private void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; //freezing rotation so we can manually rotate;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; //un-freezing rotation so the physics can take over
  }


  void StartThrusting()
  {
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(mainEngine);
    }
    if (!mainEngineParticles.isPlaying)
    {
      mainEngineParticles.Play();
    }
  }

  private void StopThrusting()
  {
    audioSource.Stop();
    mainEngineParticles.Stop();
  }

}

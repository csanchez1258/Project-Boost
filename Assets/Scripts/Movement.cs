using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 100;
  [SerializeField] float rotateThrust = 100;
  [SerializeField] AudioClip mainEngine;


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

  void ProcessRotation()
   {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotateThrust);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-rotateThrust);
    }
   }

  private void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; //freezing rotation so we can manually rotate;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; //un-freezing rotation so the physics can take over
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
      }
    }
    else audioSource.Stop();
  }
}

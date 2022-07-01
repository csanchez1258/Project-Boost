using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
  [SerializeField] Vector3 startingPosition;
  [SerializeField] Vector3 movementVector;
  float movementFactor;
  [SerializeField] float period = 2f;

  // Start is called before the first frame update
  void Start()
  {
    startingPosition = transform.position;
    Debug.Log(startingPosition);
  }

    // Update is called once per frame
  void Update()
  {
    if(period <= Mathf.Epsilon)
    {
      return;
    }
    float cycles = Time.time / period;  //continually growing overtime

    const float tau = Mathf.PI * 2;  //(tau = 2pi) 6.283...

    float rawSinWave = Mathf.Sin(cycles*tau); //going from (-1,1)

    movementFactor = (rawSinWave + 1f) / 2f;  //add 1 now shifted to (0,2) then / 2 then (0,1)

    Vector3 offset = movementVector * movementFactor; //create the offset of the movement
    transform.position = startingPosition + offset;   //then transform its position (move the object based on offset) affected also by sin
  }
}

using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour 
{
  public float speed;

  private static GameObject _guardPrefab = Resources.Load("Prefabs/Guard") as GameObject;
  private IGuardMovementStrategy _guardMovementStrategy;
  private IGuardDetectStrategy _guardDetectStrategy;
  private Rigidbody2D _rigidbody;
  private Vector2 nextPoint;

  private bool clockwise = false;
  private bool counterclockwise = false;

  public static Guard Create(IGuardMovementStrategy mStrat, IGuardDetectStrategy dStrat)
  {
    GameObject newGuardObject = Instantiate(Guard._guardPrefab);
    Guard newGuard = newGuardObject.GetComponent<Guard>();
    newGuard._guardMovementStrategy = mStrat;
    newGuard._guardDetectStrategy = dStrat;
    return newGuard;
  }

  void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
    //nextPoint = _rigidbody.position;
    nextPoint = new Vector2(5.0f, 5.0f);
  }

  void Update()
  {
    /*
    if(VectorCloseTo(_rigidbody.position, new Vector2(5.0f, 5.0f))) {
      MoveTo(new Vector2(-5.0f, 5.0f));
    } else {
      MoveTo(new Vector2(5.0f, 5.0f));
    }
    */
    if(VectorCloseTo(_rigidbody.position, nextPoint))
    {
      _rigidbody.velocity = Vector2.zero;
      nextPoint = new Vector2(-5.0f, -5.0f);
    }
    MoveTo(nextPoint);

  }

  //Rotates the object by 1 degree CounterClockwise (1 degree)
  private void RotateCCWOne()
  {
    _rigidbody.transform.RotateAround(_rigidbody.position, Vector3.forward, 1.0f);
  }

  //Rotates the object by 1 degree Clockwise (-1 degrees)
  private void RotateCWOne()
  {
    _rigidbody.transform.RotateAround(_rigidbody.position, Vector3.forward, -1.0f);
  }

  private bool MoveTo(Vector2 targetLocation)
  {
    //Acquires the current position for calculations
    Vector2 initialPosition = _rigidbody.position;
    Vector2 targetDirection = (targetLocation - initialPosition).normalized;
    //float targetAngle = Mathf.Atan(targetLocation.x / targetLocation.y) * 360 / (2 * Mathf.PI);
    float targetAngle = Mathf.Atan(targetDirection.y / targetDirection.x) * 360 / (2 * Mathf.PI);
    if(targetDirection.x < 0)
    {
      targetAngle += 180.0f;
    }
    //Debug
    //.Log("Target Angle: " + (int)targetAngle);
    //Debug.Log("Target Point: " + targetDirection.x + ", " + targetDirection.y);

    //Initial angle of guard
    //Euler Angle of Z has 0 facing down
    float initialAngle = _rigidbody.transform.eulerAngles.z - 90.0f;
    float initialAngleRadians = initialAngle * (2 * Mathf.PI) / 360;
    //float initialEulerAngle = _rigidbody.transform.eulerAngles.z - 90.0f;
    Vector2 initialDirection = new Vector2 (Mathf.Cos(initialAngleRadians), Mathf.Sin(initialAngleRadians));
    //Debug
    //Debug.Log("Initial Angle: " + (int)initialAngle);

    float angleChange = Vector2.Angle(initialDirection, targetDirection);
    //Debug
    //Debug.Log("Angle Change: " + angleChange);

    //If the current position is already at the final position, return and set velocity to 0
    if(VectorCloseTo(initialPosition, targetLocation)) 
    {
      _rigidbody.velocity = Vector2.zero;
      return true;
    }

    //Already looking towards final destination, so set velocity
    if(angleChange < 0.5)
    {
      _rigidbody.velocity = targetDirection * speed;
      return false;
    } else
    {
      if(initialAngle - targetAngle <= 0)
      {
        //Debug.Log("CCW");
        RotateCCWOne();
      } else
      {
        //Debug.Log("CW");
        RotateCWOne();
      }
      return false;
    }
    return false;
  }

  //Checks whether the 2 vectors are within 0.1 of each other.
  private bool VectorCloseTo (Vector2 a, Vector2 b)
  {
    bool horizontalCheck = ((Mathf.Abs(a.x - b.x)) <= 0.1);
    bool verticalCheck = ((Mathf.Abs(a.y - b.y)) <= 0.1);
    return (horizontalCheck && verticalCheck);
  }
}

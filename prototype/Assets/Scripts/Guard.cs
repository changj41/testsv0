using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour 
{
  public float speed;

  private static GameObject _guardPrefab = Resources.Load("Prefabs/Guard") as GameObject;
  private IGuardMovementStrategy _guardMovementStrategy;
  private IGuardDetectStrategy _guardDetectStrategy;
  private Rigidbody2D _rigidbody;

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
    Vector2 a = Vector2.up;
    Vector2 b = Vector2.right;
    Debug.Log("Angle: " + Vector2.Angle(b, a));


    _rigidbody = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    MoveTo(new Vector2 (5.0f, 5.0f));
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

    //Initial angle of guard
    float initialAngle = _rigidbody.transform.eulerAngles.z - 90.0f;
    Vector2 initialDirection = new Vector2 (Mathf.Cos(initialAngle), Mathf.Sin(initialAngle));
    float angleChange = Vector2.Angle(initialDirection, targetDirection);
    if (angleChange == 0) {

    }

    //If the current position is already at the final position, return and set velocity to 0
    if (VectorCloseTo(initialPosition, targetLocation)) {
      _rigidbody.velocity = Vector2.zero;
      return true;
    }

    //Set the velocity
    _rigidbody.velocity = targetDirection * speed;
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

using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour 
{
  private static GameObject _guardPrefab = Resources.Load("Prefabs/Guard") as GameObject;
  private IGuardMovementStrategy _guardMovementStrategy;
  private IGuardDetectStrategy _guardDetectStrategy;

  public static Guard Create(IGuardMovementStrategy mStrat, IGuardDetectStrategy dStrat)
  {
    GameObject newGuardObject = Instantiate(Guard._guardPrefab);
    Guard newGuard = newGuardObject.GetComponent<Guard>();
    newGuard._guardMovementStrategy = mStrat;
    newGuard._guardDetectStrategy = dStrat;
    return newGuard;
  }


}

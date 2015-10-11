using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public float speed;
  private InputManager _inputManager;

	// Use this for initialization
	void Start()
  {
    _inputManager = new InputManager();
	}
	
	// Update is called once per frame
	void Update() 
  {
    ScreenInput input = _inputManager.GetKeyboardKey();
    MoveDirection(input);
	}

  public void MoveDirection(ScreenInput input)
  {
    if(input == null)
    {
      GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
      return;
    }

    ScreenInput.Arrow arrow = input.arrow;
    if(arrow.Equals(ScreenInput.Arrow.Up))
    {
      GetComponentInChildren<Rigidbody>().velocity = Vector3.up * speed;
    }
    if(arrow.Equals(ScreenInput.Arrow.Down))
    {
      GetComponentInChildren<Rigidbody>().velocity = Vector3.down * speed;
    }
    if(arrow.Equals(ScreenInput.Arrow.Left))
    {
      GetComponentInChildren<Rigidbody>().velocity = Vector3.left * speed;
    }
    if(arrow.Equals(ScreenInput.Arrow.Right))
    {
      GetComponentInChildren<Rigidbody>().velocity = Vector3.right * speed;
    }

  }
}

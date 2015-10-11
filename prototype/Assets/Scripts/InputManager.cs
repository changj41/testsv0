//------------------------------------------------------------------------------
// The purpose of InputManager is to tell the game when inputs happen.
// The InputManager sorts through the details of whether the input was a mouse
// click or a touch
//
// Added in keyboard arrows functionality
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class InputManager
{

  private bool _useTouch;
  
  public InputManager()
  {
    _useTouch = Input.touchSupported;
  }
  
  public ScreenInput GetInput()
  {
    if(_useTouch)
    {
      return getTouchInput();
    }
    else
    {
      return getMouseInput();
    }
  }

  public ScreenInput GetKeyboardKey()
  {
    return getKeyboardInput();
  }
  
  private ScreenInput getTouchInput()
  {
    ScreenInput result = null;
    if(Input.touchCount > 0)
    {
      Touch firstTouch = Input.GetTouch(0);
      if (firstTouch.phase == TouchPhase.Began)
      {
        result = new ScreenInput();
        result.inputPoint = Camera.main.ScreenToWorldPoint(firstTouch.position);
        result.state = ScreenInput.State.Down;
      }
    }
    
    return result;
  }
  
  private ScreenInput getMouseInput()
  {
    ScreenInput result = null;
    
    if(Input.GetMouseButtonDown(0 /*left*/))
    {
      result = new ScreenInput();
      result.inputPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      result.state = ScreenInput.State.Down;
    }
    
    return result;
  }

  //For arrow inputs
  private ScreenInput getKeyboardInput()
  {
    ScreenInput result = null;

    if (Input.GetKey(KeyCode.DownArrow)) 
    {
      result = new ScreenInput();
      result.inputPoint = Vector2.zero;
      result.state = ScreenInput.State.Hold;
      result.arrow = ScreenInput.Arrow.Down;
    }

    if (Input.GetKey(KeyCode.UpArrow)) 
    {
      result = new ScreenInput();
      result.inputPoint = Vector2.zero;
      result.state = ScreenInput.State.Hold;
      result.arrow = ScreenInput.Arrow.Up;
    }

    if (Input.GetKey(KeyCode.LeftArrow)) 
    {
      result = new ScreenInput();
      result.inputPoint = Vector2.zero;
      result.state = ScreenInput.State.Hold;
      result.arrow = ScreenInput.Arrow.Left;
    }

    if (Input.GetKey(KeyCode.RightArrow)) 
    {
      result = new ScreenInput();
      result.inputPoint = Vector2.zero;
      result.state = ScreenInput.State.Hold;
      result.arrow = ScreenInput.Arrow.Right;
    }

    return result;

  }

}



// Represents in input on the screen, such as a mouse click or a touch
public class ScreenInput
{
  public enum State
  {
    Down,  // First frame click/touch down
    Hold,  // Click/touch held down
    Up,    // First frame click/touch up (released)
  }

  public enum Arrow
  {
    Up,     //Up arrow
    Down,   //Down arrow
    Left,   //Left arrow
    Right,  //Right arrow
  }
  
  public Vector2 inputPoint;
  public State state;
  public Arrow arrow;
	
}

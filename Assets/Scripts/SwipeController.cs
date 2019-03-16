using UnityEngine;
using System.Collections;
//using UnityEditor;

public enum Swipe {
	None,
	Up,
	Down,
	Left,
	Right,
	UpLeft,
	UpRight,
	DownLeft,
	DownRight
};

public class SwipeController : MonoBehaviour
{

	// Min length to detect the Swipe
	public float MinSwipeLength = 2f;

	private Vector2 _firstPressPos;
	private Vector2 _secondPressPos;
	private Vector2 _currentSwipe;

	private Vector2 _firstClickPos;
	private Vector2 _secondClickPos;

	public static Swipe SwipeDirection;
	public float ReturnForce = 10f;


	//Player and aniamtor here
	private Animator anim;
	public float swipeValue;

    public bool isTouchActive;


	public float smooth = 1f;
	private Quaternion targetRotation;
	public RectTransform []hudButtons;

	void Start()
	{
        isTouchActive = true;
		anim = gameObject.GetComponent<Animator>();
		targetRotation = transform.rotation;
	}

	private void Update() 
    {
        if(isTouchActive == true)
        {
            DetectSwipe();
			if (Input.GetKeyDown (KeyCode.Space)) { 
				targetRotation *=  Quaternion.AngleAxis(60, Vector3.up);
			}
			transform.rotation= Quaternion.Lerp (transform.rotation, targetRotation , 10 * smooth * Time.deltaTime); 
        }
	}

	public void DetectSwipe()
	{
		#if UNITY_EDITOR
		MouseSwipeControl();
		#else
		TouchSwipeControl();
		#endif
	}


	#region TouchSwipeControl
	void TouchSwipeControl()
	{
		if ( Input.touches.Length > 0 ) {
			Touch t = Input.GetTouch( 0 );

			for(int i = 0;i < Input.touches.Length;i++)
			{
				if(ExclusionOfUIButtons(Input.GetTouch(i)))
				{
					t = Input.GetTouch(i);					
				}
			}

			if(!ExclusionOfUIButtons(t))
			{
				return;
			}

			if ( t.phase == TouchPhase.Began ) {
				_firstPressPos = new Vector2( t.position.x, t.position.y );
				firstClick = true;
			}

			if (firstClick) {
				_secondPressPos = new Vector2( t.position.x, t.position.y );
				_currentSwipe = new Vector3( _secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y );

				// Make sure it was a legit swipe, not a tap
				if ( _currentSwipe.magnitude < MinSwipeLength ) {
					SwipeDirection = Swipe.None;
					return;
				}

				firstClick = false;
				_currentSwipe.Normalize();

				// Swipe up
				if ( _currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
					SwipeDirection = Swipe.Up;
					Debug.Log ("UP");
				}// Swipe up right
				else if ( _currentSwipe.y > 0 && _currentSwipe.x > 0 ) {
					SwipeDirection = Swipe.UpRight;
					RightEvent ();
					//	UpEvent ();
				}
				// Swipe up left
				else if ( _currentSwipe.y > 0 && _currentSwipe.x < 0 ) {
					SwipeDirection = Swipe.UpLeft;
					LeftEvent ();
					//	UpEvent ();
				}
				// Swipe down
				else if ( _currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
					SwipeDirection = Swipe.Down;
					DownEvent ();
				}
				// Swipe down right
				else if ( _currentSwipe.y < 0 && _currentSwipe.x > 0 ) {
					SwipeDirection = Swipe.DownRight;
					RightEvent ();
				}
				// Swipe down left
				else if ( _currentSwipe.y < 0 && _currentSwipe.x < 0 ) {
					SwipeDirection = Swipe.DownLeft;
					LeftEvent ();
				}
				// Swipe left
				else if ( _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
					SwipeDirection = Swipe.Left;
					LeftEvent();
				}
				// Swipe right
				else if ( _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
					SwipeDirection = Swipe.Right;
					RightEvent();
				}
			}//TouchPhase Ended
			//}
//			if ( t.phase == TouchPhase.Ended ) {
//				_secondPressPos = new Vector2( t.position.x, t.position.y );
//				_currentSwipe = new Vector3( _secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y );
//
//				// Make sure it was a legit swipe, not a tap
//				if ( _currentSwipe.magnitude < MinSwipeLength ) {
//					SwipeDirection = Swipe.None;
//					return;
//				}
//
//				_currentSwipe.Normalize();
//
//				// Swipe up
//				if ( _currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
//					SwipeDirection = Swipe.Up;
//					Debug.Log ("UP");
//				}// Swipe up right
//				else if ( _currentSwipe.y > 0 && _currentSwipe.x > 0 ) {
//					SwipeDirection = Swipe.UpRight;
//					RightEvent ();
//				//	UpEvent ();
//				}
//				// Swipe up left
//				else if ( _currentSwipe.y > 0 && _currentSwipe.x < 0 ) {
//					SwipeDirection = Swipe.UpLeft;
//					LeftEvent ();
//				//	UpEvent ();
//				}
//				// Swipe down
//				else if ( _currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
//					SwipeDirection = Swipe.Down;
//					DownEvent ();
//				}
//				// Swipe down right
//				 else if ( _currentSwipe.y < 0 && _currentSwipe.x > 0 ) {
//					SwipeDirection = Swipe.DownRight;
//					RightEvent ();
//				}
//				// Swipe down left
//				else if ( _currentSwipe.y < 0 && _currentSwipe.x < 0 ) {
//					SwipeDirection = Swipe.DownLeft;
//					LeftEvent ();
//				}
//				// Swipe left
//				else if ( _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
//					SwipeDirection = Swipe.Left;
//                    LeftEvent();
//				}
//				// Swipe right
//				else if ( _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
//					SwipeDirection = Swipe.Right;
//                    RightEvent();
//				}
//			}//TouchPhase Ended
		} 
	}
	#endregion


	public bool firstClick=false;
	#region MouseSwipeControl
	void MouseSwipeControl()
	{
		if ( Input.GetMouseButtonDown( 0 ) ) {
			_firstClickPos = new Vector2( Input.mousePosition.x, Input.mousePosition.y );
			firstClick = true;
		} else {
			SwipeDirection = Swipe.None;
			//Debug.Log ("None");
		}
		if (firstClick) {
		
			_secondClickPos = new Vector2( Input.mousePosition.x, Input.mousePosition.y );
			_currentSwipe = new Vector3( _secondClickPos.x - _firstClickPos.x, _secondClickPos.y - _firstClickPos.y );
		
			// Make sure it was a legit swipe, not a tap
			if ( _currentSwipe.magnitude < MinSwipeLength ) {
				SwipeDirection = Swipe.None;
				return;
			}
			firstClick = false;
			_currentSwipe.Normalize();

			//Swipe directional check
			if ( _currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
				SwipeDirection = Swipe.Up;
				Debug.Log( "Up" );
				//UpEvent ();
			}
			// Swipe down
			else if ( _currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
				SwipeDirection = Swipe.Down;
				Debug.Log( "Down" );
				DownEvent ();
			}
			// Swipe left
			else if ( _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
				SwipeDirection = Swipe.Left;
				Debug.Log( "Left" );
				LeftEvent();
			}
			// Swipe right
			else if ( _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
				SwipeDirection = Swipe.Right;
				Debug.Log( "right" );
				RightEvent();

			}     // Swipe up left
			else if ( _currentSwipe.y > 0 && _currentSwipe.x < 0 ) {
				SwipeDirection = Swipe.UpLeft;
				Debug.Log( "UpLeft" );
				//	UpEvent ();

			}
			// Swipe up right
			else if ( _currentSwipe.y > 0 && _currentSwipe.x > 0 ) {
				SwipeDirection = Swipe.UpRight;
				Debug.Log( "UpRight" );
				//UpEvent ();

			}
			// Swipe down left
			else if ( _currentSwipe.y < 0 && _currentSwipe.x < 0 ) {
				SwipeDirection = Swipe.DownLeft;
				Debug.Log( "DownLeft" );
				DownEvent();
				// Swipe down right
			} else if ( _currentSwipe.y < 0 && _currentSwipe.x > 0 ) {
				SwipeDirection = Swipe.DownRight;
				Debug.Log( "DownRight" );
				DownEvent();
			}
		
		}
	
//		if ( Input.GetMouseButtonUp( 0 ) ) {
//			_secondClickPos = new Vector2( Input.mousePosition.x, Input.mousePosition.y );
//			_currentSwipe = new Vector3( _secondClickPos.x - _firstClickPos.x, _secondClickPos.y - _firstClickPos.y );
//
//			// Make sure it was a legit swipe, not a tap
//			if ( _currentSwipe.magnitude < MinSwipeLength ) {
//				SwipeDirection = Swipe.None;
//				return;
//			}
//
//			_currentSwipe.Normalize();
//
//			//Swipe directional check
//			if ( _currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
//				SwipeDirection = Swipe.Up;
//				Debug.Log( "Up" );
//				//UpEvent ();
//			}
//			// Swipe down
//			else if ( _currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f ) {
//				SwipeDirection = Swipe.Down;
//				Debug.Log( "Down" );
//				DownEvent ();
//			}
//			// Swipe left
//			else if ( _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
//				SwipeDirection = Swipe.Left;
//				Debug.Log( "Left" );
//				LeftEvent();
//			}
//			// Swipe right
//			else if ( _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f ) {
//				SwipeDirection = Swipe.Right;
//				Debug.Log( "right" );
//				RightEvent();
//
//			}     // Swipe up left
//			else if ( _currentSwipe.y > 0 && _currentSwipe.x < 0 ) {
//				SwipeDirection = Swipe.UpLeft;
//				Debug.Log( "UpLeft" );
//			//	UpEvent ();
//
//			}
//			// Swipe up right
//			else if ( _currentSwipe.y > 0 && _currentSwipe.x > 0 ) {
//				SwipeDirection = Swipe.UpRight;
//				Debug.Log( "UpRight" );
//				//UpEvent ();
//
//			}
//			// Swipe down left
//			else if ( _currentSwipe.y < 0 && _currentSwipe.x < 0 ) {
//				SwipeDirection = Swipe.DownLeft;
//				Debug.Log( "DownLeft" );
//                DownEvent();
//				// Swipe down right
//			} else if ( _currentSwipe.y < 0 && _currentSwipe.x > 0 ) {
//				SwipeDirection = Swipe.DownRight;
//				Debug.Log( "DownRight" );
//                DownEvent();
//			}
	//	}
	}
	#endregion


	public void UpEvent()
	{
		
		//GetComponent<PlayerMovementDemo> ().Jump ();
	}

	public void LeftEvent()
	{
		 
		//swipeValue = 1f;
	}
	public void RightEvent()
	{
		
		//swipeValue = 2f;
	}
	public void DownEvent()
	{
		
		//swipeValue = 3f;
	}


	Vector2 insidePoints;
	bool ExclusionOfUIButtons(Touch t)
	{
		for(int i=0;i<hudButtons.Length;i++)
		{
			if(RectTransformUtility.ScreenPointToLocalPointInRectangle(hudButtons[i],t.position,null,out insidePoints))
			{
				if(hudButtons[i].rect.Contains(insidePoints))
				{
					return false;
				}
			}
		}

		return true;
	}
}

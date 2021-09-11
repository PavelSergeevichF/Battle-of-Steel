using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SimpleTouchController : MonoBehaviour {

	// PUBLIC
	public delegate void TouchDelegate(Vector2 value);
	public event TouchDelegate TouchEvent;
	public float Sent_x, Sent_y;
	float valStartX, valX;

	public delegate void TouchStateDelegate(bool touchPresent);
	public event TouchStateDelegate TouchStateEvent;

	// PRIVATE
	[SerializeField]
	private RectTransform joystickArea;
	private bool touchPresent = false;
	private bool StartWriteTouch=false;
	private Vector2 movementVector;
	public Text PrintData;


	public Vector2 GetTouchPosition
	{
		get { return movementVector;}
	}


	public void BeginDrag()
	{
		touchPresent = true;
		StartWriteTouch=true;
		//Debug.Log("StartWriteTouch ");
		if(TouchStateEvent != null)
			TouchStateEvent(touchPresent);
	}

	public void EndDrag()
	{
		touchPresent = false;
		movementVector = joystickArea.anchoredPosition = Vector2.zero;
		if(TouchStateEvent != null)
			TouchStateEvent(touchPresent);

	}

	public void OnValueChanged(Vector2 value)
	{
		if(touchPresent)
		{
			movementVector.x = ((1 - value.x) - 0.5f)* 2f;
			movementVector.y = ((1 - value.y) - 0.5f) * 0.5f;
			if(movementVector.x>-0.91)
			Sent_x=movementVector.x;
			Sent_y=movementVector.y;
			string str="";
			str+=" X=";str+=Sent_x;
			str+="\n Y=";str+=Sent_y;
			PrintData.text=str;
			if(TouchEvent != null)
			{
				TouchEvent(movementVector);
			}
		}
        //Debug.Log("movementVector "+movementVector);
	}

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobilController : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler 
{
	private Image joystickBG;
	[SerializeField]
	private Image joystick;
	private Vector2 inputVector;//полученные координаты джойстика
	public Text PrintData;

	private void Start()
	{
		joystickBG = GetComponent<Image>();
		joystick = transform.GetChild(0).GetComponent<Image>();
	}
	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag(ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector2.zero;
		joystick.rectTransform.anchoredPosition = Vector2.zero;
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform,ped.position,ped.pressEventCamera, out pos))
		{
			pos.x=(pos.x/joystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джоистик
			pos.y=(pos.y/joystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джоистик
			float dataX = 0;
			if (pos.x < -0.2 || pos.x > 0.2) dataX = pos.x;
			 inputVector =new Vector2(dataX * 4,pos.y*4);//установка точных координат косания (new Vector2(pos.x*2-1,pos.y*2-1))
			inputVector = (inputVector.magnitude>1.0f) ? inputVector.normalized : inputVector;
			string strData = "X=" + pos.x + "\nY=" + pos.y;
			PrintData.text = strData;
			joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x*(joystickBG.rectTransform.sizeDelta.x/6),inputVector.y*(joystickBG.rectTransform.sizeDelta.y/4));
		}
	}

	public float Horizontal()
	{
		if(inputVector.x!=0) 
		{
			return inputVector.x;
		}
		else return Input.GetAxis("Horizontal");
	}
	public float Vertical()
	{
		if(inputVector.y!=0) 
		{
			return inputVector.y;
		}
		else return Input.GetAxis("Vertical");
	}
}

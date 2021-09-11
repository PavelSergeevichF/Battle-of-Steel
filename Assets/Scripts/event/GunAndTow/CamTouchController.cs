using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CamTouchController : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    private Image joystickBG;
	[SerializeField]
	private Image joystick;
	private Vector2 inputVector;//полученные координаты джойстика
    public Text PrintData;
    public float Sent_x, Sent_y;
    float start_x,start_y;
    bool firsttach=true;
    public bool checktouch=false;
    private void Start()
	{
		joystickBG = GetComponent<Image>();
		joystick = transform.GetChild(0).GetComponent<Image>();
	}
	public virtual void OnPointerDown(PointerEventData ped)
	{
        checktouch=true;
		OnDrag(ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector2.zero;
		joystick.rectTransform.anchoredPosition = Vector2.zero;
        checktouch=false;
        firsttach=true;
		Sent_x=0; 
        Sent_y=0;
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform,ped.position,ped.pressEventCamera, out pos))
		{
            if(firsttach)
            {
                firsttach=false;
                start_x=(pos.x/joystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джоистик
			    start_y=(pos.y/joystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джоистик
            }
			pos.x=(pos.x/joystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джоистик
			pos.y=(pos.y/joystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джоистик

			inputVector =new Vector2(pos.x*4,pos.y*4);//установка точных координат косания (new Vector2(pos.x*2-1,pos.y*2-1))
			inputVector = (inputVector.magnitude>1.0f) ? inputVector.normalized : inputVector;

			joystick.rectTransform.anchoredPosition = new Vector2
            (inputVector.x*(joystickBG.rectTransform.sizeDelta.x/2.8f),
             inputVector.y*(joystickBG.rectTransform.sizeDelta.y/2));
             Sent_x=start_x-pos.x; 
             Sent_y=start_y-pos.y;
		}
        string str="";
		str+=" X=";str+=pos.x;
		str+="\n Y=";str+=pos.y;
		PrintData.text=str;
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

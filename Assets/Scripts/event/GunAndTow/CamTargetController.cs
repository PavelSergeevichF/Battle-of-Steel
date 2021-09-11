using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;


public class CamTargetController : MonoBehaviour   //CharecterMechanic
{

	private Vector3 TargetVector;
	public Vector3 TransformCam;
	private CamTouchController TouchController;
	public float ValRotat = 5f;
	public float Sent_X, Sent_Y, Sent_Z;
	float SaveX=0, SaveY=0, tempX, tempY, tempZ, aY=0f;
	//public CharecterMechanic Ch_Mechanic;
	BotPos InfoBotPos;
	bool check=true;
	[SerializeField]
	bool searchTouchControllerPanel = false;

	private PhotonView photonView;//компанент для фотона

	void Start()
	{
		photonView = GetComponent<PhotonView>();
		TouchController =GameObject.Find("CamTouchControllerPanel").GetComponent<CamTouchController>();
		if (TouchController == null) searchTouchControllerPanel = false;
		else searchTouchControllerPanel = true;
	}

	private void FixedUpdate()
	{
		//if (!photonView.IsMine) return;
		if(PhotonNetwork.NickName== PlayerPrefs.GetString("Name"))
        {
			targetCam();
			Mov();
		}
		
		SentDataPos();
		if (!TouchController.checktouch) { SaveX = tempX; SaveY = tempY; }
		if (TouchController == null)
		{
			searchTouchControllerPanel = false;
			TouchController = GameObject.Find("CamTouchControllerPanel").GetComponent<CamTouchController>();
		}
		else searchTouchControllerPanel = true;
	}
	void Mov()
	{
		transform.position = Charecter_Mechanics.MovePoint;
	}
	void targetCam()
	{
		if(TouchController.Sent_x!=0.0f && TouchController.Sent_y!=0.0f)
		{
			TargetVector=Vector3.zero;
			tempX=(TouchController.Sent_x)*3.14f+SaveX;
			tempY=(TouchController.Sent_y)*3.14f+SaveY;//вертикаль
			tempZ=tempX-1.57f;
			if(tempZ<-3.14)tempZ=(3.14f+tempZ)*(-1f)+1.57f;
			float tmpX=Mathf.Sin(tempX);
			tempZ=Mathf.Sin(tempZ);
			if(tempY>0.7f)tempY=0.7f;
			if(tempY<-0.1f)tempY=-0.1f;
			float tmpY=Mathf.Sin(tempY);//вертикаль
			string str="Right Touch:";
			str+="\n X=";str+=tmpX;
			str+="\n Y=";str+=tmpY;
			str+="\n Z=";str+=tempZ;
			TargetVector.x=Sent_X= tmpX* Time.deltaTime*ValRotat;
			TargetVector.z=Sent_Z=tempZ* Time.deltaTime*ValRotat;
			TargetVector.y=Sent_Y= tmpY* Time.deltaTime*ValRotat;//вертикаль
			aY=tmpY;
            Vector3 direct=Vector3.RotateTowards(transform.forward, TargetVector, ValRotat, 0.0f);
			transform.rotation = Quaternion.LookRotation(direct);
			TransformCam = direct;
		}
	}
	void SentDataPos()
	{
        if (InfoBotPos == null) { InfoBotPos = GameObject.Find("InfoBotPos").GetComponent<BotPos>(); }
        else 
		{
			InfoBotPos.setPos(transform.position.x, transform.position.y, transform.position.z);
		}
	}
}

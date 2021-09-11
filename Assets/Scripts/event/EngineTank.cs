using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EngineTank : MonoBehaviour {

    //скорость движения танка
    public float MoveSpeed = 1f;
	public float MaxMoveSpeed = 20f;
    //скорость поворота танка
    float RotateSpeed = 20;
    //текущая скорость
    float CurrentSpeed = 0;
    //скоростя танка 
    int SpeedNum = 0;
    int IdBot;
    //получаем компонент движения танка 
    Rigidbody TankEngine;
    public GameObject Tower;
    //партрон которым будем стрелять
    public GameObject Bullet;
    //старт для нашего ствола
    public GameObject StartStwol;
	private MobilController mContr;
	public Vector3 MovePoint;
    public float Coof=1f;
    //рычаги управления танком 
    public bool FierTank = false;          //стрельба танка 
    //public float RotateTank = 0;           //угол поворота танка 
    public float RotateTower = 0;          //угол поворота башни 
    public float SetGas = 0;               //выбранное направление переключения газа
    //задаем количество }{P для танка
    public float MaxHP = 100;
    //текущее здоровье танка 
    public float CurrentHP = 100;
    //добавляем UI на коотором будем отображать }{
    public Canvas MyGUI;
    //добавляем полоску здоровья которую будем добалвть на экран 
    public Slider MyHPslider;
    public Transform metka;
    //ссылка на камеру из которой смотрим 
    public Camera MyCamera;
    Slider MyShowHP;
    
 // Use this for initialization
 void Start ()
  {
        TankEngine = GetComponent<Rigidbody>();//_________________________________________________получаем компонент движения танка 
		mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobilController>();//__получаем координаты положения джойстика
        IdBot=PlayerPrefs.GetInt("IdBot");
        Coof=0.012f;
		//Mass_Data=GameObject.Find("MassDataObj").GetComponent<MassData>();//______________________получаем массу объекта
        //MyShowHP = (Slider)Instantiate(MyHPslider);//___________________________________________создаем новый slider на основе эталона 
        //MyShowHP.transform.SetParent(MyGUI.transform, true);//__________________________________Обьявляем что он будет расположен в canvas
        //MyShowHP.maxValue = MaxHP;
        //MyShowHP.value = CurrentHP;
    }
 // Update is called once per frame
 void Update ()
  {
    //запоминаем что нажал пользователь
    //float Axis = Input.GetAxis("Vertical");
    //float Axis = SetGas;
    /* if (Input.GetButtonUp("Vertical"))
    {
        if (Axis > 0) UpSpeed();
        if (Axis < 0) DownSpeed();
	}*/
    if (MyShowHP != null) 
    {
        Vector3 screenPos = MyCamera.WorldToScreenPoint(metka.position);//получаем экранные координаты расположения танка
        MyShowHP.transform.position = screenPos;//задаем координаты располопжения ХП
        MyShowHP.value = CurrentHP;
    }
    //если }{ танка равно 0 мы его убиваем 
    if (CurrentHP == 0)
    {
        Destroy(MyShowHP.gameObject);
        Destroy(this.gameObject);
    }
 }
    void FixedUpdate()
    {
        Fier();
        //двигаем танк
        Move();
        //поворачиваем танк 
        Rotates();
        //вызов поворота башни
        RotatesTower();
    }
  void OnGUI()
    {
        
    }
     //увеличиваем скорость движения танка 
    void UpSpeed()
    {
        if ((SpeedNum + 1) < 6) SpeedNum++;//_____________________________________________________если скорость не выше 6 то будем ускорять танк 
    }
      //уменьшение скорости
    void DownSpeed()
    {
        if ((SpeedNum - 1) > -2) SpeedNum--;
    }	
    void OnCollisionEnter(Collision collision)
    {
       MoveSpeed=0.1f;
    }
    void Fier() 
    {
        //включаем управление через переменную 
        if (FierTank) 
       // if (Input.GetButtonUp("Fier4")) 
        {
            Vector3 SpawnPoint = StartStwol.transform.position;//________________________________________получаем текущюю координату для создания снаряда
            Quaternion SpawnRoot = StartStwol.transform.rotation;//______________________________________создаем угол поворота для старта снаряда
            GameObject Pula_for_faer = Instantiate(Bullet, SpawnPoint, SpawnRoot) as GameObject;//_______создаем снаряд
            Rigidbody Run = Pula_for_faer.GetComponent<Rigidbody>();//___________________________________пробуем получить компонент 
            Run.AddForce(Pula_for_faer.transform.forward * 50, ForceMode.Impulse);//_____________________придаем ускорение снаряду
            Destroy(Pula_for_faer, 5);//_________________________________________________________________удаляем партрон через 5  секунд 
            FierTank = false;//__________________________________________________________________________после того как танк выстрелил отключаем выстрел 
        }

    }
    void Move()
    {
        //Vector3 Move = transform.forward * CurrentSpeed * MoveSpeed * Time.deltaTime;
		if(mContr.Vertical()!=0 ){if(MoveSpeed<MaxMoveSpeed)MoveSpeed+=0.1f;} else {MoveSpeed=1;}//______расчитываем куда будет двигаться танк
        MoveSpeed=MoveSpeed*Coof*PlayerPrefs.GetInt("Speed"+IdBot);
		Vector3 Move = transform.forward * -mContr.Vertical() * MoveSpeed * Time.deltaTime;
        Vector3 Poze = TankEngine.position + Move;//_____________________________________________________получаем ткущую позицию танка
        if(Poze.x>400)Poze.x=400;
        if(Poze.x<0)Poze.x=0;
        if(Poze.z>400)Poze.z=400;
        if(Poze.z<0)Poze.z=0;
        TankEngine.MovePosition(Poze);//_________________________________________________________________пробуем двигаться в указанном направлении
		MovePoint=Poze;//________________________________________________________________________________
        //Debug.Log("MoveSpeed_"+MoveSpeed+" Vector3 Move_"+Move+" Coof_"+Coof+" Speed+ID"+PlayerPrefs.GetInt("Speed"+IdBot));
    }
    void Rotates()
    {
        //расчитываем поворот
        //float R = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
		float R = mContr.Horizontal()*-1 * RotateSpeed * Time.deltaTime;
        //включаем поворот через переменную 
        //float R = RotateTank * RotateSpeed * Time.deltaTime;
        //создаем новый угол поворота танка 
        Quaternion RotateAngle = Quaternion.Euler(0, R, 0);
        //получим текущий угол поворота танка 
        Quaternion CurrentUgol = TankEngine.rotation * RotateAngle;
        //поворачиваем танк
        TankEngine.MoveRotation(CurrentUgol);
        //после поворта танка обнуляем значение 
        //RotateTank = 0;
    }
    //поворот башни
    void RotatesTower()
    {
        //RotateSpeed
       // float AngleRotate = Time.deltaTime * RotateSpeed * Input.GetAxis("MoveTower");
        //поворот башни 
        float AngleRotate = Time.deltaTime * RotateSpeed * RotateTower;
      //  AngleRotate = Time.deltaTime * RotateSpeed * Input.GetAxis("Mouse X");
        //поворот башни кнопками
       // Tower.transform.Rotate(0, 0, AngleRotate);
        //после поворота башни обнуляем значнеие 
        RotateTower = 0;
    }

}


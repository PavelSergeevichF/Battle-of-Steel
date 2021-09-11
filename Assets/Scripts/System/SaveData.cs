using UnityEngine;
using System.IO;
using System;

public class SaveData : MonoBehaviour
{
    public GameObject namePan;
    public GameObject Checkpassword;
    public GameObject ButtonInputData;

    private Save sv = new Save();
    private string path;
    private string nameIn;
    private string passwordIn;
    public bool ExitUser=false;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "SaveData.fsh");
#else
        path = Path.Combine(Application.dataPath, "SaveData.fsh");//json
#endif
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            Debug.Log("Добро пожаловать: " + sv.name + "\nВаш пароль: " + sv.password);
        }
        else namePan.SetActive(true);
    }
    void Update () 
    {
        if(ExitUser) namePan.SetActive(true);
    }
    public void CheckName(string name)
    {
        if (!string.IsNullOrEmpty(name) && name.Length >= 3)
        {
            sv.name = name;
        }
        else Debug.Log("Введите нормальное имя!");
    }
    public void CheckPassword(string password)//password age CheckAge
    {
        if (!string.IsNullOrEmpty(password) && password.Length > 5)
        {
           sv.password = password; //int.Parse(password);
        }
        else Debug.Log("Пароль слишком короткий!");
    }
    public void CheckPasswordUser()
    {
        Checkpassword.SetActive(true);
        ButtonInputData.SetActive(false);
    }
    public void EnterDataUser()
    {
        namePan.SetActive(false);
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause) File.WriteAllText(path, JsonUtility.ToJson(sv));
    }
#endif
    private void OnApplicationQuit()
    {
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }
}
[Serializable]
public class Save
{
    public string name;
    public int IDBotSave;
    public string password;
}

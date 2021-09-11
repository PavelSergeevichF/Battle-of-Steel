using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Reflection;
using System.IO;

public class GetMethod : MonoBehaviour
{
    
    void Start()
    {
        //ShowElementPhoton();
    }

    void ShowElementPhoton()
    {
        Type t = typeof(PhotonNetwork); // получить объект класса Type, представляющий класс PhotonNetwork
        Debug.Log("Анализ методов, определенных " + "в классе " + t.Name);
        Debug.Log("Поддерживаемые методы: ");
        MethodInfo[] mi = t.GetMethods();
        // Вывести методы, поддерживаемые в классе PhotonNetwork.
        List<string> metodsStr = new List<string>();
        foreach (MethodInfo m in mi)
        {
            string metodStr = "";
            // Вывести возвращаемый тип и имя каждого метода.
            metodStr += " " + m.ReturnType.Name + " " + m.Name + "(";
            // Вывести параметры.
            ParameterInfo[] pi = m.GetParameters();
            for (int i = 0; i < pi.Length; i++)
            {
                metodStr += pi[i].ParameterType.Name + " " + pi[i].Name;
                if (i + 1 < pi.Length) metodStr += ", ";
            }
            metodStr += ")";
            metodsStr.Add(metodStr);
            Debug.Log(metodStr);
        }
        string path = @"D:\temp\";
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }
        string tempStr = "";
        foreach (string str in metodsStr)
        {
            tempStr += str + "\n";
        }
        using (FileStream fstream = new FileStream($"{path}note.txt", FileMode.OpenOrCreate))
        {
            // преобразуем строку в байты
            byte[] array = System.Text.Encoding.Default.GetBytes(tempStr);
            // запись массива байтов в файл
            fstream.Write(array, 0, array.Length);
            Debug.Log("Текст записан в файл");
        }
    }
}

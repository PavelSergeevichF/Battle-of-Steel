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
        Type t = typeof(PhotonNetwork); // �������� ������ ������ Type, �������������� ����� PhotonNetwork
        Debug.Log("������ �������, ������������ " + "� ������ " + t.Name);
        Debug.Log("�������������� ������: ");
        MethodInfo[] mi = t.GetMethods();
        // ������� ������, �������������� � ������ PhotonNetwork.
        List<string> metodsStr = new List<string>();
        foreach (MethodInfo m in mi)
        {
            string metodStr = "";
            // ������� ������������ ��� � ��� ������� ������.
            metodStr += " " + m.ReturnType.Name + " " + m.Name + "(";
            // ������� ���������.
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
            // ����������� ������ � �����
            byte[] array = System.Text.Encoding.Default.GetBytes(tempStr);
            // ������ ������� ������ � ����
            fstream.Write(array, 0, array.Length);
            Debug.Log("����� ������� � ����");
        }
    }
}

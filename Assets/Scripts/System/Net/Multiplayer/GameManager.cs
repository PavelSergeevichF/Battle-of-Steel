using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    static public GameObject PlayerPrefab;
    public GameObject PlayerPrefabLBT;
    public GameObject PlayerPrefabSBT;
    public GameObject PlayerPrefabLT;
    public GameObject PlayerPrefabTT;
    public GameObject[] PlayerSpawnObjA = new GameObject[10];
    public GameObject[] PlayerSpawnObjB = new GameObject[10];
    public position_Spavn[,] pos_spawn = new position_Spavn[2,10];
    public Object Id;
    // Start is called before the first frame update
    void Start()
    {
        setSpavnPos();
        setBot();
        int SpawnPoint = Random.Range(0, 9);
        Vector3 pos = new Vector3(pos_spawn[1, SpawnPoint].x, pos_spawn[1, SpawnPoint].y, pos_spawn[1, SpawnPoint].z);
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
       //Id=
    }
    void setBot()
    {
        switch(PlayerPrefs.GetInt("IdBot"))
        {
            case 1:
                PlayerPrefab = PlayerPrefabLBT;
                break;
            case 2:
                PlayerPrefab = PlayerPrefabSBT;
                break;
            case 3:
                PlayerPrefab = PlayerPrefabLT;
                break;
            case 4:
                PlayerPrefab = PlayerPrefabTT;
                break;
        }
    }
    public void Lave()
    {
        PhotonNetwork.LeaveRoom();//функция добровольно покинуть комнату
    }
    public override void OnLeftRoom()
    {
        //когда текущий игрок (мы) покидаем комнату
        SceneManager.LoadScene(1);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} enter room", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
    void setSpavnPos()
    {
        for(int i=0;i<10;i++)//[0,i] point A
        {
            pos_spawn[0, i].x = PlayerSpawnObjA[i].transform.position.x;
            pos_spawn[0, i].y = PlayerSpawnObjA[i].transform.position.y;
            pos_spawn[0, i].z = PlayerSpawnObjA[i].transform.position.z;
        }
        for (int i = 0; i < 10; i++)//[0,i] point B
        {
            pos_spawn[1, i].x = PlayerSpawnObjB[i].transform.position.x;
            pos_spawn[1, i].y = PlayerSpawnObjB[i].transform.position.y;
            pos_spawn[1, i].z = PlayerSpawnObjB[i].transform.position.z;
        }
    }
}
public struct position_Spavn
{
    public float x,y,z;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDestroySystem : MonoBehaviour
{
    public Renderer r;
    public SoundFire[] Sound;
    public int SetSoundMG=0;
    public int SetSoundG=1;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag=="BullTag")//ShellPrefabCube
        {
            Sound[SetSoundMG].ShotPlay();
            r.material.color=Color.yellow;
        }
        if(Col.gameObject.tag=="ShellTag")//ShellPrefabCube
        {
            Sound[SetSoundG].ShotPlay();
            r.material.color=Color.red;
        }
    }
}

using UnityEngine;
using UnityEngine.Advertisements;

public class SimpleAds : MonoBehaviour
{
    public  void ShowAd()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        
    }
}

using UnityEngine;
using UnityEngine.Advertisements;

public class ShowVidioAds : MonoBehaviour
{
    public GameObject ButtonAds;
    public bool CheckShow=false;
    public int TimePause=2,G_set=1;
    public TimeGetNet timeGetNet;
    public ExchangeNet exchangeNet;
    void Start ()
    {
        ButtonAds.SetActive(false);
    }
    void Update()
    {
        if(timeGetNet.minutes%TimePause==0&&!CheckShow){ButtonAds.SetActive(true);CheckShow=true;}
        if(timeGetNet.minutes%TimePause!=0&&CheckShow)CheckShow=false;
    }
    public void Play2Ads() 
    {
         if(Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show();
        }
        exchangeNet.moneySet(G_set,0,0,1);
    }
    public void PlayAds()
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            var options= new ShowOptions{resultCallback=HandleShowResult};
            Debug.Log("options "+options);
            Advertisement.Show("rewardedVidio",options);
        }
    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                //YOUR CODE TO REWARD THE GAME
                //Gig coints etc
                exchangeNet.moneySet(G_set,0,0,1);
                ButtonAds.SetActive(false);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad eas skipped reaching the end");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                ButtonAds.SetActive(false);
                break;
        }
    }
}

using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : Singleton<AdManager>
{
    public bool testMode = true;

    private string _appID = "ca-app-pub-5196837691024580~9901211378";
    private string _testAppId = "ca-app-pub-3940256099942544~3347511713";

    private InterstitialAd _endLevelAd;
    private string _endLevelAdID = "ca-app-pub-5196837691024580/7798911770";
    private string _testAdUnitId = "ca-app-pub-3940256099942544/1033173712";

    private void Start()
    {
        RequestEndLevelAd();
    }

    public void RequestEndLevelAd()
    {
        string id = testMode ? _testAdUnitId : _endLevelAdID;
        _endLevelAd = new InterstitialAd(id);

        AdRequest request = new AdRequest.Builder().Build();

        _endLevelAd.LoadAd(request);
    }

    public void ShowEndLevelAd()
    {
        if (_endLevelAd.IsLoaded())
        {
            _endLevelAd.Show();
        }
        else
        {
            Debug.Log("ad not loaded");
        }
    }
}

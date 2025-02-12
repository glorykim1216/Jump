﻿using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class LoadBanner : MonoBehaviour
{
	RewardBasedVideoAd ad;
	[SerializeField] string appId;
	[SerializeField] string unitId;
	[SerializeField] bool isTest;
	[SerializeField] string deviceId;

    BannerView banner;
    public AdSize size;
    public AdPosition position;
    bool active;
    void Start ()
	{
		
    }

    private void OnEnable()
    {
        MobileAds.Initialize(appId);
        ad = RewardBasedVideoAd.Instance;

        //광고 요청이 성공적으로 로드되면 호출됩니다.
        ad.OnAdLoaded += OnAdLoaded;
        //광고 요청을 로드하지 못했을 때 호출됩니다.
        ad.OnAdFailedToLoad += OnAdFailedToLoad;
        //광고가 표시될 때 호출됩니다.
        ad.OnAdOpening += OnAdOpening;
        //광고가 재생되기 시작하면 호출됩니다.
        ad.OnAdStarted += OnAdStarted;
        //사용자가 비디오 시청을 통해 보상을 받을 때 호출됩니다.
        ad.OnAdRewarded += OnAdRewarded;
        //광고가 닫힐 때 호출됩니다.
        ad.OnAdClosed += OnAdClosed;
        //광고 클릭으로 인해 사용자가 애플리케이션을 종료한 경우 호출됩니다.
        ad.OnAdLeavingApplication += OnAdLeavingApplication;

        LoadAd();
        InitAd();
        banner.Show();
    }

    private void OnDisable()
    {
        banner.Hide();
        active = true;
    }

    void InitAd()
    {
        string id = "ca-app-pub-3940256099942544/6300978111";
        banner = new BannerView(id, AdSize.SmartBanner, position);

        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(deviceId).Build();

        banner.LoadAd(request);

        //banner.Show();
    }

    public void ToggleAd()
    {
        if (active)
        {
            banner.Show();
            active = false;
        }
        else
        {
            banner.Hide();
            active = true;
        }

    }

    void LoadAd()
	{
		AdRequest request = new AdRequest.Builder().Build();
		if (isTest)
		{
			if(deviceId.Length > 0)
				request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(deviceId).Build();
			else
				unitId = "ca-app-pub-3940256099942544/5224354917"; //테스트 유닛 ID
			
		}
		ad.LoadAd(request, unitId);
    }

	public void OnBtnViewAdClicked()
	{
		if (ad.IsLoaded())
		{
			//Debug.Log("View Ad");
			ad.Show();
		}
		else
		{
			//Debug.Log("Ad is Not Loaded");
			LoadAd();
		}
	}

	void OnAdLoaded(object sender, EventArgs args) { //Debug.Log("OnAdLoaded"); 
    }
	void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e) { //Debug.Log("OnAdFailedToLoad");
    }
	void OnAdOpening(object sender, EventArgs e) { //Debug.Log("OnAdOpening"); 
    }
	void OnAdStarted(object sender, EventArgs e) { //Debug.Log("OnAdStarted"); 
    }
	void OnAdRewarded(object sender, Reward e) { //Debug.Log("OnAdRewarded"); 
    }
	void OnAdClosed(object sender, EventArgs e)
	{
		//Debug.Log("OnAdClosed");
		LoadAd();
	}
	void OnAdLeavingApplication(object sender, EventArgs e) { //Debug.Log("OnAdLeavingApplication"); 
    }
}
using System;
using System.Net;
using System.IO;
using UnityStandardAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
    public class Weather
{
    public int id;
    public string main;
}

[Serializable]
public class WeatherInfo
{
    public int id;
    public string name;
    public List<Weather> weather;
}


public class WeatherController : MonoBehaviour
{

    private const string API_KEY = "e59e5d7a544f92c306bbea9152c59d5d";
    private const float API_CHECK_TIME = 10 * 60.0f;
    public GameObject rainPrefab;
    public Material sunSkybox;
    public Material rainSkybox;
    public string cityId;
    private float apiCheckTime = API_CHECK_TIME;

    
    void Start()
    {
        CheckRainStatus();
        CheckOvercast();
    }

    // Update is called once per frame
    void Update()
    {
        apiCheckTime -= Time.deltaTime;
        if (apiCheckTime <= 0)
        {
            CheckRainStatus();
            apiCheckTime = API_CHECK_TIME;
        }
    }

    public void CheckRainStatus()
    {
        bool raining = GetWeather().weather[0].main.Equals("Rain");
        if (raining)
        {
            RenderSettings.skybox = rainSkybox;
            rainPrefab.SetActive(true);
        }
        else
        {
            RenderSettings.skybox = sunSkybox;
            rainPrefab.SetActive(false);
        }
    }

    public void CheckOvercast()
    {
        bool overcast = GetWeather().weather[0].main.Equals("Clouds");
        if (overcast)
        {
            RenderSettings.skybox = rainSkybox;
            rainPrefab.SetActive(false);
        }
        else
        {
            RenderSettings.skybox = sunSkybox;
            rainPrefab.SetActive(false);
        }
    }


    private WeatherInfo GetWeather()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", cityId, API_KEY));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
        return info;
    }
   
}

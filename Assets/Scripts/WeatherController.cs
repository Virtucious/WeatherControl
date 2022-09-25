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
    public string cityId;

    private WeatherInfo GetWeather()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", cityId, API_KEY));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
        return info;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

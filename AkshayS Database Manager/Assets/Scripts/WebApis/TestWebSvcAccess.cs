using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

public class TestWebSvcAccess : MonoBehaviour
{
    void Start()
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync("https://localhost:44332/weatherforecast").Result;
        gameObject.GetComponent<Text>().text = JsonConvert.DeserializeObject<WebApisClassDefs.TestWebSvcResponse>(
            httpResponseMessage.Content.ReadAsStringAsync().Result)?.testStrings?.First<WebApisClassDefs.TestWebSvcResponse.TestString>()?.testString1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthSpotify : MonoBehaviour
{
    void Start()
    {
        Application.OpenURL("https://localhost:44325/SpotifyAuth"); 
    }

    void Update()
    {
        
    }
}

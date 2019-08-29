using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SenceJump : MonoBehaviour
{
    AsyncOperation test;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Click()
    {
        AsyncOperation test= SceneManager.LoadSceneAsync("Restrant");
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}

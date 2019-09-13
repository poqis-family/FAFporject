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
        string jumpSence = gameObject.name;
        AsyncOperation test= SceneManager.LoadSceneAsync(jumpSence);
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}

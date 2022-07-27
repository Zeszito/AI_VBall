using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartLEvel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

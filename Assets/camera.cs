using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    public float cameraHeight = 47.19f;
    public float cameraflet = 2.81f;
    public float camerarigth = -5.6f;



    private void Start()
    {
        cameraHeight = 47.19f;
        cameraflet = 2.81f;
        camerarigth = -5.6f;
    }
    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.z = cameraHeight;
        pos.x += camerarigth;
        pos.y += cameraflet;
        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Rotate(float cameraRotationX)
    {
        this.transform.eulerAngles = new Vector3(cameraRotationX, player.transform.eulerAngles.y, 0f);
    }
}

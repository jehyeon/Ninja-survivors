using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float offsetZ = -5.0f;
    public float offsetY = 1.5f;
    public float delay = .5f;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // FollowPlayer();
    }

    // private void FollowPlayer()
    // {
    //     Debug.DrawRay(this.transform.position, this.transform.forward, Color.red);

    //     Vector3 cameraPos = new Vector3(
    //         player.transform.position.x, 
    //         player.transform.position.y + offsetY, 
    //         player.transform.position.z + offsetZ);

    //     this.transform.position = cameraPos;
    // }

    public void Rotate(float cameraRotationX)
    {
        this.transform.eulerAngles = new Vector3(cameraRotationX, player.transform.eulerAngles.y, 0f);
    }
}

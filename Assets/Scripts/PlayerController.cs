using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float gravity;
    public float lookSensitivity;   // 감도
    private Vector3 moveDir;

    private CharacterController cc;
    private GameObject playerCamera;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        Debug.Log(Mathf.Clamp(-1, -50, 30));
        Debug.Log(Mathf.Clamp(0, -50, 30));
        Debug.Log(Mathf.Clamp(1, -50, 30));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {   
        // 평면 이동
        moveDir = transform.TransformDirection(
            Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.forward
        );
        // moveDir.Normalize();

        // Jump
        // if (Input.GetButtonDown("Jump"))
        // {
        //     moveDir.y = jumpPower;
        // }

        cc.Move(moveDir * speed * Time.deltaTime);
    }

    private void Rotate()
    {
        // Player 좌우 시야 전환
        float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        float playerRotationY = this.transform.eulerAngles.y + mouseX;
        this.transform.eulerAngles = new Vector3(0f, playerRotationY, 0f);

        // Camera 상하 시야 전환
        float mouseY = -1 * Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        float rotate = playerCamera.transform.eulerAngles.x + mouseY;
        if (rotate > 180)
        {
            rotate = (rotate - 360);
        }
        float cameraRotationX = Mathf.Clamp(rotate, -50f, 30f);
        playerCamera.GetComponent<PlayerCamera>().Rotate(cameraRotationX);
    }
}

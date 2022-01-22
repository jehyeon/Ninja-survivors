using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float gravity;
    public float lookSensitivity;   // 감도
    private Vector3 moveDir = Vector3.zero;

    private CharacterController cc;
    private GameObject playerCamera;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {   
        if (cc.isGrounded)
        {
            // 평면 이동
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            // Jump
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpPower;
            }
            moveDir.y -= gravity * 0.1f;    // small gravity
        }
        
        moveDir.y -= gravity * Time.deltaTime;

        cc.Move(moveDir * Time.deltaTime);
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

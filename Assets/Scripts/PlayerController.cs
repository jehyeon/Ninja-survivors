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
    private Animator animator;
    private GameObject playerCamera;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
        // if (cc.isGrounded)
        // {
            float h = Input.GetAxis("Horizontal"); 
            float v = Input.GetAxis("Vertical");

            // 평면 이동
            moveDir = new Vector3(h, 0, v);
            moveDir = transform.TransformDirection(moveDir);
            if (v == 1 && h == 0)
            {
                // sprint
                moveDir *= speed * 2f;
                Debug.Log(moveDir);   
            }
            else
            {
                moveDir *= speed;
                Debug.Log(moveDir);  
            }

            // Jump
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpPower;
            }
            // moveDir.y = -1 * gravity * 0.1f;    // small gravity

            // Animator
            if (h != 0 || v != 0)
            {
                animator.SetBool("isMove", true);
            }
            
            animator.SetFloat("MoveX", h);
            animator.SetFloat("MoveY", v);
        // }
        
        // moveDir.y -= gravity * Time.deltaTime;

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

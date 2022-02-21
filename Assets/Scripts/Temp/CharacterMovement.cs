using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 moveDir = Vector3.zero;
    private CharacterController cc;
    private Animator animator;
    private bool isJump;
    private float jumpingTime = 0f;
    private float h;
    private float v;
    public float softmove = 5f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        // Rotate();
    }

    private void Move()
    {
        // if (jumpingTime > 0.2f)
        // {
        //     animator.SetBool("isRand", false);
        // }

        // if (IsGrounded())
        // {
            // if (isJump && jumpingTime > 0.1f)
            // {
            //     // 점프 상태에서 ground를 밟게 되면
            //     animator.SetBool("isJump", false);
            //     animator.SetBool("isRand", true);
            //     jumpingTime = 0f;
            //     isJump = false;
            // }
        
            // float h = Input.GetAxis("Horizontal"); 
            // float v = Input.GetAxis("Vertical");
        
        
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            h += Time.deltaTime * softmove;
            if (h > 1)
            {
                h = 1;
            }
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            h -= Time.deltaTime * softmove;
            if (h < -1)
            {
                h = -1;
            }
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (h > 0)
            {
                h -= Time.deltaTime * softmove;
                if (h < 0)
                {
                    h = 0;
                }
            }
            if (h < 0)
            {
                h += Time.deltaTime * softmove;
                if (h > 0)
                {
                    h = 0;
                }
            }
        }

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            v += Time.deltaTime * softmove;
            if (v > 1)
            {
                v = 1;
            }
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            v -= Time.deltaTime * softmove;
            if (v < -1)
            {
                v = -1;
            }
        }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            if (v > 0)
            {
                v -= Time.deltaTime * softmove;
                if (v < 0)
                {
                    v = 0;
                }
            }
            if (v < 0)
            {
                v += Time.deltaTime * softmove;
                if (v > 0)
                {
                    v = 0;
                }
            }
        }

            // 평면 이동
            moveDir = new Vector3(h, 0, v);
            moveDir = transform.TransformDirection(moveDir);
            if (v == 1 && h == 0)
            {
                // sprint
                moveDir *= 1f * 1.5f;
            }
            else
            {
                moveDir *= 1f;
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = 5f;
                // animator.SetBool("isJump", true);
                animator.SetTrigger("isJump");
            }


            // Animator
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }
            animator.SetFloat("moveX", h);
            animator.SetFloat("moveY", v);
        // }
        // else
        // {
        //     jumpingTime += Time.deltaTime;
        //     isJump = true;
        // }
        
        // moveDir.y -= gravity * Time.deltaTime;

        cc.Move(moveDir * Time.deltaTime * 5f);
    }

    private void Rotate()
    {
        // Player 좌우 시야 전환
        float mouseX = Input.GetAxisRaw("Mouse X") * 2f;
        float playerRotationY = this.transform.eulerAngles.y + mouseX;
        this.transform.eulerAngles = new Vector3(0f, playerRotationY, 0f);

        // // Camera 상하 시야 전환
        // float mouseY = -1 * Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        // float rotate = playerCamera.transform.eulerAngles.x + mouseY;
        // if (rotate > 180)
        // {
        //     rotate = (rotate - 360);
        // }
        // float cameraRotationX = Mathf.Clamp(rotate, -50f, 30f);
        // playerCamera.GetComponent<PlayerCamera>().Rotate(cameraRotationX);
    }

    // private bool IsGrounded()
    // {
    //     RaycastHit hit;
    //     Physics.Raycast(this.transform.position, Vector3.down, out hit, .15f);
    //     // Debug.DrawRay(this.transform.position, Vector3.down * 0.15f);
    //     if (hit.transform == null)
    //     {
    //         return false;
    //     }
    //     return hit.transform.CompareTag("Ground");
    // }

    private void Attack()
    {
        // 공격 애니메이션만 처리
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttack");
            animator.SetInteger("attackType", Random.Range(0, 3));
        }
    }
}

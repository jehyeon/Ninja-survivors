using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public float lookSensitivity;   // 감도
    private Vector3 moveDir = Vector3.zero;

    private Stat stat;
    private CharacterController cc;
    private Animator animator;
    private GameObject playerCamera;
    private bool isJump;
    private float jumpingTime = 0f;
    
    // 스킬 커맨드
    private SkillCommandManager skillCommandManager = null;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerCamera = GameObject.Find("Main Camera");
        stat = GetComponent<Stat>();

        // 스킬 커맨드 생성
        skillCommandManager = new SkillCommandManager();
        // Dash dashSkillCommand = new Dash(this.transform);
        Dash dashSkillCommand = gameObject.AddComponent<Dash>();
        dashSkillCommand.SetTransform(this.transform);
        skillCommandManager.SetSkillCommand("Dash", dashSkillCommand);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Rotate();
    }

    private void Move()
    {
        if (jumpingTime > 0.2f)
        {
            animator.SetBool("isRand", false);
        }

        if (IsGrounded())
        {
            if (isJump && jumpingTime > 0.1f)
            {
                // 점프 상태에서 ground를 밟게 되면
                animator.SetBool("isJump", false);
                animator.SetBool("isRand", true);
                jumpingTime = 0f;
                isJump = false;
            }

            float h = Input.GetAxis("Horizontal"); 
            float v = Input.GetAxis("Vertical");

            // 평면 이동
            moveDir = new Vector3(h, 0, v);
            moveDir = transform.TransformDirection(moveDir);
            if (v == 1 && h == 0)
            {
                // sprint
                moveDir *= stat.Speed * 1.5f;
            }
            else
            {
                moveDir *= stat.Speed;
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = stat.JumpPower;
            }

            // 이동기
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                skillCommandManager.InvokeExecute("Dash");
            }

            // Animator
            if (h != 0 || v != 0)
            {
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }
            animator.SetFloat("MoveX", h);
            animator.SetFloat("MoveY", v);
        }
        else
        {
            jumpingTime += Time.deltaTime;
            isJump = true;
            animator.SetBool("isJump", true);
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

    private bool IsGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, Vector3.down, out hit, .15f);
        // Debug.DrawRay(this.transform.position, Vector3.down * 0.15f);
        if (hit.transform == null)
        {
            return false;
        }
        return hit.transform.CompareTag("Ground");
    }

    private void Attack()
    {
        // 공격 애니메이션만 처리
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttack");
            animator.SetInteger("AttackType", Random.Range(0, 3));
        }
    }
}

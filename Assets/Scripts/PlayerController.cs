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
    
    // for soft move
    private float h;
    private float v;
    private float softMoveOffset = 3f;   // 고정
    public float realSpeed;
    public float realJumpPower;

    private float previousY;
    public bool isGround;
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
        // 바닥 체크
        bool isGround = IsGrounded();

        // temp
        stat.Speed = realSpeed;
        stat.JumpPower = realJumpPower;

        // Movement
        // 키 입력 및 시간에 따라 moveDir를 위한 h, v 조정
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            h += Time.deltaTime * softMoveOffset;
            if (h > 1)
            {
                h = 1;
            }
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            h -= Time.deltaTime * softMoveOffset;
            if (h < -1)
            {
                h = -1;
            }
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (h > 0)
            {
                h -= Time.deltaTime * softMoveOffset;
                if (h < 0)
                {
                    h = 0;
                }
            }
            if (h < 0)
            {
                h += Time.deltaTime * softMoveOffset;
                if (h > 0)
                {
                    h = 0;
                }
            }
        }

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            v += Time.deltaTime * softMoveOffset;
            if (v > 1)
            {
                v = 1;
            }
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            v -= Time.deltaTime * softMoveOffset;
            if (v < -1)
            {
                v = -1;
            }
        }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            if (v > 0)
            {
                v -= Time.deltaTime * softMoveOffset;
                if (v < 0)
                {
                    v = 0;
                }
            }
            if (v < 0)
            {
                v += Time.deltaTime * softMoveOffset;
                if (v > 0)
                {
                    v = 0;
                }
            }
        }

        moveDir = new Vector3(h, 0, v);
        moveDir = transform.TransformDirection(moveDir);    // 로컬 좌표로 변환

        // Speed 적용, 정면 달리기는 가속
        if (v == 1 && h == 0)
        {
            // sprint
            moveDir *= stat.Speed * 1.3f;
        }
        else
        {
            moveDir *= stat.Speed;
        }

        // 애니메이션 제어
        if (isGround)
        {
            previousY = 0f;
            // 바닥에서만 move animation 재생
            animator.SetFloat("moveX", h);
            animator.SetFloat("moveY", v);
            animator.SetFloat("moveSpeed", stat.Speed * 0.15f);
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                previousY += stat.JumpPower;
                animator.SetTrigger("isJump");
                Invoke("Jumping", 0.03f);   // 0.03초 이후 지면검사를 다시 함
            }
        }

        // 이동기
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            skillCommandManager.InvokeExecute("Dash");
        }

        moveDir.y = previousY;
        moveDir.y -= gravity * Time.deltaTime;      // 중력 적용
        previousY = moveDir.y;

        cc.Move(moveDir * Time.deltaTime);

        if (isGround)
        {
            animator.SetBool("isGround", true);
        }
        else
        {
            animator.SetBool("isGround", false);
        }
    }

    private void Rotate()
    {
        // Player 좌우 시야 전환
        float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        float playerRotationY = this.transform.eulerAngles.y + mouseX;
        this.transform.eulerAngles = new Vector3(0f, playerRotationY, 0f);

        // !!! 상체 각도 조절

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

    private void Jumping()
    {
        isGround = false;
    }

    private bool IsGrounded()
    {
        if (isGround)
        {
            // 착지 상태면 더이상 체크 안함
            return true;
        }

        if (cc.isGrounded)
        {
            // 캐릭터 컨트롤러에서 확인되면 ray 체크 안함
            return true;
        }

        // 캐릭터 컨트롤 isGrounded false인 경우 check ray
        RaycastHit hit;
        // Debug.DrawRay(this.transform.position, Vector3.down * 0.15f, Color.red);
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 0.15f, 1 << 6))
        {
            // 바닥이 ground인 경우
            return true;   
        }

        return false;
    }

    private void Attack()
    {
        // 공격 애니메이션만 처리
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("attackType", Random.Range(0, 3));
            animator.SetTrigger("isAttack");
        }
    }

}

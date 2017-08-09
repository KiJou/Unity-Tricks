using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("重力")]
    public float gravity = 9.8f;
    [Header("弹力")]
    public float elasticity = 10.0f;
    [Header("移动速度")]
    public float runSpeed = 5.0f;

    // 玩家刚体
    private Rigidbody mRigidbody;
    // 玩家动画
    private Animation mAnimation;
    // 一段跳标志
    private bool isJump = false;
    // 二段跳标志
    private bool isDoubleJump = false;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mAnimation = GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        // 施加重力 持续力
        mRigidbody.AddForce(Vector3.down * gravity, ForceMode.Force);
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 施加弹力 冲击力
            if (!isJump)
            {
                mRigidbody.AddForce(Vector3.up * elasticity, ForceMode.Impulse);
                mAnimation.CrossFade("Jump");
                isJump = true;
            }
            else if (!isDoubleJump)
            {
                mRigidbody.AddForce(Vector3.up * elasticity / 2, ForceMode.Impulse);
                mAnimation.CrossFade("Attack4"); // 模拟二段跳...
                isDoubleJump = true;
            }
        }

        if (isJump || isDoubleJump) { return; }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Run
        if (Mathf.Abs(h) >= 0.05f || Mathf.Abs(v) >= 0.05f)
        {
            float y = Camera.main.transform.rotation.eulerAngles.y;
            Vector3 targetPos = Quaternion.Euler(0, y, 0) * new Vector3(h, 0, v);
            mRigidbody.velocity = targetPos * runSpeed;
            mAnimation.CrossFade("Run");
        }

        // Jump
        else if (Mathf.Abs(mRigidbody.velocity.y) >= 0.05f)
        {
            mAnimation.CrossFade("Jump");
        }

        // Idle
        else
        {
            mAnimation.CrossFade("Idle");
        }
    }

    // Box Collider Is Trigger => true
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            isJump = false;
            isDoubleJump = false;
        }
    }
}

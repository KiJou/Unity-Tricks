using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移动速度")]
    public float runSpeed = 5.0f;
    [Header("跳跃速度")]
    public float jumpSpeed = 5.0f;

    // 玩家刚体
    private Rigidbody mRigidbody;
    // 玩家动画
    private Animation mAnimation;
    // 是否跳跃
    private bool isJump = false;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mAnimation = GetComponent<Animation>();
    }

    void Update()
    {
        if (isJump) { return; }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Run
        if (Mathf.Abs(h) >= 0.05f || Mathf.Abs(v) >= 0.05f)
        {
            // 正对镜头方向
            float y = Camera.main.transform.rotation.eulerAngles.y;
            Vector3 targetPos = Quaternion.Euler(0, y, 0) * new Vector3(h, 0, v);
            transform.rotation = Quaternion.LookRotation(targetPos);
            mRigidbody.velocity = targetPos * runSpeed;
            mAnimation.CrossFade("Run");
        }
        // Idle
        else
        {
            mRigidbody.velocity = Vector3.zero;
            mAnimation.CrossFade("Idle");
        }
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 targetPos = new Vector3(mRigidbody.velocity.x, jumpSpeed, mRigidbody.velocity.z);
            mRigidbody.velocity = targetPos;
            mAnimation.CrossFade("Jump");
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") { isJump = false; }
    }
}

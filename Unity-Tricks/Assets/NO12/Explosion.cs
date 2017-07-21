using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5.0f;  // 定义爆炸半径  
    public float force = 500.0f;  // 定义爆炸强度
    public float ups = 1.0f;  // 定义上升的力

    private string groundTag = "Ground";

    void OnCollisionEnter(Collision col)
    {
        // 如果碰撞物是地面，进行爆炸处理
        if (col.transform.CompareTag(groundTag))
        {
            // 定义爆炸位置为炸弹位置
            Vector3 explosionPos = transform.position;

            // 这个方法用来返回球型半径之内的所有碰撞体 collider
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            // 遍历返回的碰撞体，如果是刚体，则给刚体添加力
            foreach (Collider hit in colliders)
            {
                if (hit.GetComponent<Rigidbody>())
                {
                    hit.GetComponent<Rigidbody>().AddExplosionForce(force, explosionPos, radius, ups);
                }
                // 销毁地面和炸弹
                Destroy(col.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

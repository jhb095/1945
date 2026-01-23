using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 4.0f;
    public int damage = 10;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            // 이펙트
            GameObject effect = ObjectPool.Instance.GetObject("BulletEffect");
            effect.transform.SetParent(collision.gameObject.transform);
            effect.transform.position = collision.ClosestPoint(transform.position);
            effect.SetActive(true);

            collision.GetComponent<Monster>().HP -= damage;

            // 미사일 비활성화
            gameObject.SetActive(false);
        }
    }
}

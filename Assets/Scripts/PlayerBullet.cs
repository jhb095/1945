using System;
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
        if(collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            // 이펙트
            GameObject effect = ObjectPool.Instance.GetObject("BulletEffect");
            effect.transform.SetParent(collision.transform);
            effect.transform.position = collision.ClosestPoint(transform.position);
            effect.SetActive(true);

            if (collision.CompareTag("Enemy"))
            {
                Action detachHandler = null;
                detachHandler = () =>
                {
                    effect.transform.SetParent(null);
                    collision.GetComponent<Monster>().OnDie -= detachHandler;
                };

                collision.GetComponent<Monster>().OnDie += detachHandler;

                collision.GetComponent<Monster>().HP -= damage;
            }

            // 미사일 비활성화
            gameObject.SetActive(false);
        }
    }
}

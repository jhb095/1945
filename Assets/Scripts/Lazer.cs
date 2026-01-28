using System;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public int damage = 10;

    private float damageInterval = 0.1f;
    private float lastDamageTime = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            CreateEffect(collision);
            collision.GetComponent<Monster>().HP -= damage;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                lastDamageTime = Time.time;
                CreateEffect(collision);
                collision.GetComponent<Monster>().HP -= damage;
            }
        }
    }

    private void CreateEffect(Collider2D target)
    {
        GameObject go = ObjectPool.Instance.GetObject("BulletEffect");

        go.transform.SetParent(target.transform);
        go.transform.position = target.ClosestPoint(transform.position);
        go.SetActive(true);

        Action detachHandler = null;

        detachHandler = () =>
        {
            go.transform.SetParent(null);
            target.GetComponent<Monster>().OnDie -= detachHandler;
        };

        target.GetComponent<Monster>().OnDie += detachHandler;
    }
}

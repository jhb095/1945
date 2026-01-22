using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float speed = 3f;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // 미사일 비활성화
            gameObject.SetActive(false);
        }
    }
}

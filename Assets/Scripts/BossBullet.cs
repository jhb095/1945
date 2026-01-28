using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 3f;

    private Vector2 vec2 = Vector2.down;

    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * vec2);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 미사일 비활성화
            gameObject.SetActive(false);
        }
    }
}

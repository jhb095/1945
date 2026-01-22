using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 4.0f;

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
        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().HP -= 20;

            // 몬스터 사망시 비활성화
            if (collision.GetComponent<Monster>().HP <= 0)
                collision.gameObject.SetActive(false);

            // 미사일 비활성화
            gameObject.SetActive(false);
        }
    }
}

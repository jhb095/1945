using UnityEngine;

public class Homing : MonoBehaviour
{
    public float speed = 3f;

    private GameObject player;
    private Vector2 dir;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * dir);
    }

    private void OnEnable()
    {
        dir = player.transform.position - transform.position;

        // 방향벡터
        dir = dir.normalized;
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

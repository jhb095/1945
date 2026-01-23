using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody2D rb;

    private Vector2 viewportToWorldMin;
    private Vector2 viewportToWorldMax;

    private float itemHalfHeight;
    private float itemHalfWidth;

    private void Awake()
    {
        // 아이템 이미지 너비 높이 구하기
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        itemHalfHeight = sr.bounds.size.y / 2f;
        itemHalfWidth = sr.bounds.size.x / 2f;

        // 뷰포트 -> 월드 좌표 변환
        viewportToWorldMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        viewportToWorldMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.linearVelocity = new Vector2(speed, speed);
    }

    private void FixedUpdate()
    {
        Bounce();
    }

    private void Bounce()
    {
        Vector2 pos = rb.position;
        Vector2 velocity = rb.linearVelocity;

        if (pos.x - itemHalfWidth <= viewportToWorldMin.x && velocity.x < 0f)
        {
            velocity.x = -velocity.x;
            pos.x = viewportToWorldMin.x + itemHalfWidth;
        }
        else if (pos.x + itemHalfWidth >= viewportToWorldMax.x && velocity.x > 0f)
        {
            velocity.x = -velocity.x;
            pos.x = viewportToWorldMax.x - itemHalfWidth;
        }

        if (pos.y - itemHalfHeight <= viewportToWorldMin.y && velocity.y < 0f)
        {
            velocity.y = -velocity.y;
            pos.y = viewportToWorldMin.y + itemHalfHeight;
        }
        else if (pos.y + itemHalfHeight >= viewportToWorldMax.y && velocity.y > 0f)
        {
            velocity.y = -velocity.y;
            pos.y = viewportToWorldMax.y - itemHalfHeight;
        }

        rb.position = pos;
        rb.linearVelocity = velocity;
    }
}

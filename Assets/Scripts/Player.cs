using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float fireRate = 5f;
    public int power = 0;
    public float slowTimeScale = 0.2f;
    public GameObject bomb;

    [SerializeField] private Transform launcherPos;
    [SerializeField] private GameObject powerUp;

    [Header("레이저")]
    [SerializeField] private GameObject lazer;
    [SerializeField] private Image gage;
    [SerializeField] private float gValue = 0f;

    Animator animator;

    private Vector2 dir;
    private Vector2 viewportToWorldMin;
    private Vector2 viewportToWorldMax;

    private float playerHalfHeight;
    private float playerHalfWidth;
    private float fireCooldown;

    private bool isFire;
    private bool isLazer;
    private bool isSlowTime;

    //private float normalDeltaTime;
    
    private void Awake()
    {
        // 플레이어 이미지 너비 높이 구하기
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        playerHalfHeight = sr.bounds.size.y / 2f;
        playerHalfWidth = sr.bounds.size.x / 2f;

        // 뷰포트 -> 월드 좌표 변환
        viewportToWorldMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        viewportToWorldMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
        // 애니메이터 가져오기
        animator = GetComponent<Animator>();
        
        // 초기 델타 타임
        //normalDeltaTime = Time.deltaTime;
    }

    private void Update()
    {
        ClampPlayerPosition();
        Shoot();
        Lazer();
        SlowTime();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();

        if (dir.x <= -0.5f)
            animator.SetBool("isLeft", true);
        else
            animator.SetBool("isLeft", false);

        if (dir.x >= 0.5f)
            animator.SetBool("isRight", true);
        else
            animator.SetBool("isRight", false);

        if (dir.y >= 0.5f)
            animator.SetBool("isUp", true);
        else
            animator.SetBool("isUp", false);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            isFire = true;
        else if (context.canceled)
            isFire = false;
    }

    public void OnBoom(InputAction.CallbackContext context)
    {
        if (context.performed)
            Instantiate(bomb, Vector2.zero, Quaternion.identity);
    }

    public void OnLazer(InputAction.CallbackContext context)
    {
        if (context.performed)
            isLazer = true;
        else if (context.canceled)
            isLazer = false;
    }

    public void OnSlowTime(InputAction.CallbackContext context)
    {
        if (context.performed)
            isSlowTime = true;
        else if (context.canceled)
            isSlowTime = false;
    }

    public void OnShootWithJoyStick()
    {
        isFire = true;
        Shoot();
        isFire = false;
    }

    public void OnPressLazerWithJoyStick() => isLazer = true;

    public void OnReleaseLazerWithJoyStick() => isLazer = false;

    private void Shoot()
    {
        fireCooldown -= Time.deltaTime;

        if (isFire)
        {
            if (fireCooldown <= 0f)
            {
                GameObject bullet = ObjectPool.Instance.GetObject($"PlayerBullet{(power == 0 ? "" : power)}");
                bullet.transform.position = launcherPos.position;

                // 사운드
                SoundManager.Instance.PlayBulletSound();

                bullet.SetActive(true);

                fireCooldown = 1f / Mathf.Max(0.0001f, fireRate);
            }
        }
    }

    private void Lazer()
    {
        if (isLazer)
        {
            gValue += Time.deltaTime;
            gage.fillAmount = gValue;

            if (gValue >= 1f)
            {
                GameObject go = Instantiate(lazer, launcherPos.position, Quaternion.identity);
                go.transform.SetParent(launcherPos);

                gValue = 0f;
            }
        }
        else
        {
            gValue -= Time.deltaTime;

            if (gValue <= 0f)
                gValue = 0f;

            gage.fillAmount = gValue;
        }
    }

    private void SlowTime()
    {
        if (isSlowTime)
        {
            Time.timeScale = slowTimeScale;
            //Time.fixedDeltaTime = normalDeltaTime * slowTimeScale;
        }
        else
        {
            Time.timeScale = 1f;
            //Time.fixedDeltaTime = normalFixedDeltaTime;
        }
    }

    private void ClampPlayerPosition()
    {
        if (dir == Vector2.zero)
            return;

        Vector2 move = moveSpeed * Time.deltaTime / Time.timeScale * dir;

        transform.Translate(move);

        // 플레이어 스프라이크 크기 반영해서 화면 밖으로 나가지 않게 조정
        float clampedX = Mathf.Clamp(transform.position.x, viewportToWorldMin.x + playerHalfWidth, viewportToWorldMax.x - playerHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, viewportToWorldMin.y + playerHalfHeight, viewportToWorldMax.y - playerHalfHeight);

        // 위치 갱신
        transform.position = new Vector2(clampedX, clampedY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);

            if (power >= 3) return;

            Instantiate(powerUp, Vector2.zero, Quaternion.identity);

            power++;
        }
    }
}

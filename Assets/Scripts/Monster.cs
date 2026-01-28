using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;
    public float delay = 1f;

    public event Action OnDie;

    [SerializeField] private int hp;
    private int maxHP;
    [SerializeField] private GameObject item;

    private string bulletName;
    private float monsterHalfWidth;

    public int MaxHP => maxHP;
    public float MonsterHalfWidth => monsterHalfWidth;

    public int HP
    {
        get => hp;
        set
        {
            if (value <= 0)
                Die();

            hp = value;
        }
    }

    [SerializeField] private Transform[] launchers;

    private void Awake()
    {
        char lastChar = gameObject.name[^1];

        if(char.IsDigit(lastChar))
            bulletName = $"MonsterBullet{lastChar}";
        else
            bulletName = "MonsterBullet";

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        monsterHalfWidth = sr.bounds.size.x / 2f;

        maxHP = hp;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Shoot), delay, delay);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Shoot));
    }

    private void Shoot()
    {
        for (int i = 0; i < launchers.Length; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletName);

            bullet.transform.position = launchers[i].position;

            bullet.SetActive(true);
        }
    }
    private void DropItem()
    {
        // 아이템 생성
        Instantiate(item, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        // 이벤트 있으면 실행
        OnDie?.Invoke();

        // 사망 사운드
        SoundManager.Instance.PlayDieSound();

        gameObject.SetActive(false);

        DropItem();
    }
}

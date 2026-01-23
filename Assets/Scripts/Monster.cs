using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;
    public float delay = 1f;

    [SerializeField] private int hp;
    [SerializeField] private GameObject item;

    private string bulletName;

    public int HP
    {
        get => hp;
        set
        {
            if (value <= 0)
            {
                gameObject.SetActive(false);

                DropItem();
            }

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
        Instantiate(item, transform);

        item.SetActive(true);
    }
}

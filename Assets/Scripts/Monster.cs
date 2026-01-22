using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;
    public float delay = 1f;

    [SerializeField]
    private int hp;

    public int HP
    {
        get => hp;
        set => hp = value;
    }

    [SerializeField] private Transform[] launchers;

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
            GameObject bullet = ObjectPool.Instance.GetObject("MonsterBullet");

            bullet.transform.position = launchers[i].position;

            bullet.SetActive(true);
        }
    }
}

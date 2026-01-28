using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private bool isSpawning = true;
    [SerializeField] private GameObject textBossWarning;
    [SerializeField] private GameObject boss;

    private Vector2 viewportToWorldMin;
    private Vector2 viewportToWorldMax;

    private void Awake()
    {
        viewportToWorldMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        viewportToWorldMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        textBossWarning.SetActive(false);

    }

    private void Start()
    {
        StartCoroutine(RandomSpawn());
        Invoke(nameof(Stop), 10f);
    }

    IEnumerator RandomSpawn()
    {
        while(isSpawning)
        {
            yield return new WaitForSeconds(spawnTime);

            string monsterName = $"Monster{Random.Range(0, 2)}";

            if (monsterName[^1] == '0')
                monsterName = monsterName[..^1];

            GameObject monster = ObjectPool.Instance.GetObject(monsterName);
            float monsterHalfWidth = monster.GetComponent<Monster>().MonsterHalfWidth;
            float randomX = Random.Range(viewportToWorldMin.x + monsterHalfWidth, viewportToWorldMax.x - monsterHalfWidth);

            monster.transform.position = new Vector2(randomX, 6f);
            monster.GetComponent<Monster>().HP = monster.GetComponent<Monster>().MaxHP;
            monster.SetActive(true);
        }
    }

    void Stop()
    {
        isSpawning = false;
        StopCoroutine(RandomSpawn());

        // 보스
        textBossWarning.SetActive(true);

        // 카메라 흔들기
        StartCoroutine(Shake());

        Instantiate(boss, new Vector2(0, 3f), Quaternion.identity);
    }

    IEnumerator Shake()
    {
        int shakeCnt = 30;

        while(shakeCnt-- > 0)
        {
            CameraImpulse.Instance.ShowCameraShake();
            yield return new WaitForSeconds(0.1f);
        }
    }
}

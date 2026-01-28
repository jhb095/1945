using System.Collections;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private Transform[] launchers;

    private Vector2 dir = Vector2.right;

    private void Start()
    {
        Invoke(nameof(HideWarning), 2f);
        StartCoroutine(BossMissile());
        StartCoroutine(FireCircle());
    }

    private void Update()
    {
        if (transform.position.x >= 1f)
            dir = Vector2.left;
        else if (transform.position.x <= -1f)
            dir = Vector2.right;

        transform.Translate(speed * Time.deltaTime * dir);
    }

    private void HideWarning()
    {
        GameObject go = GameObject.Find("TextBossWarning");

        if(go != null)
            go.SetActive(false);
    }

    // 보스 미사일
    IEnumerator BossMissile()
    {
        while(true)
        {
            // 미사일 두개
            for(int i = 0; i < launchers.Length; i++)
            {
                GameObject missile = ObjectPool.Instance.GetObject("MonsterBullet");
                missile.transform.position = launchers[i].position;
                missile.SetActive(true);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    // 원방향으로 미사일 발사
    IEnumerator FireCircle()
    {
        int count = 30;                     // 발사체 생성 갯수
        float fireRate = 3f;                // 공격 주기
        float intervalAngle = 360 / count;  // 발사체 간격 각도
        float weightAngle = 0f;             // 가중 각도

        while(true)
        {
            for(int i = 0; i < count; i++)
            {
                GameObject missile = ObjectPool.Instance.GetObject("BossBullet");

                // 발사체 이동 방향(각도 및 벡터)
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                missile.transform.position = transform.position;
                missile.GetComponent<BossBullet>().Move(new Vector2(x, y));
                missile.SetActive(true);
            }

            weightAngle += 1f;

            yield return new WaitForSeconds(fireRate);
        }
    }
}

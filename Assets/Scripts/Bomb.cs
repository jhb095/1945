using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 1.5f;
    [SerializeField] private LayerMask hitLayers = Physics2D.DefaultRaycastLayers;

    private void Start()
    {
        // 폭발 반경 내 모든 콜라이더 가져오기
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, hitLayers);
        
        // 충돌한 모든 콜라이더 순회
        foreach(Collider2D hit in hits)
        {
            if (hit == null) continue;

            Monster monster = hit.GetComponent<Monster>();

            if (monster != null)
            {
                GameObject effect = ObjectPool.Instance.GetObject("BulletEffect");
                effect.transform.position = monster.gameObject.transform.position;
                effect.SetActive(true);

                monster.HP = 0;
            }
        }
    }

    // 폭발반경 시각화 Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

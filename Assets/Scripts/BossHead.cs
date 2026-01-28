using JetBrains.Annotations;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    public void CreateMissile(Vector2 vec)
    {
        GameObject missile = ObjectPool.Instance.GetObject("BossBullet");
        missile.transform.position = transform.position;
        missile.GetComponent<BossBullet>().Move(vec);
        missile.SetActive(true);
    }

    public void LeftDownLaunch()
    {
        CreateMissile(new Vector2(-1, -1));
    }

    public void RightDownLaunch()
    {
        CreateMissile(new Vector2(1, -1));
    }

    public void DownLaunch()
    {
        CreateMissile(new Vector2(0, -1));
    }
}

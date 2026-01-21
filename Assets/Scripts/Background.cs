using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.01f;

    private Material material;

    private float offset;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        offset %= 1f;

        material.mainTextureOffset = new Vector2(0, offset);
    }
}

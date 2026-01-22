using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.01f;

    private Material material;
    private float offsetY;
    
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        offsetY = (offsetY + Time.deltaTime * scrollSpeed) % 1;

        material.mainTextureOffset = new Vector2(0, offsetY);
    }
}

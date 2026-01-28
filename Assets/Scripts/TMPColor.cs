using System.Collections;
using TMPro;
using UnityEngine;

public class TMPColor : MonoBehaviour
{
    // 색상 전환에 걸리는 시간
    [SerializeField] private float lerpTime = 0.1f;

    // 텍스트 컴포넌트
    TextMeshProUGUI textBossWarning;

    private void Awake()
    {
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(ColorLerpLoop());
    }

    IEnumerator ColorLerpLoop()
    {
        while(true)
        {
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
        }
    }

    IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;
            textBossWarning.color = Color.Lerp(startColor, endColor, percent);
            yield return null;
        }
    }
}

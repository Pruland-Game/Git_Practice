using UnityEngine;
using TMPro;

public class DebugGameStateController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;

    private void Start()
    {
        if (debugText != null)
        {
            debugText.text = "Debug Mode";
            debugText.gameObject.SetActive(true);
        }
    }
}

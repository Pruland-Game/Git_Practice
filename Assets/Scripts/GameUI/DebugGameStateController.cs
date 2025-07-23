using UnityEngine;
using TMPro;

public class DebugGameStateController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private TextMeshProUGUI stateText;       // 状態表示用（新規）
    private bool isDebugMode = true;

    private enum GameState
    {
        None,
        Pause,
        GameOver,
        GameClear
    }

    private GameState currentState = GameState.None;

    private void Start()
    {
        if (isDebugMode == true)
        {
            if (debugText != null)
            {
                debugText.text = "Debug Mode";
                debugText.gameObject.SetActive(true);
            }
            if (stateText != null)
            {
                stateText.text = "State: " + currentState;
                stateText.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        // 常時：EscでPause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetState(GameState.Pause);
        }

        if (isDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SetState(GameState.GameOver);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                SetState(GameState.GameClear);
            }
        }
    }

    private void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("現在ステータス: " + currentState);

        if (isDebugMode && stateText != null)
        {
            stateText.text = "State: " + currentState;
        }
    }
}
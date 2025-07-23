using UnityEngine;
using TMPro;
public enum GameState
{
    None,
    Pause,
    GameOver,
    GameClear
}
public class DebugGameStateController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;　　　 // デバッグモード中表示
    [SerializeField] private TextMeshProUGUI stateText;       // デバッグモード中状態表示
    private bool isDebugMode = true;

    private GameState currentState = GameState.None;//現在のゲーム状態
    public GameState CurrentState => currentState;//他スクリプトからの状態参照プロパティ
    public static DebugGameStateController Instance { get; private set; }//シングルトン用
    private void Awake()//シングルトン
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // 複製防止
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (isDebugMode == true)//デバッグモードが有効な場合の表示類
        {
            if (debugText != null)
            {
                debugText.text = "Debug Mode";//フォントの関係でいったん英語。後で日本語化
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

        // 常時：Escでポーズ機能
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

        if (CurrentState == GameState.Pause)
        {
            if (debugText != null && debugText.gameObject.activeSelf)
            {
                debugText.gameObject.SetActive(false);
            }
            if (stateText != null && stateText.gameObject.activeSelf)
            {
                stateText.gameObject.SetActive(false);
            }
        }
        else
        {
            if (debugText != null && !debugText.gameObject.activeSelf)
            {
                debugText.gameObject.SetActive(true);
            }
            if (stateText != null && !stateText.gameObject.activeSelf)
            {
                stateText.gameObject.SetActive(true);
            }
        }


    }

    public void SetState(GameState newState)//状態管理用の共有メソッド
    {
        currentState = newState;
        Debug.Log("現在ステータス: " + CurrentState);

        if (isDebugMode && stateText != null)
        {
            stateText.text = "State: " + CurrentState;
        }
    }
}
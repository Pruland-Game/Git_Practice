using UnityEngine;
using UnityEngine.UI;
public enum GameState
{
    None,
    Pause,
    GameOver,
    StageClear
}
public class DebugGameStateController : MonoBehaviour
{
    [SerializeField] private Text DebugText;　　　 // デバッグモード中表示
    [SerializeField] private Text StateText;       // デバッグモード中状態表示
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
            if (DebugText != null)
            {
                DebugText.text = "Debug Mode";
                DebugText.gameObject.SetActive(true);
            }
            if (StateText != null)
            {
                StateText.text = "State: " + currentState;
                StateText.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (isDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SetState(GameState.GameOver);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                SetState(GameState.StageClear);
            }
        }

        if (CurrentState == GameState.Pause)
        {
            if (DebugText != null && DebugText.gameObject.activeSelf)
            {
                DebugText.gameObject.SetActive(false);
            }
            if (StateText != null && StateText.gameObject.activeSelf)
            {
                StateText.gameObject.SetActive(false);
            }
        }
        else
        {
            if (DebugText != null && !DebugText.gameObject.activeSelf)
            {
                DebugText.gameObject.SetActive(true);
            }
            if (StateText != null && !StateText.gameObject.activeSelf)
            {
                StateText.gameObject.SetActive(true);
            }
        }


    }

    public void SetState(GameState newState)//状態管理用の共有メソッド
    {
        currentState = newState;
        Debug.Log("現在ステータス: " + CurrentState);

        if (isDebugMode && StateText != null)
        {
            StateText.text = "State: " + CurrentState;
        }
    }
}
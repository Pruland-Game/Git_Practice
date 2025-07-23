using UnityEngine;
using UnityEngine.UI;
public enum GameState
{
    None,
    Pause,
    GameOver,
    GameClear
}
public class DebugGameStateController : MonoBehaviour
{
    [SerializeField] private Text DebugText;�@�@�@ // �f�o�b�O���[�h���\��
    [SerializeField] private Text StateText;       // �f�o�b�O���[�h����ԕ\��
    private bool isDebugMode = true;

    private GameState currentState = GameState.None;//���݂̃Q�[�����
    public GameState CurrentState => currentState;//���X�N���v�g����̏�ԎQ�ƃv���p�e�B
    public static DebugGameStateController Instance { get; private set; }//�V���O���g���p
    private void Awake()//�V���O���g��
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // �����h�~
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (isDebugMode == true)//�f�o�b�O���[�h���L���ȏꍇ�̕\����
        {
            if (DebugText != null)
            {
                DebugText.text = "Debug Mode";//�t�H���g�̊֌W�ł�������p��B��œ��{�ꉻ
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

        // �펞�FEsc�Ń|�[�Y�@�\
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

    public void SetState(GameState newState)//��ԊǗ��p�̋��L���\�b�h
    {
        currentState = newState;
        Debug.Log("���݃X�e�[�^�X: " + CurrentState);

        if (isDebugMode && StateText != null)
        {
            StateText.text = "State: " + CurrentState;
        }
    }
}
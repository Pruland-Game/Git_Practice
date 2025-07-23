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
    [SerializeField] private TextMeshProUGUI debugText;�@�@�@ // �f�o�b�O���[�h���\��
    [SerializeField] private TextMeshProUGUI stateText;       // �f�o�b�O���[�h����ԕ\��
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
            if (debugText != null)
            {
                debugText.text = "Debug Mode";//�t�H���g�̊֌W�ł�������p��B��œ��{�ꉻ
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

    public void SetState(GameState newState)//��ԊǗ��p�̋��L���\�b�h
    {
        currentState = newState;
        Debug.Log("���݃X�e�[�^�X: " + CurrentState);

        if (isDebugMode && stateText != null)
        {
            stateText.text = "State: " + CurrentState;
        }
    }
}
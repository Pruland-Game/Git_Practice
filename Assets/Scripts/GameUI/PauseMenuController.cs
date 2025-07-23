using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;//ポーズメニュー親子関係一式

    private void Start()
    {
        if (pausePanel != null)//最初は非アクティブに
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        //ポーズ中のみ開かせる
        if (DebugGameStateController.Instance.CurrentState == GameState.Pause)
        {
            if (pausePanel != null && !pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
            }
        }
        else//ポーズを解除したら非アクティブ化
        {
            if (pausePanel != null && pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
            }
        }
    }
}

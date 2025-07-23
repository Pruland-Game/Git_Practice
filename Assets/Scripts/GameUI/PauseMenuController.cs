using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;//�|�[�Y���j���[�e�q�֌W�ꎮ

    private void Start()
    {
        if (pausePanel != null)//�ŏ��͔�A�N�e�B�u��
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        //�|�[�Y���̂݊J������
        if (DebugGameStateController.Instance.CurrentState == GameState.Pause)
        {
            if (pausePanel != null && !pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
            }
        }
        else//�|�[�Y�������������A�N�e�B�u��
        {
            if (pausePanel != null && pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
            }
        }
    }
}

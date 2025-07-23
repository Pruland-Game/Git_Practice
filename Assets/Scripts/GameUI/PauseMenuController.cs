using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private List<Button> menuButtons = new List<Button>();
    private Dictionary<Text, Color> originalColors = new Dictionary<Text, Color>();

    private int currentSelection = 0;

    private void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);

            // pausePanel ���̂��ׂĂ� Button ���擾
            menuButtons.AddRange(pausePanel.GetComponentsInChildren<Button>(true));
            foreach (var button in menuButtons)
            {
                Text label = button.GetComponentInChildren<Text>();
                if (label != null && !originalColors.ContainsKey(label))
                {
                    originalColors[label] = label.color;
                }
            }
        }

        UpdateSelectionVisual();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (DebugGameStateController.Instance.CurrentState == GameState.Pause)
            {
                DebugGameStateController.Instance.SetState(GameState.None);
            }
            else
            {
                DebugGameStateController.Instance.SetState(GameState.Pause);
            }

            return; // ��Ԃ��؂�ւ��������͏������Ȃ�
        }
        if (DebugGameStateController.Instance.CurrentState == GameState.Pause)
        {
            if (pausePanel != null && !pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
                UpdateSelectionVisual(); // �\������ɑI����Ԃ��X�V
            }
            HandleMenuInput();
        }
        else
        {
            if (pausePanel != null && pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
            }
        }
    }

    private void HandleMenuInput()
    {
        if (menuButtons.Count == 0) return;

        bool updated = false;

        if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
        {
            currentSelection++;
            if (currentSelection >= menuButtons.Count)
                currentSelection = 0;
            updated = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
            currentSelection--;
            if (currentSelection < 0)
                currentSelection = menuButtons.Count - 1;
            updated = true;
        }

        if (updated)
        {
            UpdateSelectionVisual();
        }

        // Enter�L�[�Ō���
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            switch (currentSelection)
            {
                case 0:
                    //�Q�[����ʂɖ߂�
                    DebugGameStateController.Instance.SetState(GameState.None); //�|�[�Y����
                    break;
                case 1:
                    // �X�e�[�W�Z���N�g�ɖ߂�
                    Debug.Log("�X�e�[�W�Z���N�g�͌��󖢎����ł�");
                    break;
                case 2:
                    // �^�C�g��(startScene)�ɖ߂�
                    SceneManager.LoadScene("startScene");
                    //���ڂł��Ȃ������ꍇ�̏���
                    Debug.Log("Build Profiles����V�[�����X�g��startScene��o�^���Ȃ��Ɛ��ڂł��܂���");
                    break;
            }
    }

    private void UpdateSelectionVisual()
    {
         for (int i = 0; i < menuButtons.Count; i++)
    {
        Text label = menuButtons[i].GetComponentInChildren<Text>();
        if (label != null)
        {
            if (i == currentSelection)
            {
                label.color = Color.red;
            }
            else if (originalColors.ContainsKey(label))
            {
                label.color = originalColors[label]; // ���̐F�ɖ߂�
            }
        }
    }
    }
}

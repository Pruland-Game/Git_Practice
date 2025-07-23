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

            // pausePanel 内のすべての Button を取得
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

            return; // 状態が切り替わったら入力処理しない
        }
        if (DebugGameStateController.Instance.CurrentState == GameState.Pause)
        {
            if (pausePanel != null && !pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
                UpdateSelectionVisual(); // 表示直後に選択状態を更新
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

        // Enterキーで決定
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            switch (currentSelection)
            {
                case 0:
                    //ゲーム画面に戻る
                    DebugGameStateController.Instance.SetState(GameState.None); //ポーズ解除
                    break;
                case 1:
                    // ステージセレクトに戻る
                    Debug.Log("ステージセレクトは現状未実装です");
                    break;
                case 2:
                    // タイトル(startScene)に戻る
                    SceneManager.LoadScene("startScene");
                    //推移できなかった場合の処理
                    Debug.Log("Build ProfilesからシーンリストにstartSceneを登録しないと推移できません");
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
                label.color = originalColors[label]; // 元の色に戻す
            }
        }
    }
    }
}

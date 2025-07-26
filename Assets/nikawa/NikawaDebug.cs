using UnityEngine;

public class NikawaDebug : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("F1 key pressed. Debugging information can be added here.");
        }

        //クリックのテスト
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            //Debug.Log("Left mouse button clicked.");
        }
    }
    public void Aaa()
    {
        print("Aaa method called.");
    }
}

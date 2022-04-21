using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject ExitCheckPanel;

    // Start is called before the first frame update
    void Start()
    {
        ExitCheckPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonEndGame()
    {
        ExitCheckPanel.SetActive(true);
        transform.parent.SetAsLastSibling();
    }

    public void ExitGame()
    {
//#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
//#else
        Application.Quit();
//#endif
    }

    public void ButtonQuitPanel()
    {
        ExitCheckPanel.SetActive(false);
    }
}

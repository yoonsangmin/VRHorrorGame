using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ButtonToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void ToGameClearScene()
    {
        SceneManager.LoadScene("GameClearScene");
    }
}

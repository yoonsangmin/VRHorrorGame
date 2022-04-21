using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRole : MonoBehaviour
{
    public GameObject GameRolePanel;

    // Start is called before the first frame update
    void Start()
    {
        GameRolePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonGameRole()
    {
        GameRolePanel.SetActive(true);
        transform.parent.SetAsLastSibling();
    }

    public void ButtonQuitPanel()
    {
        GameRolePanel.SetActive(false);
    }
}

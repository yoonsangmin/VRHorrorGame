using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRole : MonoBehaviour
{
    public GameObject GameRolePanel;

    // 버튼
    public Button prevButton;
    public Button nextButton;

    // 페이지 인덱스
    private int pageIdx = 0;

    // 페이지 리스트
    public List<GameObject> pagesList;

    // Start is called before the first frame update
    void Start()
    {
        prevButton.interactable = true;
        nextButton.interactable = true;

        // 버튼 초기화
        // 초기 인덱스가 처음 페이지이면
        if (pageIdx <= 0)
        {
            pageIdx = 0;
            prevButton.interactable = false;
        }
        // 초기 인덱스가 마지막 페이지이면 또는 페이지가 없으면
        if (pageIdx >= pagesList.Count - 1 || pagesList.Count == 0)
        {
            pageIdx = pagesList.Count - 1;
            nextButton.interactable = false;
        }
        // 초기 인덱스 패널 켜줌
        for (int i = 0; i < pagesList.Count; i++)
        {
            pagesList[i].SetActive(false);
        }
        if(pageIdx != -1)
            pagesList[pageIdx].SetActive(true);

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

    public void ToPrevPage()
    {
        // 이전 페이지 확인
        if (--pageIdx <= 0)
        {
            // 이전 페이지가 없으면 버튼 사용 불가 처리
            prevButton.interactable = false;
            // 인덱스 더 안 내려가게 처리
            pageIdx = 0;
        }
        // 이전 페이지 버튼 켜줌
        nextButton.interactable = true;

        // 업데이트한 페이지를 켜줌
        pagesList[pageIdx].SetActive(true);
        // 이전 페이지를 꺼줌
        pagesList[pageIdx + 1].SetActive(false);
    }

    public void ToNextPage()
    {
        // 다음 페이지 확인
        if (++pageIdx >= pagesList.Count - 1)
        {
            // 다음 페이지가 없으면 버튼 사용 불가 처리
            nextButton.interactable = false;
            // 인덱스 더 안 올라가게 처리
            pageIdx = pagesList.Count - 1;
        }
        // 이전 페이지 버튼 켜줌
        prevButton.interactable = true;

        // 업데이트한 페이지를 켜줌
        pagesList[pageIdx].SetActive(true);
        // 이전 페이지를 꺼줌
        pagesList[pageIdx - 1].SetActive(false);
    }
}

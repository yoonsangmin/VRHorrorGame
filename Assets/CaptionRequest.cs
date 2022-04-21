using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CaptionRequest : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onEnter;
    // 자막 시작시 부를 이벤트

    bool isEnterPlayed = false;

    [SerializeField]
    private UnityEvent onEnd;
    // 자막 끝나면 부를 이벤트

    public CaptionRequest mustRequiredCaption;  //선행 자막이 어떤것인지 인스펙터 창에서 설정해줌

    public bool isCaptionRunning = false;   // 자막 실행중이라고 알려줌 - 나중에 시퀀스 업데이트 할 때 필요할 거 같음
    public bool isCaptionDone = false;  //자막이 완료 되었는지 검사함 - 다음 자막이 이것을 보고 이전 자막이 끝났는지 체크할 수 있음

    //나중에 구조체로 바꼈을 때 구조체 리스트를 사용할 거임, 그 때 몇번째 순서인지 저장해야 함
    public int sequence = 0;

    public bool actionReady = false;    //캡션 종료에 필요한 액션이 있을 때 액션 받을 준비가 되면 알려줌

    //public bool isSetFalseWhenDone = false;


    PauseAndCallMenu pauseAndCallMenu = null;


    [Serializable]
    public struct CaptionStruct
    {
        public string captionText;
        public int captionPersistTime;
        public bool isUntilAction;  //액션이 필요한지 알려줌
    }

    public CaptionStruct[] captions;

    // Start is called before the first frame update
    void Start()
    {
        pauseAndCallMenu = GameObject.FindGameObjectWithTag("MenuPanel").GetComponent<PauseAndCallMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCaptionRunning && CaptionManager.Instance.EnterCaptionSequence() && !pauseAndCallMenu.isPopUpOpened)
        {
            if(!isEnterPlayed)
            {
                onEnter?.Invoke();
            }

            //Debug.Log("자막실행중");
            RequestCaptionSequence();

            //자막이 끝났으면 끝나다고 알려줘야 함 - 나중에 수정
            if (sequence == captions.Length)
            {
                isCaptionRunning = false;
                isCaptionDone = true;
            }
        }

        if(isCaptionDone && CaptionManager.Instance.EnterCaptionSequence())
        {
            onEnd?.Invoke();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject hit = other.gameObject;

        if (hit.tag == "Player")
        {
            if (mustRequiredCaption == null || mustRequiredCaption.isCaptionDone)       //필요한 선행자막이 없거나, 선행자막이 실행 되었는지 체크
            {
                if (CaptionManager.Instance.EnterCaptionSequence() && !isCaptionDone)    //자막을 보내기 전에 자막이 실행 중인지 체크, 자막이 이미 실행 됐는지 체크
                {
                    isCaptionRunning = true;    //현재 자막이 실행중임을 알림
                }
            }

            //if (CaptionManager.Instance.EnterCaptionSequence() && !isCaptionDone)    //자막을 보내기 전에 자막이 실행 중인지 체크, 자막이 이미 실행 됐는지 체크
            //{
            //    isCaptionRunning = true;    //현재 자막이 실행중임을 알림
            //}
        }
    }

    void RequestCaptionSequence()       //시퀀스에 맞게 자막을 실행하게 함수를 수정해야 함
    {
        CaptionManager.Instance.updateCaption(captions[sequence].captionText, captions[sequence].captionPersistTime, captions[sequence].isUntilAction);
        if (captions[sequence].isUntilAction)
        {
            actionReady = true;   //액션을 받을 준비가 되면 알려줌
        }
        sequence++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptionManager : MonoBehaviour
{
    private static CaptionManager instance;
    public static CaptionManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<CaptionManager>();

                if (obj != null)

                {

                    instance = obj;

                }

                else

                {

                    var newSingleton = new GameObject("CaptionManager").AddComponent<CaptionManager>();

                    instance = newSingleton;

                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public TextMesh caption;    //자막을 퍼블릭으로 만들어서 인스펙터 창에서 선택할 수 있음

    public bool isCaptionWorking = false;   //if caption is working = true, if caption is no working = false 자막이 사용중인지 아닌지

    public bool isCaptionPersistUntilAction = false; //if true, persist until some function call 액션이 완료되기 전까지 자막을 계속 실행함

    public string captionText = "확인용 텍스트";  //표시되는 텍스트 초기값은 ""
    public float persistTime = 5;                 //자막 지속시간 초기값은 0


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteCaption();
    }

    void ExecuteCaption()
    {
        if (isCaptionPersistUntilAction)     //행동이 완료되기 전까지 자막 계속 실행
        {
            caption.text = captionText;
        }

        else if (persistTime >= 0.0f)         //시간이 완료되기 전까지 자막을 표시
        {
            caption.text = captionText;

            persistTime -= Time.deltaTime;
        }

        else if (persistTime < 0.0f)        //시간이 완료되면 자막을 빈칸으로 만듦, 다음 자막이 들어올 수 있다고 알림
        {
            caption.text = "";

            isCaptionWorking = false;
        }
    }

    public bool EnterCaptionSequence()      //자막 코드에서 자막에 들어오기 전에 들어올 수 있는지 확인하는 코드
    {
        if (isCaptionWorking == false)       //캡션이 실행중이 아니면
        {
            isCaptionWorking = true;        //실행중이라고 표현해줌
            return true;                    //캡션에 들어올 수 있음
        }
        return false;                       //캡션이 실행 중이기 때문에 캡션에 들어올 수 없음
    }

    public void updateCaption(string updateText, float updateTime, bool updateIsPersistUntilAction) //자막을 받아와서 업데이트 함 (텍스트, 지속시간, 자막이 액션기반인지 아니면 시간기반인지)
    {
        persistTime = updateTime;
        captionText = updateText;
        isCaptionPersistUntilAction = updateIsPersistUntilAction;
    }
}

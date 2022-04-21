using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
//C# 언어에서 쿼리 기능을 통합하는 방식을 제공하는 라이브러리. 
//리스트 이용시 잘쓰면 코드가 엄청 줄어든다! C#에서 쓰일때와는 약간 다름. 최소값 구할때만 썼음;

public class HitData
{
    public GameObject hitObj;
    public List<Vector3> hitPoint = new List<Vector3>();
    public List<float> hitDistance = new List<float>();

    //이상하게 직접 obj를 넣어주면 리스트는 생성되는데 처음 들어간 obj만 생성된다.
    //왜그런지 이틀내내 고민하다 함수하나 만들어서 넣어주니 된다.. 
    //구조체처럼 값 기반도 아닌데 왜 수정이 안되는거지 나중에 찾아봐야지

    public void AddObj(GameObject obj)
    {
        hitObj = obj;
    }

    //포지션 정보를 담아주는 함수
    public void PositionData(Vector3 pointData, float floatData)
    {
        hitPoint.Add(pointData);
        hitDistance.Add(floatData);
    }

    //거리 최소값을 받아오고 해당 인덱스를 받아온다. 
    //거리에 따른 벡터좌표가 동일한 인덱스를 가지기 때문
    public int FindDistance()
    {
        float minDis = hitDistance.Min();
        int index = hitDistance.IndexOf(minDis);
        return index;
    }
}

public class Skill_Echolocation : MonoBehaviour
{
    float[] colliderDistance;
    public GameObject EchoSoundObj;
    public GameObject PlayerObj;        //나중에 손으로 위치 바꾸기
    public GameObject raydirObj;
    //public XRNode inputSource;

    //float buttonPressedAxisValue;
    bool isButtonPressing = false;
    bool isButtonPressed = false;

    private Vector3 PlayerLocalToWorldPoint;
    public float castDistance = 4f;
    public float hfReferenceValue = 2500f;
    public float reverbRoomValue = 0f;
    public float dryLevelValue = 3000f;
    float roundedDistance = 0f;
    float delayTime = 0f;
    float reducedValue = 0f;

    private float cooltime = 0.2f;
    private float timer = 0.2f;

    List<HitData> castedDatas = new List<HitData>();
    List<HitData> checkedDatas = new List<HitData>();

    public void button_click()
    {
        if (timer >= cooltime)
        {
            PlayerLocalToWorldPoint = PlayerObj.transform.TransformPoint(PlayerObj.transform.localPosition);
            GameObject InstnatiateEchoSoundObj = Instantiate(EchoSoundObj, PlayerLocalToWorldPoint, Quaternion.identity) as GameObject;
            AudioSource Echosound = InstnatiateEchoSoundObj.GetComponent<AudioSource>();
            Echosound.Play();
            Destroy(InstnatiateEchoSoundObj, 1f);

            StartCoroutine(CastEcholocationSphere());

            timer = 0.0f;
        }
    }

    //캐릭터가 소리를 내기 때문에 캐릭터 자리에 사운드 생성 및 재생. 이후 반사 연산 코루틴 실행.  
    void Update()
    {
        //쿨 타임

        timer += Time.deltaTime;

        //if (timer >= cooltime)
        //{
        //    //버튼 클릭 구현
        //    InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        //    device.TryGetFeatureValue(CommonUsages.trigger, out buttonPressedAxisValue);

        //    if (buttonPressedAxisValue >= 0.5f)
        //        isButtonPressing = true;
        //    else
        //        isButtonPressing = false;

        //    timer = 0.0f;
        //}

        //PlayerLocalToWorldPoint = PlayerObj.transform.TransformPoint(PlayerObj.transform.localPosition);
        //if (isButtonPressing && !isButtonPressed)
        //{
        //    GameObject InstnatiateEchoSoundObj = Instantiate(EchoSoundObj, PlayerLocalToWorldPoint, Quaternion.identity) as GameObject;
        //    AudioSource Echosound = InstnatiateEchoSoundObj.GetComponent<AudioSource>();
        //    Echosound.Play();
        //    Destroy(InstnatiateEchoSoundObj, 1f);

        //    StartCoroutine(CastEcholocationSphere());
        //}

        //isButtonPressed = isButtonPressing;


    }
    //소리를 재현해줄 레이캐스트를 쏘는 함수. 미쳐버린 삼중포문이 특징
    //수정하려 했으나 반복 횟수가 높지 않기에 그냥 그대로 두었다.
    IEnumerator CastEcholocationSphere()
    {
        //시작 각도 -60도, +60도까지 120도 각도를 스캔
        float XRotate = -48;

        for (int x = 0; x < 8; x++)
        {
            float YRotate = -48;
            XRotate += 12;
            for (int y = 0; y < 8; y++)
            {
                YRotate += 12;

                raydirObj.transform.localEulerAngles = new Vector3(XRotate, YRotate, raydirObj.transform.localEulerAngles.z);
                int layerMask = (15 << LayerMask.NameToLayer("Obstacle"));
                RaycastHit hit;
                if (Physics.Raycast(raydirObj.transform.position, raydirObj.transform.forward, out hit, castDistance))
                {
                    if (hit.transform.gameObject.CompareTag("Obstacle"))
                    {
                        HitData data = new HitData();

                        if (castedDatas.Count == 0)
                        {
                            data.AddObj(hit.transform.gameObject);
                            data.PositionData(hit.point, hit.distance);
                            castedDatas.Add(data);
                        }
                        else
                        {
                            bool existCheck = false;

                            for (int i = 0; i < castedDatas.Count; i++)
                            {
                                if (castedDatas[i].hitObj == hit.transform.gameObject)
                                    existCheck = true;

                                if (!existCheck)
                                {
                                    data.AddObj(hit.transform.gameObject);
                                    castedDatas.Add(data);
                                }
                                data.PositionData(hit.point, hit.distance);
                            }
                        }
                    }
                }
            }
            OverlapCheck();
            yield return new WaitForSeconds(0.001f);
        }
    }
    //한 오브젝트에 부딪힌 충돌 중 가장 가까운 거리와 해당 벡터 도출
    void OverlapCheck()
    {
        for (int i = 0; i < castedDatas.Count; i++)
        {
            int index = castedDatas[i].FindDistance();
            Debug.Log(castedDatas[i].hitObj.name + "개수:" + castedDatas[i].hitDistance.Count);

            Vector3 checkedPoint = castedDatas[i].hitPoint[index];
            float checkedDistance = castedDatas[i].hitDistance[index];
            genarateAudio(checkedPoint, checkedDistance);
        }
        checkedDatas.Clear();
        castedDatas.Clear();
    }
    //사운드 생성. 거리에 따라 오디오소스와 리버브필터 수치를 조정. 실제와 비슷하면서 구분되도록 설정
    void genarateAudio(Vector3 checkedHitPoint, float checkedDistance)
    {
        roundedDistance = Mathf.Round(checkedDistance * 100f) * 0.01f;
        delayTime = 0.0603f * roundedDistance - 0.001206f;

        if (delayTime < 0)
            delayTime = 0;

        GameObject InstantiateEchoSoundObj = Instantiate(EchoSoundObj, checkedHitPoint, Quaternion.identity) as GameObject;
        AudioSource Echosound = InstantiateEchoSoundObj.GetComponent<AudioSource>();
        AudioReverbFilter reverbFilter = InstantiateEchoSoundObj.GetComponent<AudioReverbFilter>();
        reverbFilter.hfReference = hfReferenceValue;
        reverbFilter.room = reverbRoomValue;
        reverbFilter.dryLevel -= dryLevelValue;
        reducedValue = castDistance - checkedDistance;
        Echosound.spread = 360;
        Echosound.volume = Echosound.volume * reducedValue * 0.25f;
        Echosound.maxDistance = castDistance;
        Echosound.minDistance = 0.1f;
        Echosound.PlayDelayed(delayTime);
        Destroy(InstantiateEchoSoundObj, 1f);
    }
}

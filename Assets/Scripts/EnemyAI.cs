using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent onPlayerAttacked;

    //시야 관련 변수

    public float m_angle = 0f;
    public float detect_distance = 0f;
    public float sight_distance = 0f;
    [SerializeField] LayerMask m_layerMask = 0;
    public LayerMask exc_layerMask;

    public GameObject player;

    //발소리 듣는 변수

    public GameObject VRRig;

    public float listenDistance = 5f;
    public float listenSpeedThreshold = 2.5f;

    //순찰 관련 변수

    public NavMeshAgent m_enemy = null;

    [SerializeField] Transform[] m_tfWayPotions = null;

    [SerializeField] Transform[] m_tfWayPotions_EXT1 = null;
    [SerializeField] Transform[] m_tfWayPotions_EXT2 = null;
    [SerializeField] Transform[] m_tfWayPotions_EXT3 = null;
    int m_count = 0;

    public Transform m_target = null;

    public float normalSpeed = 1.5f;
    public float followSpeed = 3.0f;

    public bool playerBehindObstacle;

    public float attackDistance;

    public float waitTime = 2f;

    public float atkCooltime = 3f;
    public float atkDelay;

    public bool needLookAt = false;

    public GameObject attackLight;


    public AudioClip growling;
    public AudioClip attackclip;
    public AudioClip[] bark;

    AudioSource audioSource;
    AudioSource attackSource;

    float soundtimer;

    // 캔 관련
    public CanSoundManager canSoundManager;

    //시야 관련 함수
    //플레이어 발견했고 발견했으면 따라가야함
    public void Sight()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, detect_distance, m_layerMask);
        
        if (t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform;

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward);
            if(t_angle < m_angle * 0.5f)
            {
                if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, sight_distance, exc_layerMask))
                {
                    if (t_hit.transform.tag == "Player" || t_hit.transform.tag == "Right Hand")
                    {
                        if(player.GetComponent<PlayerLittedState>().isPlayerLitted)
                        {
                            //Debug.Log("빛에 있을 때 보는 중");
                            SetTarget(t_hit.transform);
                            playerBehindObstacle = false;
                        }
                        
                    }
                    else   //플레이어가 장애물 뒤에 숨으면 그만 따라감, 빛을 받았을 때 발견했으면 어둠속에서도 계속 따라감
                    {
                        //RemoveTarget();
                        playerBehindObstacle = true;
                    }

                    //Debug.Log(t_hit.transform.tag);
                }
                else
                {
                    //RemoveTarget();
                }
            }
        }
    }

    public void Listen()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < listenDistance)
        {
            if(VRRig.GetComponent<WalkSound>().speed > listenSpeedThreshold)
            {
                SetTarget(player.transform);
            }
        }

        // 캔 소리 듣기
        if(m_target == null)
        {
            if(canSoundManager.isCanDropped)
            {
                if (Vector3.Distance(transform.position, canSoundManager.canDroppedTransform.position) < listenDistance)
                {
                    SetTarget(canSoundManager.canDroppedTransform);
                }
            }
        }
    }

    public void Sight_Obstacle()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, detect_distance, m_layerMask);

        if (t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform;

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward);
            if (t_angle < m_angle * 0.5f)
            {
                if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, sight_distance, exc_layerMask))
                {
                    if (t_hit.transform.tag == "Player" || t_hit.transform.tag == "Right Hand")
                    {
                        if (player.GetComponent<PlayerLittedState>().isPlayerLitted)
                        {
                            //Debug.Log("빛에 있을 때 보는 중");
                            //SetTarget(t_hit.transform);
                            playerBehindObstacle = false;
                        }

                    }
                    else   //플레이어가 장애물 뒤에 숨으면 그만 따라감, 빛을 받았을 때 발견했으면 어둠속에서도 계속 따라감
                    {
                        //RemoveTarget();
                        playerBehindObstacle = true;
                    }

                    //Debug.Log(t_hit.transform.tag);
                }
                else
                {
                    //RemoveTarget();
                }
            }
        }
    }


    //순찰 관련 함수

    public void SetTarget(Transform p_target)
    {
        CancelInvoke("MoveToNextWayPoint");

        m_target = p_target;

        m_enemy.ResetPath();
    }

    public void RemoveTarget()
    {
        m_target = null;

        if(this.gameObject.activeSelf)
        {
            m_enemy.ResetPath();

            m_enemy.speed = normalSpeed;
        }
    }

    public void MoveToNextWayPoint()
    {
        if(m_target == null)
        {
            if (m_enemy.velocity == Vector3.zero && this.gameObject.activeSelf == true)
            {
                m_enemy.SetDestination(m_tfWayPotions[m_count++].position);

                if (m_count >= m_tfWayPotions.Length)
                {
                    m_count = 0;
                }
            }
        }
    }

    public void FindNearestWayPoint()
    {
        if(m_tfWayPotions != null)
        {
            int idx = 0;
            float dis = Vector3.Distance(m_tfWayPotions[0].position, transform.position);
            for (int i = 1; i < m_tfWayPotions.Length; i++)
            {
                float temp = Vector3.Distance(m_tfWayPotions[i].position, transform.position);
                if (temp < dis)
                {
                    dis = temp;
                    idx = i;
                }
            }
            m_count = idx;
        }
    }

    public void StartPatrol()
    {
        InvokeRepeating("MoveToNextWayPoint", 0f, waitTime);
    }

    public void EndPatrol()
    {
        CancelInvoke("MoveToNextWayPoint");
    }

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.speed = normalSpeed;

        player = GameObject.FindGameObjectWithTag("Player");

        VRRig = GameObject.FindGameObjectWithTag("VRRig");

        canSoundManager = GameObject.FindGameObjectWithTag("CanSoundManager").GetComponent<CanSoundManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        attackSource = gameObject.AddComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f;
        audioSource.minDistance = 0.1f;
        audioSource.maxDistance = 10.0f;

        attackSource.spatialBlend = 1.0f;
        attackSource.minDistance = 0.1f;
        attackSource.maxDistance = 10.0f;

        FindNearestWayPoint();
        StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (atkDelay >= 0)
        {
            atkDelay -= Time.deltaTime;
        }

        if(needLookAt)
        {
            Vector3 dir = player.transform.position - transform.position;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);

            if(atkDelay > 0)
            {
                needLookAt = false;
            }
        }

        if (soundtimer <= 0)
        {
            soundtimer = Random.Range(7, 10);

            if(!audioSource.isPlaying)
                barksound();
        }
            
        soundtimer -= Time.deltaTime;
    }



    public void growlingsound()
    {
        audioSource.clip = growling;
        audioSource.Play();
    }

    public void attacksound()
    {
        attackSource.clip = attackclip;
        attackSource.Play();

        barksound();
    }

    public void barksound()
    {
        int idx = Random.Range(0, bark.Length);

        audioSource.clip = bark[idx];
        audioSource.Play();
    }

    public void ExtendWayPoint1()
    {
        m_tfWayPotions = m_tfWayPotions_EXT1;
        FindNearestWayPoint();
    }

    public void ExtendWayPoint2()
    {
        m_tfWayPotions = m_tfWayPotions_EXT2;
        FindNearestWayPoint();
    }

    public void ExtendWayPoint3()
    {
        m_tfWayPotions = m_tfWayPotions_EXT3;
        FindNearestWayPoint();
    }
}
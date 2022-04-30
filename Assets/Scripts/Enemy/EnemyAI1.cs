//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyAI : MonoBehaviour
//{
//    //시야 관련 변수

//    [SerializeField] float m_angle = 0f;
//    [SerializeField] float m_distance = 0f;
//    [SerializeField] LayerMask m_layerMask = 0;
//    public LayerMask exc_layerMask;

//    public GameObject player;

//    //순찰 관련 변수

//    NavMeshAgent m_enemy = null;

//    [SerializeField] Transform[] m_tfWayPotions = null;
//    int m_count = 0;

//    public Transform m_target = null;
    
//    //시야 관련 함수

//    void Sight()
//    {
//        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layerMask);
        
//        if (t_cols.Length > 0)
//        {
//            Transform t_tfPlayer = t_cols[0].transform;

//            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
//            float t_angle = Vector3.Angle(t_direction, transform.forward);
//            if(t_angle < m_angle * 0.5f)
//            {
//                if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, m_distance, exc_layerMask))
//                {
//                    if (t_hit.transform.tag == "Player" || t_hit.transform.tag == "Right Hand")
//                    {
//                        if(player.GetComponent<PlayerLittedState>().isPlayerLitted)
//                        {
//                            Debug.Log("빛에 있을 때 보는 중");
//                            SetTarget(t_hit.transform);
//                        }
                        
//                    }
//                    else   //플레이어가 장애물 뒤에 숨으면 그만 따라감, 빛을 받았을 때 발견했으면 어둠속에서도 계속 따라감
//                    {
//                        RemoveTarget();
//                    }

//                    Debug.Log(t_hit.transform.tag);
//                }
//                else
//                {
//                    RemoveTarget();
//                }
//            }
//        }
//    }

//    //순찰 관련 함수

//    public void SetTarget(Transform p_target)
//    {
//        CancelInvoke();
//        m_target = p_target;
//    }

//    public void RemoveTarget()
//    {
//        m_target = null;
//        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
//    }

//    void MoveToNextWayPoint()
//    {
//        if(m_target == null)
//        {
//            if (m_enemy.velocity == Vector3.zero)
//            {
//                m_enemy.SetDestination(m_tfWayPotions[m_count++].position);

//                if (m_count >= m_tfWayPotions.Length)
//                {
//                    m_count = 0;
//                }
//            }
//        }
//    }    
    
//    // Start is called before the first frame update
//    void Start()
//    {
//        m_enemy = GetComponent<NavMeshAgent>();
//        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(m_target == null)
//        {
//            Sight();
//        }

//        if (m_target != null)
//        {
//            m_enemy.SetDestination(m_target.position);
//        }
//    }


//}

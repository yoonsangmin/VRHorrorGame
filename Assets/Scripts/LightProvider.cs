using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProvider : MonoBehaviour
{
    public GameObject player;
    public LayerMask m_layerMask;
    public LayerMask exc_layerMask;
    public float angle;
    public float distance;

    //앵글 빼도 될 거 같음 - 넣어야지

    void Lighting()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, distance, m_layerMask);

        if (t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform;

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward);
            if (t_angle < angle * 0.5f)
            {
                if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, distance, exc_layerMask))
                {
                    if (t_hit.transform.tag == "Player" || t_hit.transform.tag == "Right Hand")
                    {
                        player.GetComponent<PlayerLittedState>().isPlayerLitted = true;
                    }
                    else
                    {
                        //player.GetComponent<PlayerLittedState>().isPlayerLitted = false;
                        //Debug.Log("레이는 쐈지만 빛 받지 않는 중" + t_direction + t_hit.transform.name);
                    }

                    //Debug.Log(t_hit.transform.name);
                }
                else
                {
                    //player.GetComponent<PlayerLittedState>().isPlayerLitted = false;
                    //Debug.Log("빛 받지않는 중");
                }
            }
            else
            {
                //player.GetComponent<PlayerLittedState>().isPlayerLitted = false;
                //Debug.Log("빛 받지않는 중");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Lighting();
    }
}

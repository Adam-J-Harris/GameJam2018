using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform visionRendererFlag;
    public Transform lineRendererFlag;
    public Transform posRef;

    public LineRenderer LR;
    public AudioSource AS;

    private Animator Anim;

    private Transform target;
    private float viewRange;
    private float searchingTurnSpeed;
    private float viewAngle;

    private Vector3 length;

    private bool foundPlayer;
    private bool playerDead;

    void Awake()
    {
        viewRange = 0f;
        viewAngle = 0f;
        searchingTurnSpeed = 0f;

        foundPlayer = false;

        length = new Vector3(0f, 0f, 10f);
    }

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();

        if (foundPlayer && !playerDead)
            Fire();
    }

    void DrawLine()
    {
        RaycastHit hit;

        if (Physics.Raycast(lineRendererFlag.transform.position, lineRendererFlag.transform.forward, out hit) && (hit.collider.CompareTag("Block") || hit.collider.CompareTag("Player")))
        {

            if (hit.collider.CompareTag("Player"))
            {
                target = hit.transform;
                foundPlayer = true;
            }
            else
            {
                target = null;    
                foundPlayer = false;
            }
            
            LR.SetPosition(1, new Vector3(0, 0, hit.distance * 100));
        }
        else
        {
            LR.SetPosition(1, new Vector3(0, 0, 10000));
        }
    }

    void Fire()
    {
        AS.Play();

        RaycastHit hit;

        if (Physics.Raycast(lineRendererFlag.transform.position, lineRendererFlag.transform.forward, out hit) && hit.collider.CompareTag("Player") && !GameMaster.instance.GameOver)
        {
            hit.collider.GetComponent<PlayerManager>().KillPlayer();
        }

    }
}

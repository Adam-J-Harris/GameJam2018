using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public PlayerController playerController;
    public BatteryManager batteryManager;
    public Rigidbody rb;
    public Transform GFX;
    public LayerMask moveCheckMask;
    public float rotationSpeed = 10.0f;
    public float moveSpeed = 10.0f;

    private bool startNewAction = false;
    Vector3 moveToPosition = new Vector3(0, 0, 0);


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GFX = transform.GetChild(0).GetComponent<Transform>();

        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
    }


    public IEnumerator PerformNextAction()
    {
        if (GameMaster.instance.GoalReached)
        {
            yield return 0;
        }

        for (int i = 0; i < playerController.GetActionListSize(); i++)
        {
            startNewAction = false;
            yield return new WaitForSeconds(0.2f);

            Action action = playerController.GetActionFromList(i);
            switch (action)
            {
                case Action.MOVE_UP:
                    moveToPosition = transform.position + Vector3.forward;
                    break;

                case Action.MOVE_DOWN:
                    moveToPosition = transform.position + Vector3.back;
                    break;

                case Action.MOVE_LEFT:
                    moveToPosition = transform.position + Vector3.left;
                    break;

                case Action.MOVE_RIGHT:
                    moveToPosition = transform.position + Vector3.right;
                    break;
            }

            moveToPosition = new Vector3(Mathf.Round(moveToPosition.x), moveToPosition.y, Mathf.Round(moveToPosition.z));

            if (CheckMove(action))
            {
                StartCoroutine(Move());
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                startNewAction = true;
            }

            while (!startNewAction)
            {
                yield return null;
            }
        }

        if (!GameMaster.instance.GoalReached)
        {
            GameMaster.instance.EndGame("Buddy Could Not Reach Boo!");
        }
    }

    bool CheckMove(Action action)
    {
        if (GameMaster.instance.GameOver || GameMaster.instance.GoalReached)
        {
            return false;
        }

        Vector3 rayEndPoint = new Vector3(0,0,0);

        switch (action)
        {
            case Action.MOVE_UP:
                rayEndPoint = new Vector3(Vector3.forward.x, GFX.position.y + 0.2f, Vector3.forward.z);
                break;

            case Action.MOVE_DOWN:
                rayEndPoint = new Vector3(Vector3.back.x, GFX.position.y + 0.2f, Vector3.back.z);
                break;

            case Action.MOVE_LEFT:
                rayEndPoint = new Vector3(Vector3.left.x, GFX.position.y + 0.2f, Vector3.left.z);
                break;

            case Action.MOVE_RIGHT:
                rayEndPoint = new Vector3(Vector3.right.x, GFX.position.y + 0.2f, Vector3.right.z);
                break;
        }

        Debug.DrawRay(GFX.position, rayEndPoint, Color.green, 5.0f);

        RaycastHit hitInfo;

        if (Physics.Raycast(GFX.position, rayEndPoint, out hitInfo, 1.0f, moveCheckMask))
        {
            Debug.Log("Hit");
            GetComponent<Animator>().SetTrigger("pathBlocked");
            return false;
        }

        Debug.Log("No Hit");
        return true;
    }


    IEnumerator Move()
    {
        batteryManager.ReduceBattery();

        while (Vector3.Distance(transform.position, moveToPosition) > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveToPosition - transform.position, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, moveToPosition, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        startNewAction = true;
        yield return 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(moveToPosition, 0.2f);
    }


}

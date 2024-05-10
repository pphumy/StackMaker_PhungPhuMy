using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] public LayerMask layer;
    [SerializeField] Transform player;

    [SerializeField] GameObject brickPrefab, playerBrick, playerModel;

    List<GameObject> bricks = new List<GameObject>();
    [SerializeField] Transform StartPoint;

    [SerializeField] Collider playerCollider;

    private RaycastHit hit;
    private bool isMoving = false;
    private Vector3 dir = Vector3.zero;
    private float brickHeight = 0.3f;

    private Vector3 nextPoint;
    private Vector3 offset = new Vector3(0, 3.3f, 0);
    private string currentAnimName;

    private void Awake()
    {
        OnInit();
        nextPoint = transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            playerCollider.enabled = false;
        }
        else
        {
            playerCollider.enabled = true;
        }
        //CheckCollider();
        getMovePostition();
        MovePlayer();
    }

    private void getMovePostition()
    {
        if (!isMoving)
        {
            switch (InputManager.Instance.direction)
            {
                case EDirection.Forward:

                    dir = Vector3.forward;
                    break;
                case EDirection.Backward:

                    dir = Vector3.back;
                    break;
                case EDirection.Right:

                    dir = Vector3.right;
                    break;
                case EDirection.Left:

                    dir = Vector3.left;
                    break;
                case EDirection.None:
                    dir = Vector3.zero;
                    break;
            }

            Debug.DrawRay(transform.position, dir, Color.red, Mathf.Infinity);
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, layer))
            {

                Vector3 tempPos = hit.transform.position;
                tempPos.y = transform.position.y;
                nextPoint = tempPos - dir * 1f;
            }
        }
        else
        {
            return;
        }
    }

    private void MovePlayer()
    {
        if (Vector3.Distance(transform.position, nextPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void AddBrick()
    {
        Vector3 newBrickPosition = playerBrick.transform.position;
        GameObject brick = Instantiate(brickPrefab, newBrickPosition, Quaternion.Euler(-90, 0, 0), playerBrick.transform);
        //brick.transform.SetParent(playerBrick.transform);
        //Set vi tri de cac brick xep chong len nhau
        brick.transform.localPosition = new Vector3(0, bricks.Count * 0.3f, 0);
        //add brick vua tao ra
        bricks.Add(brick);
        playerModel.transform.localPosition += Vector3.up * 0.3f;
    }

    private void RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            //lay vien gach tren cung
            GameObject brick = bricks[bricks.Count - 1];
            //xoa vien gach ra khoi list
            bricks.Remove(brick);
            //destroy vien gach di
            Destroy(brick);
            //set position cua player 
            playerModel.transform.localPosition -= Vector3.up * brickHeight;
        }
    }

    private void ClearBrick()
    {
        foreach (var brick in bricks)
        {
            Destroy(brick);
        }
        //xoa het gach trong list
        bricks.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Brick"))
        {
            
            Debug.Log("Brick");
            AddBrick();
            //ChangeAnim("HitStack");
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("UnBrick"))
        {
            Bridge bridge = other.gameObject.GetComponent<Bridge>();
            if (!bridge.havePassed)
            {
                if (bricks.Count > 0)
                {
                    RemoveBrick();
                    bridge.havePassed = true;
                    bridge.ChangeColor();
                    
                }
                else if (bricks.Count <= 0)
                {
                    speed = 0;
                    Debug.Log("Retry");
                }
            }
            else
            {
                return;
            }
        }
        if (other.gameObject.CompareTag("WinPos"))
        {
            speed = 0;
            //ChangeAnim("Win");
            Debug.Log("WinPos");
            
        }


    }



    //if (other.gameObject.CompareTag("Finish"))
    //{
    //    other.gameObject.GetComponent<BoxController>().HitPlayer();
    //}

    //if (other.gameObject.CompareTag("FinishPoint"))
    //{
    //    ClearBrick();
    //    //phat di su kien khi win game
    //    winGameEvent?.Invoke();
    //}

    private void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }

    private void OnInit()
    {
        isMoving = false;
        transform.position = StartPoint.position + offset;
    }

}

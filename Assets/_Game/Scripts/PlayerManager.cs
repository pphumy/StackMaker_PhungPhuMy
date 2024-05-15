using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public float speed = 15f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] public LayerMask layer;
    [SerializeField] Transform player;

    [SerializeField] GameObject brickPrefab, playerBrick, playerModel;

    public int coin = 0;

    List<GameObject> bricks = new List<GameObject>();
    public Transform StartPoint ;

    [SerializeField] Collider playerCollider;

    private RaycastHit hit;
    private bool isMoving = false;
    private Vector3 dir = Vector3.zero;
    private float brickHeight = 0.3f;

    private Vector3 nextPoint;
    private Vector3 offset = new Vector3(0, 3.5f, 0);
    private string currentAnimName;

    private void Awake()
    {
        OnInit();
        

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
        if(GameManager.Instance.currentState == GameManager.GameState.MainMenu)
        {
            return;
        }
        else
        {
            if (!isMoving)
            {
                nextPoint = transform.position;
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

                //Debug.DrawRay(transform.position, dir, Color.red, Mathf.Infinity);
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
            ChangeAnim("Idle");
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
        ChangeAnim("Hit");
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
        playerModel.transform.localPosition = playerModel.transform.localPosition - Vector3.up * 0.3f;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Brick"))
        {

            //Debug.Log("Brick");
            AddBrick();
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
                    //Debug.Log("Retry");
                    LevelManager.Instance.LoadLevel();
                    
                }
            }
            else
            {
                return;
            }
        }
        if (other.gameObject.CompareTag("WinPos"))
        {
           // isWon = true;
            speed = 0;
            ChangeAnim("Win");
            ClearBrick();
            //Debug.Log("WinPos");
            //UIManager.Instance.StartCoroutine(UIManager.Instance.ShowWinUI());
            coin += 50;
            PlayerPrefs.SetInt("Coin", coin);
            GameManager.Instance.WinGame();
        }


    }

    public void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnInit()
    {
        
        isMoving = false;
        speed = 15f;
        InputManager.Instance.direction = EDirection.None;
        
    }
    public void SetStartPoint(Vector3 startPoint)
    {
        transform.position = startPoint + offset;
        Debug.Log(startPoint);
        Debug.Log(transform.position);
    }

}

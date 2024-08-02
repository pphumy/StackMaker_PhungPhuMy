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
    public int score = 0;

    List<GameObject> bricks = new List<GameObject>();
    public Transform StartPoint ;

    [SerializeField] Collider playerCollider;

    private RaycastHit hit;
    private Transform basePos;
    private bool isMoving = false;
    public Vector3 dir = Vector3.zero;
    private float brickHeight = 0.3f;

    private Vector3 nextPoint;
    private Vector3 offset = new Vector3(0, 4f, 0);
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
        if (dir != Vector3.zero)
        {
            MovePlayer();
        }
        
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
                        AudioManager.Instance.PlaySwipeSfx();
                        break;
                    case EDirection.Backward:

                        dir = Vector3.back;
                        AudioManager.Instance.PlaySwipeSfx();
                        break;
                    case EDirection.Right:

                        dir = Vector3.right;
                        AudioManager.Instance.PlaySwipeSfx();
                        break;
                    case EDirection.Left:
                        AudioManager.Instance.PlaySwipeSfx();
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

    public void AddBrick()
    {
        Vector3 newBrickPosition = playerBrick.transform.position;
        GameObject brick = Instantiate(brickPrefab, newBrickPosition, Quaternion.Euler(-90, 0, 0), playerBrick.transform);
        //brick.transform.SetParent(playerBrick.transform);
        //Set vi tri de cac brick xep chong len nhau
        brick.transform.localPosition = new Vector3(0, (bricks.Count + 1) * 0.3f, 0);
        //add brick vua tao ra
        bricks.Add(brick);
        playerModel.transform.localPosition = brick.transform.localPosition-Vector3.up*0.2f;
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
            playerModel.transform.localPosition -= Vector3.up * brickHeight;
            Destroy(brick);
            //set position cua player 
            
        }
    }
    public void setHeight()
    {
        
            playerModel.transform.localPosition -= Vector3.up * brickHeight*(bricks.Count-1);
        
    }

    public void ClearBrick()
    {

        //xoa het gach trong list
        //setHeight();
        playerModel.transform.localPosition = playerModel.transform.localPosition - Vector3.up * brickHeight * (bricks.Count-1);
        foreach (var brick in bricks)
        {
            Destroy(brick);
        }
        bricks.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Brick"))
        {

            //Debug.Log("Brick");
            AddBrick();
            score++;
            UIManager.Instance.scoreUi.SetText(score.ToString());
            AudioManager.Instance.PlayHitSfx();
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
            AudioManager.Instance.PlayWinSfx();
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
        ChangeAnim("Idle");
        isMoving = false;
        speed = 15f;
        InputManager.Instance.direction = EDirection.None;
    }

    public void SetStartPoint(Vector3 startPoint)
    {
        transform.position = startPoint +offset;
        playerModel.transform.localPosition = new Vector3(playerModel.transform.localPosition.x, 0.52f, playerModel.transform.localPosition.z);
        //basePos.po = startPoint+offset;
        Debug.Log(startPoint);
        Debug.Log(transform.position);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 oriPos, targetPos;
    private float timeToMove = 0.2f;

    //[SerializeField]
    //private Tilemap Tilemap_Ground;
    //[SerializeField]
    //private Tilemap Tilemap_Collider;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Transform movePoint;

    public LayerMask whatStopMovement;

    private void Start()
    {
        movePoint.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Horizontal"), 0f);
                }
            }
        }
       //if(Input.GetKey(KeyCode.W) && !isMoving)
       // {
       //     StartCoroutine(MovePlayer(Vector3.up));
       // }
       //if(Input.GetKey(KeyCode.A) && !isMoving)
       // {
       //     StartCoroutine(MovePlayer(Vector3.left));
       // }
       //if (Input.GetKey(KeyCode.S) && !isMoving)
       // {
       //     StartCoroutine(MovePlayer(Vector3.down));
       // }
       // if (Input.GetKey(KeyCode.D) && !isMoving)
       // {
       //     StartCoroutine(MovePlayer(Vector3.right));
       // }
    }

    //private IEnumerator MovePlayer(Vector3 direction)
    //{
    //    isMoving = true;

    //    float elapsedTime = 0;
    //    oriPos = transform.position;
    //    targetPos = oriPos + direction;

    //    while(elapsedTime < timeToMove)
    //    {
    //        transform.position = Vector3.Lerp(oriPos, targetPos, (elapsedTime / timeToMove));
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.position = targetPos;

    //    isMoving = false;
    //}

    /*private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        float elapsedTime = 0;

        if (canMove(direction))
        {
            while (elapsedTime < timeToMove)
            {
                //transform.position = Vector3.Lerp(oriPos, targetPos, (elapsedTime / timeToMove));
                transform.position += direction;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            //transform.position += direction;
            isMoving = false;
        }
    }*/
    
    /*private bool canMove(Vector2 direction)
    {
        Vector3Int gridPosition = Tilemap_Ground.WorldToCell(transform.position + (Vector3)direction);
        if(!Tilemap_Ground.HasTile(gridPosition) || Tilemap_Collider.HasTile(gridPosition))
        {
            Debug.Log("canMove false");
            return false;
            
        }
        Debug.Log("canMove true");
        return true;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    //private bool isMoving;
    //private Vector3 oriPos, targetPos;
    //private float timeToMove = 0.2f;

    //[SerializeField]
    //private Tilemap Tilemap_Ground;
    //[SerializeField]
    //private Tilemap Tilemap_Collider;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Transform movePoint;
    [SerializeField]
    private LayerMask whatStopMovement;

    private GameObject[] key;
    
    public bool haveKey = false;

    private void Start()
    {
        movePoint.parent = null;
        key = GameObject.FindGameObjectsWithTag("Key");

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(key.Length);

        //haveKey = true;

        if (col.gameObject.tag == "Key")
        {
            Debug.Log("Key !");
            haveKey = true;
            Debug.Log(haveKey);
            GameZone.Destroy(key[0]);
        }

        if (col.gameObject.tag == "Door")
        {
            Debug.Log("Door !");
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Transform movePoint;
    [SerializeField]
    private LayerMask whatStopMovement;

    private GameObject[] key;
    private GameObject[] door;
    private Scene scene;
    
    public bool haveKey = false;
    public bool haveFinish = false;

    private void Start()
    {
        movePoint.parent = null;
        key = GameObject.FindGameObjectsWithTag("Key");
        door = GameObject.FindGameObjectsWithTag("Door");
        scene = SceneManager.GetActiveScene();
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
        
        if(scene.name == "Level 2")
        {
            haveFinish = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Key")
        {
            haveKey = true;
            Debug.Log("Key !"+haveKey);
            GameZone.Destroy(key[0]);
        }

        if (col.gameObject.tag == "Door")
        {          
            if (!(col.gameObject.tag == "Door" && haveKey))
            {
                Debug.Log("You need a key !");
            }
            else {Debug.Log("Door open !");
            GameZone.Destroy(door[0]); }
            
        }

        if (col.gameObject.tag == "Exit")
        {
            Debug.Log("haveFinish" + haveFinish);

            if (haveFinish)
            {
                SceneManager.LoadScene("Menu");
                haveFinish = false;
            }
            else if (!haveFinish)
            {
                SceneManager.LoadScene("Level 2");
                haveFinish = true;
                haveKey = false;
            }
        }
       
        
    }
}

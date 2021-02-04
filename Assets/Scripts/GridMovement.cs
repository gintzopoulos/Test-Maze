using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Transform movePoint;
    [SerializeField]
    private LayerMask whatStopMovement;
    [SerializeField]
    private GameObject keyImage;
    [SerializeField]
    public bool haveKey = false;
    [SerializeField]
    public bool haveFinish = false;

    private GameObject[] key;
    private GameObject[] door;
    private Scene scene;

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
        //Player movements - BEGIN
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            //Left - Right Movements
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            //Top - Down Movements
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }//Player movements - END
        

        
        IsPossesKey();

        //Put "haveFinish" at True if the player have finish the first level
        if (scene.name == "Level 2")
        {
            haveFinish = true;
        }
    }

    //Collider detection
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Detect Key's collider
        if (col.gameObject.tag == "Key")
        {
            haveKey = true;
            Debug.Log("Key !"+haveKey);
            GameZone.Destroy(key[0]);
        }

        //Detect Door's collider
        if (col.gameObject.tag == "Door")
        {          
            if (!haveKey)
            {
                Debug.Log("You need a key !");
            }
            else {Debug.Log("Door open !");
            GameZone.Destroy(door[0]); }
            
        }

        //Detect Exit's collider
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

    //Show if the player have the key
    private void IsPossesKey()
    {
        if (haveKey)
        {
            keyImage.SetActive(true);
        }
        else
        {
            keyImage.SetActive(false);
        }    
    }
}

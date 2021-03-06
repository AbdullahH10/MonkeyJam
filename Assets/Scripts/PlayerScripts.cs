using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScripts : MonoBehaviour
{
    private Rigidbody2D myBody;
    public float moveSpeed = 2f;
    public float normal_Push = 10f;
    public float extra_Push = 14f;

    private bool initial_Push;
    private int push_Count;
    private bool player_Died;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {

        if (player_Died)
            return;
        //if pressed right arrow key
        if (Input.GetAxisRaw("Horizontal") > 0)
        {

            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);

        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {

            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);

        }//if pressed left arrow key

    } // player movement

    void OnTriggerEnter2D(Collider2D target)
    {
        if (player_Died)
            return;
        if (target.tag == "ExtraPush")
        {
            if ( !initial_Push )
            {
                initial_Push = true;
                myBody.velocity = new Vector2(myBody.velocity.x, 18f);

                target.gameObject.SetActive(false);

                //sound manager
                //exit from the on trigger enter because of inital push

                return;
            } // initial push

            // outside of the initial push

        } // because of the initial push
        if (target.tag == "NormalPush")
        {

            myBody.velocity = new Vector2(myBody.velocity.x, normal_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            SoundManager.instance.JumpSoundFX();

        }

        if (target.tag == "ExtraPush")
        {

            myBody.velocity = new Vector2(myBody.velocity.x, extra_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            SoundManager.instance.JumpSoundFX();

        }

        if (push_Count == 2)
        {

            push_Count = 0;
            PlatformSpawner.instance.SpawnPlatform();

        }

        if (target.tag == "FallDown" || target.tag == "Bird")
        {
        
            player_Died = true;

            SoundManager.instance.GameOverSoundFX();

            //GameManager.instance.RestartGame();

            SceneManager.LoadScene("Gameover");
        }

    } // on trigger enter

}

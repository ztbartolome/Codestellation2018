using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour {
    Queue<string> actions;

    // This image is the object that holds the png
    [SerializeField] Image image;

    [SerializeField] Button walk;
    [SerializeField] Button start;

    //character sprite
    [SerializeField] GameObject character;
	Animator anim; //THIS IS THE ANIMATOR THING DO NOT DELETE

    // This is the music that plays during the game
    AudioSource source;
    [SerializeField] AudioClip playMusic;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip buttonPress1;
    [SerializeField] AudioClip buttonPress2;


    private bool dequeuing, executing = false;
    private string action = "";
    private float originalXPos, originalYPos;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        actions = new Queue<string>();
		anim = character.GetComponent<Animator> ();
		anim.SetInteger ("State", 0);
        source.Play();
	}

    // Update is called once per frame
    void Update()
    {
        if (dequeuing && actions.Count!=0)
        {
            if (!executing)
            {
                action = actions.Peek();
                originalXPos = character.transform.position.x;
                originalYPos = character.transform.position.y;
                if (isOnGround())

                {
                    if (action == "jump")
                    {
                        source.PlayOneShot(jump);
                    }
                    executing = true;
                } else if(FindObjectOfType<WindDraft>()!=null && FindObjectOfType<WindDraft>().inDraft)
                {
                    dequeuing = false;
                    actions.Clear();
                }
            } else {
                if (action == "walk" && (FindObjectOfType<WindDraft>() == null || !FindObjectOfType<WindDraft>().inDraft))
                {
                    anim.SetInteger("State", 1);
                    WalkForwards();
                }
                else if (action == "jump")
                {
                    anim.SetInteger("State", 2);
                    jumpUp();
                } else
                {
                    originalXPos = character.transform.position.x;
                    originalYPos = character.transform.position.y;
                }
            }
        } else
        {
            dequeuing = false;
			anim.SetInteger ("State", 0);
        }
    }

    public void clickJump() {
        actions.Enqueue("jump");
        source.PlayOneShot(buttonPress2);
    }

    public void clickWalk()
    {
        print("Walk queued.");
        actions.Enqueue("walk");
        source.PlayOneShot(buttonPress2);
    }

    public void startDequeue(){
        print("Dequeuing...");
        if (actions.Count > 0)
        {
            dequeuing = true;
            source.Stop();
            source.PlayOneShot(playMusic);
        }
        source.PlayOneShot(buttonPress1);
    }

    private void WalkForwards()
    {
        
        if (character.transform.position.x < (originalXPos + 2f))
        {
            character.transform.position = new Vector2(character.transform.position.x + .13f, character.transform.position.y);
        }
        else
        {
            DropAndCheckEnd();
            character.transform.position = new Vector2(originalXPos + 2f, character.transform.position.y);
        }
    }

    private void jumpUp()
    {
        if (character.transform.position.y < (originalYPos + 2.7f))
        {
            character.transform.position = new Vector2(character.transform.position.x, character.transform.position.y+.43f);
        }
        else
        {
            DropAndCheckEnd();
            character.transform.position = new Vector2(character.transform.position.x, originalYPos + 2.5f);
        }
    }

    public void DropAndCheckEnd()
    {
        executing = false;
        actions.Dequeue();
        if (actions.Count == 0)
        {
            source.Stop();
            source.Play();
        }
    }

    public bool isOnGround() {
        //check Layer type of object colliding with character
        return character.GetComponent<CircleCollider2D>().IsTouchingLayers();
    }
}
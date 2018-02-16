using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{

    public float restartLevelDelay = 1f;

    private Animator animator;
    private int wallDamage = 1;

	// Use this for initialization
	protected override void Start ()
    {
        animator = GetComponent<Animator>();
        base.Start();
	}

    // Update is called once per frame
    void Update ()
    {
		int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
	}

    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        base.AttemptMove <T> (xDir, yDir);

        //RaycastHit2D hit;

        /*if (Move (xDir, yDir, out hit))
        {

        }*/
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
    }

    protected override void OnCantMove <T> (T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

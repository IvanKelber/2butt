using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ResetableObject
{
    public float maxSpeed = 10;
    [Range(.05f, 10)]
    public float minJumpHeight = 3;
    [Range(.05f,10)]
    public float timeToJumpApex = 1;
    [Range(0,100)]
    public float maxAcceleration = 25, maxAirAcceleration = 75;

    [Range(.01f, 2)]
    public float kickDistance = .5f;

    public LayerMask squareMask;


    float maxJumpVelocity, minJumpVelocity;

    Vector2 playerInput;
    Vector2 desiredVelocity;
    bool desiredJump;
    bool desiredFloat;
    bool onGround;
    bool desiredKick;

    int playerDirection = 1;

    Vector2 velocity;
    Rigidbody2D body;
    BoxCollider2D collider;
    Bounds bounds;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        maxJumpVelocity = Mathf.Abs(Physics2D.gravity.y * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * minJumpHeight);
    }

    void Update () {
        UpdateBounds();
		playerInput.x = Input.GetAxis("Horizontal");
		// playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);

		desiredVelocity =
			new Vector2(playerInput.x, 0) * maxSpeed;

        desiredJump |= Input.GetKeyDown(KeyCode.W);
        if(Input.GetKeyUp(KeyCode.W)) {
            desiredFloat = true;
        }

        desiredKick |= Input.GetKeyDown(KeyCode.Space);
        if(desiredKick) {
            Debug.Log("Desired Kick");
            Kick();
            desiredKick = false;
        }
	}

	void FixedUpdate () {
		velocity = body.velocity;
		float maxSpeedChange = maxAcceleration * Time.deltaTime;
		velocity.x =
			Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        if(velocity.x != 0) {
            playerDirection = (int) Mathf.Sign(velocity.x);
        }

        if(desiredJump) {
            desiredJump = false;
            Jump();
        }
        if(desiredFloat) {
            if(velocity.y > minJumpVelocity) {
                velocity.y = minJumpVelocity;
            }
        }
		body.velocity = velocity;
        onGround = false;
        desiredFloat = false;
	}

    void Jump() {
        if(onGround) {
            velocity.y += maxJumpVelocity;
        } else { 
            Debug.Log("Not on ground");
        }
    }

    void Kick() {
        FlipSquare square = CheckForSquare();
        if(square) {
            Debug.Log("kicking square");
            square.GetKicked(playerDirection);
        }
    }

    void UpdateBounds() {
        bounds = collider.bounds;
    }



    FlipSquare CheckForSquare() {
        Vector2 leftBottom = new Vector2(bounds.center.x, bounds.center.y) + 
            (Vector2.down * bounds.extents) + (Vector2.left * bounds.extents);
        Vector2 rightBottom = new Vector2(bounds.center.x, bounds.center.y) + 
            (Vector2.down * bounds.extents) + (Vector2.right * bounds.extents);        

        for(int i = 0; i < 10; i++) {
            Vector2 rayOrigin = (playerDirection == 1 ? rightBottom : leftBottom) + Vector2.up * i * (bounds.size.x/10);

            Debug.DrawRay(rayOrigin, Vector2.right * playerDirection * kickDistance, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * playerDirection, kickDistance, squareMask);

            if(hit) {
                if(hit.collider != null) {
                    FlipSquare square = hit.collider.gameObject.GetComponent<FlipSquare>() as FlipSquare;
                    Debug.Log("found square: " + square);
                    return square;
                }
            }
        }
        return null;
    }

    void OnCollisionEnter2D () {
		onGround = true;
	}

    void OnCollisionStay2D() {
        onGround = true;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector2 leftBottom = new Vector2(bounds.center.x, bounds.center.y) + 
            (Vector2.down * bounds.extents) + (Vector2.left * bounds.extents);
        Vector2 rightBottom = new Vector2(bounds.center.x, bounds.center.y) + 
            (Vector2.down * bounds.extents) + (Vector2.right * bounds.extents);        

        for(int i = 0; i < 10; i++) {
            Vector2 rayOrigin = (playerDirection == 1 ? rightBottom : leftBottom) + Vector2.up * i * (bounds.size.x/10);

            Gizmos.DrawRay(rayOrigin, Vector2.right * playerDirection * kickDistance);
        }
    }

}

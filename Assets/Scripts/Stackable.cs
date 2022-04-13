using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StackItem { pillow, cushion, mattress, pea, watermelon, melonslice, icecube, crown };

public class Stackable : MonoBehaviour
{
    // Is this a hazard object? (Immediately wakes up princess on contact)
    public bool isHazard;

    // What type of stack item is this?
    public StackItem itemType;

    // Used to melt the ice cube
    private bool isMelting = false;

    private void FixedUpdate()
    {
        if (itemType == StackItem.icecube && GameManager.S.gameState == GameState.sleeping && !isMelting)
        {
            isMelting = true;
            StartCoroutine(Melt());
        }
    }

    private IEnumerator Melt()
    {
        float meltTime = 0.0f;
        float totalTime = 4.0f;
        Vector3 initialScale = transform.localScale;
        Vector3 finalScale = new Vector3(0.1f, 0.1f, 0.1f);

        while (meltTime < totalTime)
        {
            meltTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, finalScale, meltTime / totalTime);
            yield return null;
        }

        // Once it reaches such a small size, it's destroyed
        Destroy(gameObject);
    }

    // TODO: Setup to detect collisions from the children, use those forces to determine when to break:
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (itemType == StackItem.watermelon)
        {
            float force = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity) * collision.rigidbody.mass;
            Debug.Log("Collided with " + collision.gameObject.name + " with force " + force);
        }
    }
    */
}

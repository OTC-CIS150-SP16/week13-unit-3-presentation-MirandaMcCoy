using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemController : MonoBehaviour {

    // dGem =          Active looking gem on door
    // dDeGem =        Inactive looking gem on door
    // gemsCollected = Has the user collected all the gems?
    public GameObject dGem1, dGem2, dGem3, dDeGem1, dDeGem2, dDeGem3;
    public int gemsCollected = 0;

    // touch =         audio cue gem was touched
    // winning =       audio cue for the end
      // For some reason, the audio gets shorter with each play
      // Serperating them out lessens this effect
    // audio =         audiosource on the player
    public AudioClip touch1, touch2, touch3, winning;
    AudioSource audio;

    public Text text;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}

    // When user collides with an on trigger object
    void OnTriggerEnter(Collider col)
    {
        // More efficient that if (coll.getTag == "Pick Up") ((Or whatever it is))
        // When user collides with a non-door gem, that gem disappears and the
          // corresponding deactivated gem on the door changes to an active one
          // (Or rather an active one is moved to its place)
          // Hindsight is 20/20 but it would have been more efficent to maybe
          // move the existing gem to the deactivated position instead of having
          // a seperate one, but oh well. *Wayy too lazy*
          // Also plays sound to indicate player has done good
        if (col.gameObject.CompareTag("Gem"))
        {
            if (col.gameObject.name == "Gem (1)")
            {
                Vector3 pos = dGem1.transform.position;
                pos.x = 33.9F;
                pos.z = -9.72F;
                dGem1.transform.position = pos;

                audio.PlayOneShot(touch1, 1F);

                dDeGem1.SetActive(false);
            }
            else if (col.gameObject.name == "Gem (2)")
            {
                Vector3 pos = dGem2.transform.position;
                pos.x = 33.9F;
                pos.z = -9.72F;
                dGem2.transform.position = pos;

                audio.PlayOneShot(touch2, 1F);

                dDeGem2.SetActive(false);
            }
            else if (col.gameObject.name == "Gem (3)")
            {
                Vector3 pos = dGem3.transform.position;
                pos.x = 33.9F;
                pos.z = -9.72F;
                dGem3.transform.position = pos;

                audio.PlayOneShot(touch3, 1F);

                dDeGem3.SetActive(false);
            }

            gemsCollected++;
            col.gameObject.SetActive(false);

        }
        // If the user touches the gate instead, the game checks if all the gems
          // have been collected. If not, the game's trigger is set to false so
          // that the user can't pass through it.  It's not pretty, but it works.
          // If all gems were collected, the gate and everything in it is deactivated
          // so the user can get to the end.
        else if (col.gameObject.CompareTag("Gate")) // If user touches gate
        {
            if (gemsCollected == 3) // If all gems have been collected, deactivate door and gems
            {
                audio.PlayOneShot(winning, 1F);
                col.gameObject.SetActive(false);
                dGem1.SetActive(false);
                dGem2.SetActive(false);
                dGem3.SetActive(false);
                
            } else
            {
                col.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }

        // If user collides with the winning gem, game over

        else if (col.gameObject.CompareTag("Finish"))
        {
            print("You win!");
            text.text = "You Win!";
            col.gameObject.SetActive(false);
        }
    }

    // If the user set the gate's trigger to false, when the user stops touching
      // it, that value is returned to true
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Gate"))
        {
            col.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}

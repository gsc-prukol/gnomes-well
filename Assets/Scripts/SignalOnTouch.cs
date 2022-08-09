using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class SignalOnTouch : MonoBehaviour
{
    public UnityEvent onTouch;

    public bool palyAudioOnTouch = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SendSignal(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SendSignal(collision.gameObject);
    }

    private void SendSignal(GameObject objectThatHit)
    {
        if (!objectThatHit.CompareTag(Tags.PLAYER))
        {
            return;
        }

        if (palyAudioOnTouch)
        {
            var audio = GetComponent<AudioSource>();
            if (audio && audio.gameObject.activeInHierarchy)
            {
                audio.Play();
            }
        }

        onTouch?.Invoke();
    }
}

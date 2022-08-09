using UnityEngine;

public class Swinging : MonoBehaviour
{
    public float swingSensitivity = 100f;

    private void FixedUpdate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody == null)
        {
            Destroy(this);
            return;
        }

        float swing = InputManager.instance.sidewaysMotion;
        var force = new Vector2(swing * swingSensitivity, 0);

        rigidbody.AddForce(force);
    }
}

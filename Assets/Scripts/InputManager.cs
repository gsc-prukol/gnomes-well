using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private float _sidewaysMotion = 0.0f;

    public float sidewaysMotion => _sidewaysMotion;

    // Update is called once per frame
    void Update()
    {
        _sidewaysMotion = Input.acceleration.x;
    }
}

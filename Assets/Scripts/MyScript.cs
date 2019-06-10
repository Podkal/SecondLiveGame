
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class MyScript : MonoBehaviour

{

    public FixedJoystick MoveJoystick;
    public FixedButton JumpButton;
    public FixedTouchField TouchField;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
            var fps = GetComponent<RigidbodyFirstPersonController>();

            fps.RunAxis = MoveJoystick.Direction;
            fps.JumpAxis = JumpButton.Pressed;
            fps.mouseLook.LookAxis = TouchField.TouchDist;
       
    }
}

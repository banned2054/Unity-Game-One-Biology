using UnityEngine;
using UnityEngine.UI;

public class CameraMgr : MonoBehaviour
{
    public Slider SpeedSlider;

    float mDelta = 10; // Pixels. The width border at the edge in which the movement work
    float mSpeed = 5.0f; // Scale. Speed of the movement

    private Vector3 mRightDirection = Vector3.right;
    private Vector3 mLeftDirection = Vector3.left;
    private Vector3 mUpDirection = Vector3.up;
    private Vector3 mDownDirection = Vector3.down;

    void Update()
    {
        mSpeed = SpeedSlider.value;

        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            transform.position += mRightDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.x <= 0)
        {
            transform.position += mLeftDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y >= Screen.height - mDelta)
        {
            transform.position += mUpDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y <= 70)
        {
            transform.position += mDownDirection * Time.deltaTime * mSpeed;
        }
    }
}
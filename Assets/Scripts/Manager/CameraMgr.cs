using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class CameraMgr : MonoBehaviour
    {
        public Slider speedSlider;

        private const float MDelta = 10;

        private float _mSpeed = 5.0f;

        private readonly Vector3 _mRightDirection = Vector3.right;
        private readonly Vector3 _mLeftDirection  = Vector3.left;
        private readonly Vector3 _mUpDirection    = Vector3.up;
        private readonly Vector3 _mDownDirection  = Vector3.down;

        private void Update()
        {
            _mSpeed = speedSlider.value;

            if (Input.mousePosition.x >= Screen.width - MDelta)
            {
                transform.position += _mRightDirection * (Time.deltaTime * _mSpeed);
            }

            if (Input.mousePosition.x <= 0)
            {
                transform.position += _mLeftDirection * (Time.deltaTime * _mSpeed);
            }

            if (Input.mousePosition.y >= Screen.height - MDelta)
            {
                transform.position += _mUpDirection * (Time.deltaTime * _mSpeed);
            }

            if (Input.mousePosition.y <= 70)
            {
                transform.position += _mDownDirection * (Time.deltaTime * _mSpeed);
            }
        }
    }
}

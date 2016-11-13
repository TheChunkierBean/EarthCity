using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public Vector2 clampInDegrees = new Vector2(360, 180);
        public Vector2 sensitivity = new Vector2(2, 2);
        public Vector2 smoothing = new Vector2(3, 3);
        public Vector2 targetDirection;
        public Vector2 targetCharacterDirection;

        // Assign this if there's a parent object controlling motion, such as a Character Controller.
        // Yaw rotation will affect this object instead of the camera if set.
        public GameObject characterBody;

        public Transform cameraObject;

        Vector2 _mouseAbsolute;
        Vector2 _smoothMouse;

        Vector2 _initSensitivity;

        private void Awake ()
        {
            // Set target direction to the camera's initial orientation.
            targetDirection = cameraObject.localRotation.eulerAngles;

            _initSensitivity = sensitivity;

            // Set target direction for the character body to its inital state.
            if (characterBody) 
                targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
        }

        public void UpdateLookState (PlayerInput.Input input)
        {
            // Allow the script to clamp based on a desired target value.
            var targetOrientation = Quaternion.Euler(targetDirection);
            var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

            // Get raw mouse input for a cleaner reading on more sensitive mice.
            var mouseDelta = new Vector2(input.VerticalLook, input.HorizontalLook);

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
            _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

            // Find the absolute mouse movement value from point zero.
            _mouseAbsolute += _smoothMouse;

            // Clamp and apply the local x value first, so as not to be affected by world transforms.
            if (clampInDegrees.x < 360)
                _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

            var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
            cameraObject.localRotation = xRotation;

            // Then clamp and apply the global y value.
            if (clampInDegrees.y < 360)
                _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

            cameraObject.localRotation *= targetOrientation;

            // If there's a character body that acts as a parent to the camera
            if (characterBody)
            {
                var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, characterBody.transform.up);
                characterBody.transform.localRotation = yRotation;
                characterBody.transform.localRotation *= targetCharacterOrientation;
            }
            else
            {
                var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, cameraObject.InverseTransformDirection(Vector3.up));
                cameraObject.localRotation *= yRotation;
            }
        }

        public void OnWeaponAimed (PlayerController p, Weapon w)
        {
			float FOVRatio = w.ScopeToFOVRatio;
			float currentFieldOfView = 90.0F / 100.0F * FOVRatio;			// 90 is the default FOV. This should be changeable!
			
			p.PlayerCamera.fieldOfView = currentFieldOfView;

            sensitivity.x = _initSensitivity.x / 100 * FOVRatio;
            sensitivity.y = _initSensitivity.y / 100 * FOVRatio;
        }
    }
}
using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 10f;        // The speed with which the camera will be following.
		public float zoomSpeed = 4.0f;
		public float minZoom = 0.2f;
		public float maxZoom = 1f;

		private float currentZoom = 10f;
        private Vector3 offset;                     // The initial offset from the target.


        void Start ()
        {
            // Calculate the initial offset.
			offset = transform.position - target.position;
        }


        void FixedUpdate ()
        {
			//zoom 
			currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
			currentZoom = Mathf.Clamp (currentZoom, minZoom, maxZoom);
			// Create a postion the camera is aiming for based on the offset from the target.
			Vector3 targetCamPos = target.position + (offset * currentZoom);

            // Smoothly interpolate between the camera's current position and it's target position.
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);
			//transform.position = targetCamPos;
        }
    }
}
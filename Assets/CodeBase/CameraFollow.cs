using UnityEngine;

namespace CodeBase {
    public class CameraFollow : MonoBehaviour {
        public Transform player;
        public float smoothing = 5f;
        private Vector3 offset;

        private void Start() {
            offset = transform.position - player.position;
        }

        private void LateUpdate() {
            if(player == null)
                return;
            Vector3 targetCamPos =
                new Vector3(transform.position.x, transform.position.y, player.position.z + offset.z);

            if (transform.position.z >= -0.5f) {
                if (targetCamPos.z < transform.position.z) {
                    transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
                }

                return;
            }

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
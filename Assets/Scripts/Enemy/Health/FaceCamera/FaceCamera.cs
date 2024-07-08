using UnityEngine;


namespace Enemy.Health.FaceCamera
{
    public class FaceCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public Camera Camera
        {
            get => _camera;
            set => _camera = value;
        }

        private void Update()
        {
            transform.LookAt(_camera.transform, Vector3.up);
        }
    }
}
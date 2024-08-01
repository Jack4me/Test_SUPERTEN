using CodeBase.Input;
using Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero {
    public class HeroMove : MonoBehaviour {
        [SerializeField] private GameObject enemy;
        public CharacterController characterController;
        public float movementSpeed = 4.0f;
        public InputService _inputService;
        private Camera _camera;
        private const float EPSILON = 0.01f;
        private Animator animator;
        public float enemyDetectionRadius = 10f; // Радиус поиска врагов
        public LayerMask enemyLayer;
        private AttackHero attackHero;
        public float xLimit = 8.9f;
        private Vector3 movementVector;
        private void Awake() {
            _inputService = new StandaloneInputService();
            animator = GetComponentInChildren<Animator>();
            attackHero = GetComponentInChildren<AttackHero>();
        }

        private void Start() {
            _camera = Camera.main;
        }

        private void Update() {
            movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > EPSILON) {
                //Трансформируем экранныые координаты вектора в мировые
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
           
            characterController.Move(movementSpeed * movementVector * Time.deltaTime);
            if (Mathf.Abs(_inputService.Axis.x) > 0.01f || Mathf.Abs(_inputService.Axis.y) > 0.01f) {
                animator.SetBool("Moving", true);
            }
            else {
                animator.SetBool("Moving", false);
                FindNearestEnemy();
            }
        }

        


        private void FindNearestEnemy() {
            Collider[] enemies = Physics.OverlapSphere(transform.position, enemyDetectionRadius, enemyLayer);
            if (enemies.Length == 0) {
                Debug.Log("No enemies found.");
                return;
            }

            Collider nearestEnemy = null;
            float shortestDistance = Mathf.Infinity;

            foreach (Collider enemy in enemies) {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < shortestDistance) {
                    shortestDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null) {
                Debug.Log("Nearest enemy found: " + nearestEnemy.name);
                transform.LookAt(nearestEnemy.transform);
                SmoothlyRotate(nearestEnemy.transform);
                attackHero.Shoot(nearestEnemy.transform);
            }
        }

        private void SmoothlyRotate(Transform enemy) {
            float rotationSpeed = 2;
            Quaternion targetRotation = Quaternion.LookRotation(enemy.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
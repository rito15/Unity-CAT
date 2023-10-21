using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2021-01-15
    public class Demo_Required : MonoBehaviour
    {
        [Required]
        public GameObject requiredGameObject;

        [Required]
        public Collider requiredCollider;

        [Required(ShowMessageBox = false)]
        public Transform requiredTransform;

        //[Required(ShowLogError = true)]
        public Rigidbody requiredRigidbody;
    }
}
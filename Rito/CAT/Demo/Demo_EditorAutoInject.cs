using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2021-01-15
    // 작성자 : Rito
    public class Demo_EditorAutoInject : MonoBehaviour
    {
        [EditorAutoInject(EInjection.FindObjectOfType)]
        public Camera _findObjectOfType;


        [EditorAutoInject(EInjection.GetComponent)]
        public GameObject _getC1;

        [EditorAutoInject(EInjection.GetComponent)]
        public Transform _getC2;

        [EditorAutoInject(EInjection.GetComponent, true)]
        public CapsuleCollider _getCEvenDisabled;



        [EditorAutoInject(EInjection.GetComponentInChildren)]
        public CapsuleCollider _getCInChildren; // 자신

        [EditorAutoInject(EInjection.GetComponentInChildren)]
        public BoxCollider _getCInChildren2; // 자식

        [EditorAutoInject(EInjection.GetComponentInChildren, injectEvenDisabled: true)]
        public CapsuleCollider _getCInChildrenEvenDisabled; // 자신

        [EditorAutoInject(EInjection.GetComponentInChildren, injectEvenDisabled: true)]
        public BoxCollider _getCInChildrenEvenDisabled2; // 자식



        [EditorAutoInject(EInjection.GetComponentInParents)]
        public Light _getCInParents;

        [EditorAutoInject(EInjection.GetComponentInParentsOnly)]
        public Light _getCInParentsOnly;

        [EditorAutoInject(EInjection.GetComponentInParents, injectEvenDisabled: true)]
        public Light _getCInParentsEvenDisabled;

        [EditorAutoInject(EInjection.GetComponentInParentsOnly, injectEvenDisabled: true)]
        public Light _getCInParentsOnlyEvenDisabled;
    }
}
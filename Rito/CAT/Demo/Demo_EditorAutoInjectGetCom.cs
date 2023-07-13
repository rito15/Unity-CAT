using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2021-01-15
    // 작성자 : Rito
    public class Demo_EditorAutoInjectGetCom : MonoBehaviour
    {
        [EditorAutoInject(EInjection.GetComponent)]
        public GameObject _getC1;

        [EditorAutoInject(EInjection.GetComponent)]
        public Transform _getC2;

        [EditorAutoInject(EInjection.GetComponent, true)]
        public CapsuleCollider _getCEvenDisabled;
    }
}
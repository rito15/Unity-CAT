using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2021-01-15
    // 작성자 : Rito
    public class Demo_EditorDIGetCom : MonoBehaviour
    {
        [EditorDI(DiMethod.GetComponent)]
        public GameObject _getC1;

        [EditorDI(DiMethod.GetComponent)]
        public Transform _getC2;

        [EditorDI(DiMethod.GetComponent, true)]
        public CapsuleCollider _getCEvenDisabled;
    }
}
using Rito.CAT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-15
    // 작성자 : Rito
    public class Demo_EditorDIGetComParent_NC : MonoBehaviour
    {
        [EditorDI(DiMethod.GetComponentInParents, nameIncludes: "NONO", evenDisabled: false)]
        public Transform _parents_Fail1;

        [EditorDI(DiMethod.GetComponentInParentsOnly, nameIncludes: "NONO", evenDisabled: false)]
        public Transform _parents_Fail2;

        [EditorDI(DiMethod.GetComponentInParents, nameIncludes: "DI", evenDisabled: false)]
        public Light _parents_Suc1;

        [EditorDI(DiMethod.GetComponentInParentsOnly, nameIncludes: "DI", evenDisabled: false)]
        public Light _parents_Suc2;


        [EditorDI(DiMethod.GetComponentInParents, nameIncludes: "NONO")]
        public Transform _parents_dsv_Fail1;

        [EditorDI(DiMethod.GetComponentInParentsOnly, nameIncludes: "NONO")]
        public Transform _parents_dsv_Fail2;

        [EditorDI(DiMethod.GetComponentInParents, nameIncludes: "DI")]
        public Light _parents_dsv_Suc1;

        [EditorDI(DiMethod.GetComponentInParentsOnly, nameIncludes: "DI")]
        public Light _parents_dsv_Suc2;
    }
}

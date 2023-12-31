# Unity CAT
- Unity Custom ATtributes
- `2023.3.21f1 (LTS)`
- UPM
```
https://github.com/rito15/Unity-CAT.git
```

<br>

## EditorDI
- 에디터 모드에서 컴포넌트 타입, 게임오브젝트 이름 일치 여부를 검사하여 자동으로 Dependency Injection 수행
- 플레이 모드 런타임에 수행하지 않으므로, 런타임 성능 저하 X
- 비활성화된 게임오브젝트 탐색 On/Off 가능
- 컴포넌트 타입뿐만 아니라 GameObject 타입에도 사용 가능(`ComponentType` 옵션 지정 필요)
```cs
[EditorDI]
[SerializeField] private LoadingProgress _progress;

[EditorDI(nameIncludes:"Loading Bar"), SerializeField]
private Image _targetProgressImage;

[EditorDI(DiMethod.FindObjectOfType, "BG")]
public RawImage _targetBG;
```
![image](https://github.com/rito15/Unity-CAT/assets/42164422/0c2cedf3-e03c-48f6-a41a-620f2181d8cf)

![image](https://github.com/rito15/Unity-CAT/assets/42164422/55f09891-1a5c-442f-a130-c7bb830c6fa6)

### 옵션
|프로퍼티|설명|
|---|---|
|**Method** | `FindObjectOfType`(기본값), `GetComponent`, `GetComponentInChildren`, `GetComponentInChildrenOnly`, `GetComponentInParents`, `GetComponentInParentsOnly`|
|**IncludeInactive** | 비활성화된 게임오브젝트를 포함하여 탐색 (기본값 : `true`)|
|**NameIncludes** | 지정한 문자열이 이름에 포함되는 게임오브젝트들만 탐색 (기본값 : `null`)|
|**NameEquals** | 지정한 문자열이 이름과 일치하는 게임오브젝트들만 탐색. `NameIncludes`보다 우선 적용 (기본값 : `null`)|
|**IgnoreCase** | `NameIncludes`, `NameEquals` 옵션에 대해 대소문자 구분하지 않고 적용 (기본값 : `true`)|
|**ComponentType** | 애트리뷰트의 대상이 게임오브젝트 타입인 경우에만 사용. `typeof(SomeComponent)` 꼴로 지정.|

<br>

## MethodButton
- 클릭 시 해당 이름의 메소드를 호출하는 버튼을 인스펙터에 그린다.
- 필드 위에 애트리뷰트로 추가하거나, `MethodButton` 클래스 타입 필드를 사용할 수 있다.
```cs
// Type 1 : Attribute
[MethodButton(nameof(TestMethod))]
public bool _a;

// Type 2 : MethodButton Class Field
public MethodButton mb1 = nameof(TestMethod);
public MethodButton mb2 = (nameof(TestMethod), "Custom Button Name");
public MethodButton mb3 = (nameof(TestMethod), "Custom Button Name", 40); // + Height

private void TestMethod()
{
    // ...
}
```
![image](https://github.com/rito15/Unity-CAT/assets/42164422/046cc3c3-4a72-45a9-84fd-6e47164b165a)

<br>

## Readonly
- 인스펙터의 프로퍼티를 읽기 전용 상태로 표시한다.
```cs
[Readonly] public int intValue = 3;
[Readonly, SerializeField] private bool boolValue = true;
[Readonly, EditorDI(ComponentType = typeof(Camera))] public GameObject go;
[Readonly] public List<int> intList = new List<int> { 1, 2, 3 };
[Readonly] public CustomClass customClass = new CustomClass { a = 10, b = "string" };

[System.Serializable]
public class CustomClass
{
    public int a;
    public string b;
}
```
![image](https://github.com/rito15/Unity-CAT/assets/42164422/b474c181-e56e-4def-af34-39aca738b93b)

<br>

## ReadonlyIf
- 지정한 필드의 값에 따라 인스펙터의 프로퍼티를 읽기 전용으로 만든다.
```cs
public bool condition = true;
[Range(100, 101)] public int  mustBe100 = 100;

[Space(12)]
[ReadonlyIf(nameof(condition), true)]
public int _conditionTrue = 1;

[ReadonlyIf(nameof(condition), true, EQorNE:false)]
public float _conditionFalse = 2;

[ReadonlyIf(nameof(condition), true)] // OR 1
[ReadonlyIf(nameof(mustBe100), 100)]  // OR 2
public GameObject _conditionTrueOr100;
```
![2023_1022_ReadonlyIf](https://github.com/rito15/Unity-CAT/assets/42164422/4cdd37bd-6940-418f-b966-ac9d9dc01aa1)

<br>

## ShowIf, HideIf
- 지정한 필드의 값에 따라 인스펙터의 프로퍼티를 보여주거나 숨긴다.
```cs
public bool condition = true;
public bool condition2 = true;

[Header("[Show If]")]
[ShowIf(nameof(condition), true)]
public int showVal1 = 1;

[ShowIf(nameof(condition), true)]
public float showVal2 = 2;

[ShowIf(nameof(condition), true)]   // And 1
[ShowIf(nameof(condition2), true)]  // And 2
public GameObject showVal3;

[Header("[Hide If]")]
[HideIf(nameof(condition), true)]
public float hideVal1 = 456f;

[HideIf(nameof(condition), true)]
public string hideVal2 = "Hide If Condition is true";
```
![2023_1022_ShowHideIf](https://github.com/rito15/Unity-CAT/assets/42164422/fad53e88-ecf8-43af-b49d-b80a6c87c079)

<br>

## ColorIf
```cs
public bool condition1 = true;
public bool condition2 = true;

[Space(12)]
[ColorIf(nameof(condition1), true, 2.0f, 0.5f, 0.5f)]
public int _conditionTrue = 1;

[ColorIf(nameof(condition1), false, 0.0f, 0.5f, 1.5f)]
public float _conditionFalse = 2;

[ColorIf(nameof(condition1), true, 1.0f, 0.5f, 0.5f)] // Next
[ColorIf(nameof(condition2), true, 0.0f, 0.5f, 1.5f)] // Top priority
public GameObject _conditionBoth;
```
![image](https://github.com/rito15/Unity-CAT/assets/42164422/1f4ae340-babf-4235-bd46-e11c56473533)

# Unity CAT
- Unity Custom ATtributes
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

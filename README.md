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

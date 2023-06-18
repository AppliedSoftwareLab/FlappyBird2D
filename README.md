# FlappyBird2D
- 2D FlappyBird 게임 구현 프로젝트입니다.
<br><br>


## Member
`FlappyBird2D` 프로젝트 멤버입니다.

- ChoiJiOne
- PROCORDER
- lym0217
- 2019603034
<br><br>


## Platform

`FlappyBird2D`는 `Windows`만 지원합니다.
<br><br>


## Requirements

`FlappyBird2D`를 유지보수 하기 위한 요구 사항은 다음과 같습니다.
- [git](https://git-scm.com/)
- [Visual Studio 2019 이상](https://visualstudio.microsoft.com/ko/)
- [python 3.x](https://www.python.org/downloads/)
<br><br>


## clone

`CMD`에 다음 명령어를 입력하면 `FlappyBird2D` 저장소의 복제본을 얻을 수 있습니다.
```
> git clone https://github.com/AppliedSoftwareLab/FlappyBird2D
```
<br><br>


## How to use `GenerateProjectFiles.bat`

`GenerateProjectFiles.bat`를 사용하기 위해서는 `FlappyBird2D` 폴더에서 `CMD`를 실행하고 다음 명령어를 입력합니다.
```
GenerateProjectFiles.bat <your-visual-studio>
```

만약, `Visual Studio 2019`를 사용하고 있다면 다음과 같이 입력합니다.
```
GenerateProjectFiles.bat vs2019
```

만약, `Visual Studio 2022`를 사용하고 있다면 다음과 같이 입력합니다.
```
GenerateProjectFiles.bat vs2022
```
<br><br>


## How to use `HotReload.bat`

`HotReload.bat`를 사용하기 위해서는 `FlappyBird2D` 폴더에서 `CMD`를 실행하고 다음 명령어를 입력합니다.
```
HotReload.bat <your-visual-studio>
```

만약, `Visual Studio 2019`를 사용하고 있다면 다음과 같이 입력합니다.
```
HotReload.bat vs2019
```

만약, `Visual Studio 2022`를 사용하고 있다면 다음과 같이 입력합니다.
```
HotReload.bat vs2022
```
<br><br>


## How to use `Build.bat`

> `Build.bat` 스크립트를 사용하기 위해서는 `msbuild.exe` 경로가 환경 변수에 등록되어야 합니다.

`Build.bat`를 사용하기 위해서는 `FlappyBird2D` 폴더에서 `CMD`를 실행하고 다음 명령어를 입력합니다.
```
Build.bat <visual-studio-version> <mode>
```

이 스크립트가 지원하는 mode는 `Debug`, `Release`, `Shipping`입니다. 각 모드 별 특징은 다음과 같습니다.

| mode | description |
|:---|:---|
| Debug |  빌드 과정에서 최적화를 하지 않고, 디버그 정보 파일(.pdb)을 생성합니다. |
| Release |  빌드 과정에서 최적화는 하지만 디버그 정보 파일(.pdb)을 생성합니다. |
| Shipping | 빌드 과정에서 컴파일러가 할 수 있는 모든 최적화를 수행합니다. | 

만약, `Visual Studio 2019`를 사용하고 있다면 다음과 같이 입력합니다.
```
Debug 모드일 경우...
> Build.bat vs2019 Debug

Release 모드일 경우...
> Build.bat vs2019 Release

Shipping 모드일 경우...
> Build.bat vs2019 Shipping
```

만약, `Visual Studio 2022`를 사용하고 있다면 다음과 같이 입력합니다.
```
Debug 모드일 경우...
> Build.bat vs2022 Debug

Release 모드일 경우...
> Build.bat vs2022 Release

Shipping 모드일 경우...
> Build.bat vs2022 Shipping
```
<br><br>



## Play

`FlappyBird2D`는 마우스 좌클릭으로 플레이할 수 있습니다.

<br><br>


## ToolSet

`FlappyBird2D` 개발을 위한 툴셋 링크는 아래와 같습니다.
- [DBViewer](https://github.com/AppliedSoftwareLab/DBViewer) : 로컬에 저장된 DB 파일을 시각화하기 위한 툴입니다.
- [DevTool](https://github.com/AppliedSoftwareLab/DevTool) : 게임 개발에 필요한 각종 배치 파일을 GUI 환경에서 사용하기 위한 툴입니다.

# ChangeBlindnessの研究

```mermaid
---
title: ネイティブアプリ
---
classDiagram
    GameLoop -- ChangeController
    ChangeController -- Changer
    ChangeController -- ChangeDB

    class GameLoop {
        <<ゲーム進行全般>>
        - int gameState
        - int buttonState
        - float time
    }
    class ChangeController {
        <<オブジェクト変化のタイミング指示役>>
        +void AddMe(GameObject obj)
    }
    class Changer{
        <<オブジェクト変化役>>
        + string name
        + int level
        + bool inFocus
        + bool saw
        +void MorphingChange()
        +void SwitchChange()
        +void ColorChange()
    }
    class ChangeDB{
        <<オブジェクト変化順序のデータベース>>
        + string patternName
        + int maxLevel
        + List<bool> changeMaterial
        + List<GameObject> objectes  
    }
```mermaid
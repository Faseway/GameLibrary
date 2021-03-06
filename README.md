﻿# GameLibrary

Open source game library for indie games. Its based on the Microsoft Xna Framework.

### Features

- Components
- Localization
- Logging (file logging included)
- Resource and file system
- Scene management
- Scripts (dynamic runtime code)
- Serialization
- Gui (Gooey) system

### Version

0.316.215 Pre-Alpha

### Tech

The base directory is organized as follows.

```
Base
├┬ Audio
│├─ Ambient
│├─ FX
│└─ Soundtrack
├─ Data
├┬ Lang
│├─ de
│└─ en
├─ Maps
├─ Scripts
├─ Specs
└─ Textures
```

###### Scripting

Our Game Library provides the ability to use scripts in the form of dynamic runtime code. We distinguish in `MissionScript`s and `CommandScript`s. Scripts are loaded once at application start. Changes to scripts at runtime have no impact on the running application.

While working with entities, we always use command scripts. You can easily create and execute them. To execute a command use

```csharp
Entity.ExecuteCommand("Attack", TargetEntity);
```

for example.

###### Gui System (Gooey)

Mouse events occur in the following order:

1. MouseEnter
2. MouseMove
3. MouseHover / MouseDown / MouseWheel
4. MouseUp
5. MouseLeave

### Development

Want to contribute? Great! Do it!

### License

The MIT License (MIT)

Copyright (c) 2015 Faseway

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

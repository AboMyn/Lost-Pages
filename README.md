# Lost Pages

**A first-person mystery exploration game developed for a Game Jam in Unity.**

**Languages:** [English](#english) | [Русский](#русский)

![Lost Pages — mysterious room](Screenshots/image%20(0).png)

## English

### About the game

*Lost Pages* is a first-person mystery game set in a dimly lit environment filled with books, notes, and strange objects. Explore the rooms, inspect pages, collect keys, and open doors. Your approach to progressing through the game determines which ending you reach.

### Gameplay features

- **First-person exploration:** mouse-look, walking, crouching, and footsteps.
- **Object interactions:** context-sensitive prompts for pages, keys, chests, and doors.
- **Readable pages:** inspect notes in a dedicated reading interface.
- **Keys and doors:** unlock doors with keys or progress through certain pages.
- **Interactive chests:** animated lids that reveal hidden pages.
- **Three endings:** good, bad, or mixed, based on whether you open doors using keys, pages, or both.
- **Atmosphere:** candlelit rooms, sound effects, screen fades, and horror-style ending sequences.

### Controls

| Action | Key |
| --- | --- |
| Move | WASD |
| Look around | Mouse |
| Crouch | Hold Left Ctrl |
| Read a page / collect a key | E |
| Close a page | E |
| Open a chest / interact with a door | Q |

### Endings

| Route | Ending |
| --- | --- |
| Open doors using only keys | Good ending |
| Open doors using only pages | Bad ending |
| Use both methods | Mixed ending |

### Technical details

- **Engine:** Unity
- **Language:** C#
- **Player movement:** `CharacterController`, gravity, and smooth crouching
- **Interaction system:** camera-based `Physics.RaycastAll` and contextual UI prompts
- **Game state:** `GameManager` tracks collected keys, read pages, and door-opening methods
- **Presentation:** TextMeshPro, UI transitions, audio cues, and coroutine-based endings

### Screenshots

**Main room**

![Candlelit mystery room](Screenshots/image%20(1).png)

**Reading and exploration**

![Notes on the desk and reading prompt](Screenshots/image%20(2).png)
![Key](Screenshots/image%20(3).png)

**Alchemy laboratory**

![Laboratory interior](Screenshots/image%20(4).png)

---

## Русский

### Об игре

*Lost Pages* — мистическая игра от первого лица, созданная для Game Jam на Unity. Игрок исследует полутёмные помещения, изучает записки, собирает ключи и открывает двери. Способ прохождения влияет на концовку.

### Игровые механики

- **Исследование от первого лица:** ходьба, обзор мышью, приседание и звуки шагов.
- **Взаимодействие с предметами:** подсказки при наведении на страницы, ключи, сундуки и двери.
- **Чтение страниц:** отдельный интерфейс для просмотра найденных записок.
- **Ключи и двери:** разные способы открытия дверей.
- **Сундуки:** анимация открытия и появление спрятанных страниц.
- **Три концовки:** хорошая, плохая и смешанная, зависящие от выбранного способа прохождения.
- **Атмосфера:** свечи, звуки, затемнение экрана и пугающие финальные сцены.

### Управление

| Действие | Клавиша |
| --- | --- |
| Передвижение | WASD |
| Обзор | Мышь |
| Присесть | Удерживать левый Ctrl |
| Читать страницу / подобрать ключ | E |
| Закрыть страницу | E |
| Открыть сундук / взаимодействовать с дверью | Q |

### Концовки

| Способ прохождения | Концовка |
| --- | --- |
| Открывать двери только ключами | Хорошая |
| Открывать двери только через страницы | Плохая |
| Использовать оба способа | Смешанная |

### Техническая реализация

- **Движок:** Unity
- **Язык:** C#
- **Передвижение:** `CharacterController`, гравитация, плавное приседание
- **Взаимодействие:** `Physics.RaycastAll` и контекстные подсказки
- **Логика игры:** `GameManager` отслеживает ключи, страницы и способы открытия дверей
- **Интерфейс и эффекты:** TextMeshPro, аудио, затемнение экрана и корутины для концовок

### Скриншоты

**Главная комната**

![Комната со свечами](Screenshots/image%20(1).png)

**Чтение и исследование**

![Записки на столе](Screenshots/image%20(2).png)

**Алхимическая лаборатория**

![Алхимическая лаборатория](Screenshots/image%20(4).png)

---

**Project status:** Game Jam project. Source code shared for portfolio and educational purposes.

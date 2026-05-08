# Proyecto Final Unity – Escape Room 3D

![Unity](https://img.shields.io/badge/Unity-2022%2B-black?style=flat&logo=unity)
![C#](https://img.shields.io/badge/C%23-Programming-blue?style=flat&logo=c-sharp)

Escape room en primera persona hecho en Unity para la materia de Desarrollo de Videojuegos en la UPB. El jugador queda encerrado en el campus y tiene 15 minutos para resolver una serie de minijuegos y conseguir el búho antes de que se acabe el tiempo.

## Cómo abrirlo

```bash
git clone https://github.com/ValeriaMartinezSoria/ProyectoFinalUnity.git
```

Abre el proyecto desde Unity Hub. Hace falta Unity 2022.3 LTS o superior. La primera importación tarda un rato porque vienen varios asset packs pesados incluidos en el repo.

Una vez abierto, abre `Assets/_Scenes/IntroScene.unity` y dale Play.

## Controles

| Acción      | Tecla              |
|-------------|--------------------|
| Mover       | W A S D            |
| Mirar       | Q                  |
| Saltar      | Espacio            |
| Sprint      | Shift (mantener)   |
| Interactuar | E o clic izquierdo |
| Pistas      | X                  |
| Pausa       | Esc                |

## Flujo del juego

Hay 5 escenas: `IntroScene` (video y narración tipo máquina de escribir), `PrincipalScene` (menú), `All` (la del juego, donde arranca el timer), y al final `WinScene` o `LoseScene` según si lograste salir a tiempo.

Dentro de la escena principal el orden de progresión es:

1. Encontrar las pistas en el aula y obtener el código para pasar a la cafetería.
2. Recoger las 10 anomalías repartidas por la cafetería.
3. Abrir la cerradura digital para ir a las oficinas de los jefes de carrera de FIA.
4. Ordenar los 5 objetos desordenados de la oficina.
5. Resolver el puzzle de cables (3 conexiones, mismo color).
6. Introducir el código de la cerradura para pasar al coliseo o centro de eventos.
7. Pasar los minijuegos de letras DTI y del letrero de la UPB.
8. Pasar a la sala de descanso donde colocamos un último código.
9. Encontrar el botón final cerca de la tarima y hacer clic al trofeo del búho para que se reproduzca el video del proyector.

## Los minijuegos

**Cerradura digital.** Teclado numérico 3D pegado a la puerta principal con un código de 4 dígitos.

**Anomalías.** Hay 10 objetos raros escondidos en el aula. Al clickearlos todos, una pared desaparece y se abre el siguiente espacio.

**Organizar oficina.** 5 objetos desordenados que hay que ir clickeando. Al terminar aparece el modelo de la oficina ya ordenada en su lugar.

**Cables.** Conectar nodos del mismo color (rojo, azul y amarillo) con un `LineRenderer` que dibuja la conexión animada. Son 3 conexiones para terminar.

**Letras DTI y UPB.** El primero es ordenar las letras D-T-I (la sigla de la sala de tecnología). El segundo es un estilo de Simon dice con las letras U-P-B: la secuencia empieza en 2 letras y crece hasta 4. Si fallas, repites el nivel.

## NPCs

Los NPCs heredan todos de la clase abstracta `NpcBase`:

| NPC              | Qué hace                                                            |
|------------------|---------------------------------------------------------------------|
| `Helper`         | Da diálogos de misión y gira hacia el jugador con `Quaternion.Slerp`|
| `NpcDance`       | Camina, calienta y baila (máquina de estados)                       |
| `NpcHurried`     | Camina apurado entre puntos hablando por teléfono                   |
| `NpcTalkAndWalk` | Pasea y se detiene a conversar cuando se cruza con otro NPC         |

## Estructura del código

El código nuestro está en `Assets/_Scripts`:

- `Player/` — `Player`, `PlayerLook`, `HighlightOnLook`, `Focus`
- `Flujo/` — `GameTimer`, `IntroVideo`, `IntroHistoria`, mensajes (`MensajeInicial`, `MensajeMisionCafe`, `MensajeMisionColiseo`), botones de menú (`JugarAhora`, `VolverAJugar`), `Pausemanager`, `FinalButton`, `TrophyInteract`, `FinalSequenceManager`
- `NPCs/` — `NpcBase`, `Helper`, `NpcDance`, `NpcHurried`, `NpcTalkAndWalk`
- `JuegoDTI/` — `OrderLettersGame`, `LetterButton`, `OrderLettersTrigger`
- `JuegoUPB/` — `MemoryGame`, `MemoryGameTrigger`, `MemoryLetter`
- En la raíz de `_Scripts/` están las interacciones del aula: `AnomalyManager`, `AnomalyInteract`, `CodeLock3D`, `KeypadButton`, `InteractiveDoor`, `Clue`, `EscritorioInteractivo`, `OrganizeOfficeManager`, `OrganizeObjectClick`, `CableManager`, `CableNode`

El resto de carpetas dentro de `Assets/` (Brick Project Studio, Denys Almaral, Food Pack-Demo, LeartesStudios, LowPolyOfficeProps_LITE, school y otros) son asset packs de terceros, no código nuestro.

## Tecnologías de Unity que usamos

Input System nuevo, Rigidbody para la física del jugador, NavMesh y NavMeshAgent para los NPCs, Animator con parámetros como `Speed`, `IsTalking`, `IsDancing` o `IsPhoneTalking`, TextMesh Pro para los textos UI y 3D, VideoPlayer para las cinemáticas, LineRenderer para los cables, SceneManager para cambiar de escena, raycasts para detectar lo que mira el jugador, `Quaternion.Slerp` para abrir puertas con suavidad, y URP para iluminación y materiales.

En cuanto al código en C#, los managers de los puzzles funcionan como singletons (`AnomalyManager`, `CableManager`, `Pausemanager`, `OrganizeOfficeManager`, `FinalSequenceManager`), los NPCs comparten una clase abstracta `NpcBase`, hay máquinas de estados simples en `NpcDance` y `NpcHurried`, y se usan corrutinas para el efecto de tipeo, las animaciones de los cables y la cinemática final.

## Audio

Todos los audios están en `Assets/_Audio`: música de fondo y de tensión, sonidos del teclado y de acierto/error en los puzzles, audios de los NPCs (bailes, llamadas, diálogos) y los efectos al recoger objetos o abrir puertas.

## Créditos

Proyecto final de la **Universidad Privada Boliviana (UPB)** — materia de Desarrollo de Videojuegos / IDE.

Los asset packs de terceros incluidos en el repo (Brick Project Studio, Denys Almaral, Food Pack-Demo, Horse Statue, Leartes Studios, LowPoly Office Props LITE, nappin, school, TextMesh Pro de Unity) mantienen sus licencias originales.


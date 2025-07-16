Noor Daood Boss Level .

Links:
Gameplay Video: 
Web Build: 
CodeReview Video:

Instructions - KeyBoard:
Shoot and Active the lock: Press the Space button.
Moveing : the left/right/up/down or WASD buttons.

Planning:
UML diagram:
+------------------------+              +------------------------+
|      LTDPlayer         |◄─┐        ┌─►|   LTDPlayerHealthUI    |
| - Movement/Input       |  │        │  | - health               |
| - Wand.Fire()          |  │        │  | - OnHealthChanged()    |
| - Animator             |  │        │  | - OnPlayerDied()       |
+------------------------+  │        │  +------------------------+
         ▲                 │        │              │
         │                 │        │              ▼
         │                 │        │   +-----------------------+
         │       Fires     │        │   |    LTDGameOverUI      |
         │                 │        │   | - Show death/win UI   |
         │                 │        │   | - Pause game          |
         │                 │        │   +-----------------------+
         │                 │        │
+------------------+       │        │
|    LTDWand       |───────┘        │
| - Instantiates   |                │
|   LTDSpell       |                │
+------------------+                │
         │                         OnHit (fire/devil) invokes:
         │                                LTDEvents.DecreasePlayerHealth
         ▼
+------------------------+
|     LTDSpell           |
| - Flies toward enemy   |
| - Destroys on hit      |
+------------------------+


+-----------------------+        +--------------------------+
|      LTDBoss          |        |   LTDSmallDevils        |
| - Shoots LTDFire      |        | - Moves toward player   |
| - HandleShooting()    |        | - Destroys on contact   |
+-----------------------+        +--------------------------+
         │                                │
         ▼                                ▼
+-------------------------+      +----------------------------+
|       LTDFire           |      |   OnTriggerEnter2D (Player)|
| - Moves via velocity    |      |   → DecreasePlayerHealth   |
| - OnTriggerEnter2D      |      +----------------------------+
|   → DecreasePlayerHealth|
+-------------------------+


+--------------------------+
|       LTDLockingSigil    |
| - Activates on Space     |
| - Calls DecreaseDevilHealth
+--------------------------+
         │
         ▼
+------------------------+
|   LTDBossHealth        |
| - On DecreaseHealth()  |
| - If <= 0              |
|   → DevilDies event    |
+------------------------+






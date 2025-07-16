# Noor Daood Boss Level

## 🔗 Links

- **Gameplay Video:** 
- **Itch.io Game:** 
- **CodeReview Video:** 

---

## 📌 Planning

**Diagram:** 📄 [Boss Level UML Diagram (PDF)](https://github.com/Huji-Bezalel-Advanced-Unity/Noor-Daoud-Boss-Level/raw/main/BossLevel_UML_Diagram.pdf)



---

## 🎮 Controls (Keyboard)

- **Shoot / Activate Lock:** `Spacebar`  
- **Movement:** Arrow Keys or `W / A / S / D`

---

## 🧙 Game Concept

### 📖 Story
You play as a wizard trapped in the Devil’s Room.  
Your goal is to lock the boss by activating sigils scattered around, all while surviving attacks from the devil and his minions.

---

## 👹 Enemies

### 🔥 Boss (The Devil)

- **Appearance:** A large devil located at the center of the room.  
- **Behavior:** Automatically shoots fireballs in all directions every 3 seconds.  
- **Invulnerability:** Cannot be damaged by player attacks.  
- **Weakness:** Temporarily stops attacking when the player enters a safe zone.

### 😈 Small Devils

- **Appearance:** Smaller devil-like creatures.  
- **Spawn:** Random directions and intervals.  
- **Attack:** Damage the player on contact and then disappear.  
- **Weakness:** Can be destroyed by the player's magic (spells).

---

## 🧝 Player Mechanics

- **Movement:** Free movement across the arena using keyboard.  
- **Attack:** Fires projectiles (spells) to eliminate small devils.  
- **Defense:** Collect a shield power-up for temporary protection.  
- **Escape Mechanism:** Entering a safe zone stops the boss's fire attack for 3 seconds.  
- **Lock Activation:** Press `Spacebar` at a locking sigil to damage the boss.

---

## 🧩 Interactive Elements

### ⚡ Power-Ups

- **Types:**
  - Increase Speed  
  - Increase Health  
  - Shield (temporary invincibility)
- **Acquisition:** Picked up when the player collides with them.

### 🛡️ Safe Zones

- **Appearance:** Glowing circles around the room.  
- **Effect:** Entering one delays the boss's attack for 3 seconds.
- **Usage Limit:** Each safe zone works only once per game and becomes inactive after activation.

### 🔒 Locking Sigils

- **Appearance:** Special symbols around the arena.  
- **Mechanic:** Activated with `Spacebar` to reduce the boss's health.

---

## 🧭 Level Design

- **Environment:** An enclosed battle arena.  
- **Layout:** Strategically placed elements (locks, safe zones, power-ups) to encourage movement and planning.  
- **Objective:** Lock and defeat the boss while surviving waves of small devils.

---

## 🔊 Audio & 🎨 Visuals

### 🎵 Sound

- **Music:** Background boss theme  
- **Effects:**
  - Power-up collection  
  - Spell casting  
  - Lock activation  

### 🖥️ UI

- **Indicators:** Health bar, death/win screens  
- **Visual Feedback:** Screen cues for power-ups, lock activation, and damage

### 🎨 Art & Assets

- Player character  
- Devils (boss + small)  
- Fireballs  
- Safe zones  
- Power-ups  
- Lock sigils  
- Room background  
- Spell effects

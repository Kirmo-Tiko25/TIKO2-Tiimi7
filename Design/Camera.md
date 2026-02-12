# Camera

## Our choice for camera implementation

### Static camera

All scenes are pictured using this.

**Pros**
+ Easiest to implement
+ Player can make predictions on flight path and objects to move the most

**Cons**
- Recognizing the main character can be more difficult than in other options

## Different camera implementation methods

### Main character following camera

**Pros**
- Map seems to larger, changeable background 
- clear who is the main character

**Cons**
- Crashing into sides can be disorienting
- Making choices about which object to move around the map is limited.

### First person camera

**Cons**
- very disorienting 
- difficult to implement

### Third person camera

**Cons**
- same problems as before

### Look ahead camera

**Pros**
- Gives a little more extra time for decisions than Main character following camera

**Cons**
- Same negatives as Main character following camera

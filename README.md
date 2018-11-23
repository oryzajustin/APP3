# Justin Koh
## Game Engine Development

## Features of Gameplay
- First Person Perspective
- Flashlight for increased visibility toggled with the F key, or left click
- Can hide in Bushes scattered across the map
- Maze spawns a single exit at random position
- 3 skeletons that traverse and chase the player upon detection
- Player can sprint by holding shift
- Player can peek using Q and E keys
- Flashing light at a monster/standing close in front of it triggers a chase
- Player scores can be seen at the Main Menu
- There is no escape from the game once it is started, you must finish or die trying
- End game screens
- Skeletons have waypoints that they navigate to, switched by use of a waypoint timer
- Nav mesh is used to help the AI traverse the terrain
- The player cannot exit the map, by indication of tree lines
- Fog is added to decrease player view distance (alleviated by use of the flashlight)

## Known Issues
- Player can get stuck on invisible walls, due to overfitted collision boxes
- Monster animation controller sometimes shows weird behavior
- Monster yell audio clip sometimes does not work

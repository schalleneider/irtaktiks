# irtaktiks
> The IRTaktiks is a tactical rpg game for multitouch screens developed using Microsoft XNA 2.0 and the touch recognition library Touchlib 2.0.

It's playable by two players who can control at least eight combat units. Each one of these units have its owns attributes such as level, class, strength, agility, dexterity, vitality and intelligence. Each attribute influences the gameplay, for example, changing the movement area, the magic damage, defense, quantity of life and mana points, hit and flee percentage, etc...

![](/docs/images/game.png)

## Videos

* Multitouch Table Test: http://www.youtube.com/watch?v=U4vC09uAxlU
* Game Beta: http://www.youtube.com/watch?v=8ucz5iruWUQ
* Game v1.0: http://www.youtube.com/watch?v=0D8NuZd00gA
* SBGames 2008: http://www.youtube.com/watch?v=yA6CWLzHvgk

## Monography

* PDF Version - 4.5Mb: irtaktiks.zip

## Multitouch Screen

The multitouch screen is composed by a acrylic table based on the Frustrated Total Internal Reflection (FTIR) principle. The acrylic have 1.20m x 1.60m and uses Polyethylene as light diffuser. In order to recognize the touches, there are a modified Microsoft Life Cam VX6000 that can only see the lights on infrared spectrum. The library TouchLib 2.0 uses the webcam to recognize the touches, process its position and movement and send these informations to the game, that updates itself. The game is projected over the multitouch table screen.

![](/docs/images/architecture.png)

## Game

The game was developed using the Microsoft XNA 2.0. The communication between the TouchLib 2.0 and the game is done using the OSC protocol over the TUIO protocol.

The objective of the game is defeat the other player by killing it's combat units. To achieve this, the player must use its own units and a combination of its status, skills, position and strategy. Each player can have at least eight units. Each one of these units can be configurable by the player before the game starts. There are six classes to choose: Knight, Paladin, Assassin, Monk, Wizard, Priest; and for each class, four pallets to configure the visual of the combat unit.

The combat unit's implemented attributes are: HP, MP, Level, Class, Skills, Attack, Defense, Magic Attack, Magic Deffense ,Hit, Flee, Delay, Strength, Agility, Vitality, Intelligence and Dexterity. The choice in the class changes the values of these attributes, for example: One Paladin always have more HP than a Assassin; even if both have the same value of vitality, and a Knight have skills based on physical attack and a Wizard, skills based on magical attack.

![](/docs/images/unit-config.png)

The touch is used to interact with the game. Actions such select the units, choose the skills and its targets; and move the unit over the map can be done by finger press and finger movements, like drag-and-drop.

![](/docs/images/game-play.png)

## Useful Links

* FTIR Principle: http://www.cs.nyu.edu/~jhan/ftirsense/
* TouchLib 2.0: http://www.nuigroup.com/touchlib
* TUIO Protocol: http://tuio.lfsaw.de/
* OSC Protocol: http://opensoundcontrol.org/
* XNA Creators Club Online: http://creators.xna.com/

## Contact

Willians Schallemberger Schneider – [@schalleneider](https://twitter.com/schalleneider)

Distributed under the GNU 2.0 license. See ``LICENSE`` for more information.

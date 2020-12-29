### Research.Unity.SquadFormation_CoordinatedMovement
This project was made for research purposes. It includes exploring ways of military squad formations and implementing them in Unity

## Squad formation and coordinated movement in games.

One of the main and coolest features that RTS and squad-based games can offer is controlling multiple units at ones. But sometimes controls are not deep enough and leave the player with just a bunch of AI moving closer to the player/leader if the distance between them is too big. 

On one hand, this is not a big deal, the player will handle the problems themselves without any difficulties. An example for that can be iconic series of games: Half-Life. Despite having an ability to organize a squad of 4 units + a leader (Half-Life 2 and episodes), many players prefer to fight alone. Friendly NPCs have some sort of AI but might be chaotic and useless at times. 

![HL2](https://user-images.githubusercontent.com/76696557/103245402-582d8b80-4960-11eb-9923-2c25362abfcd.gif)

*Heavily modded Half-life 2 footage with custom AI. Source: https://www.youtube.com/watch?v=8nKy6YEsfRI&ab_channel=k0noa*

Another more recent example could be Serious Sam 4, which introduces a brand new but hopelessly outdated NPC-companion system. In a game with such a vast amount of enemies versus the player, it could have been awesome to have a well-coordinated squad that can effectively deal with literal waves of monsters, especially when the “Legion” system, that allows thousands of enemies to be active on the level, was used. Instead we get bots which act as if they are random players playing with you in co-op mode, shooting down closest enemies and keeping up with you. 

![SS4](https://user-images.githubusercontent.com/76696557/103245420-667ba780-4960-11eb-83b8-8e318556f9d8.gif)

*Serious Sam 4 gameplay. Source (Russian): https://www.youtube.com/watch?v=H-Ra4bK7S2E&t=375s&ab_channel=%D0%91%D0%A3%D0%9B%D0%94%D0%96%D0%90%D0%A2%D1%8C*

Even the indie side-project “Serious Sam: The Random Encounter” makes more use of companions allowing to choose which guns and how each one uses them. Moreover, in this game the player controls a small squad of characters.

![SSRE](https://user-images.githubusercontent.com/76696557/103245422-67acd480-4960-11eb-87b1-3f6ecb033c59.gif)

*Serious Sam: The Random Encounter gameplay trailer. Source: https://www.youtube.com/watch?v=7MoS4oZHAcU&ab_channel=DevolverDigital*

On the other hand, many projects and an absolute majority of RTS games must have a proper coordinated movement of units. Some popular mobile games about fantasy wars like Clash of Clans or Lords mobile utilize or could utilize a proper squad formation system for more interesting and spectacular gameplay.

![LM](https://user-images.githubusercontent.com/76696557/103245413-61b6f380-4960-11eb-9ad3-2aac81753b7a.gif)

*Lords mobile gameplay. Source: https://www.youtube.com/watch?v=jf-oJrZY5-U&t=1042s&ab_channel=SergiuHellDragoonHQ*

Today games which involve controling multiple units should have a simple but working squad formation system. It is always fun to see how your loyal virtual soldiers perfectly execute commands and arrange stratigically effective formations. This looks especially awesome from top-down view.

So how are the squad formations built? What formations already exist and are used in both games and life? How is the movement of units organized? Let's find out!

## Squad elements and atributes.

Starting from zero, a squad consists of **units** and each unit has its own position in world. Units can also move according to particular rules. Units can be gathered in **groups** which in most cases makes them share the direction of movement, so the group can move all together. **Formation** is a group with special rules and position for every unit. Those rules are usually depending on unit's number or its being odd or even. In most cases formations have a **leader** who can either be an observer or an equal part of the formation just like his fellow units.

Now when we are in a formations there are two main ways of units movement: **units move at the same speed and in the same direction** and **units take the same path as the leader**. The first variant is the most common and it implies that units keep their relative to the leader positions.

![GSMove1](https://www.gamasutra.com/features/19990129/02move01.jpg)

*Units moving at the same speed. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

The "same path" variant means that each unit will follow the next one after it and the last one follows the leader.

![GSMove2](https://www.gamasutra.com/features/19990129/02move02.jpg)

*Units form a line and follow the leader. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

Finally there are 2 more global variables that affect the whole formation: **orientation of formation** and **unit positions scale**.
In this case, orientation means where does the formation face and what affects it. Usually oreintaion of the whole formation is dictated by orientation of the leader, so all units should keep that in mind.

![GSMove3](https://www.gamasutra.com/features/19990129/02move06.jpg)

*Recalculating formation's orientation. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

Unit positions scale is used to determine how big are intervals between neighboring units thus how much of an area the whole squad covers.

![GSMove3](https://www.gamasutra.com/features/19990129/02move07.jpg)

*Scaling unit positions. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

This were the basic atributes and elements of a squad. There are more advanced adjustments and features but the ones described in this article are enough to make an interesting looking squad that can build different formations.

## Types of squad formations

There are a lot of ways a squad can be formed in. Let's take a look at formations that are used in games and are even practiced in modern infantry action. Each of them has specific purpose and should be used or changed to according to the situation. This is decided by the leader (player perhaps?) on the go. In this article roles of units are ignored or counted as equal.

**Squad column**

![image](https://user-images.githubusercontent.com/76696557/103248911-18b96c00-496d-11eb-9620-002c07013a77.png)

*Squad column (WWII U.S. Army Infantry Rifle Squad). Source: https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

This formation is one of the simpliest but it also has specific adjustments:

1) The column is able to move in both **same speed and direction** and **same path** modes.

2) Besides unit positions scale the column also has the "openness" parameter. It determines how far left and right can units be scattered.

3) If formation's orientaion is based on orientation of the leader, the whole squad has to regroup each time the leader rotates in order to stay behind the leader. 

![image](https://user-images.githubusercontent.com/76696557/103249179-4bb02f80-496e-11eb-9444-a667015433ce.png)

*More "open" squad column (WWII U.S. Army Infantry Rifle Squad). Source:https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

**Skirmish line** (aka line of skirmishers, aka line)

![image](https://user-images.githubusercontent.com/76696557/103249484-c463bb80-496f-11eb-9694-75f287824405.png)

*Skirmish line (WWII U.S. Army Infantry Rifle Squad). Source: https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

The skirmish line is the first simple formation that requires a balanced unit distribution to keep the symetry of the formation. The leader of formation acts as the 0th unit, so every next unit gets on the leader's right or left side according to parity of unit's order number. There are variants of this formation where the leader stays behind the squad in order to see if all units follow the formation. This formation is often used in armies at parades and trainings.

**Sqaud wedge**

![image](https://user-images.githubusercontent.com/76696557/103250084-84ea9e80-4972-11eb-8c18-f78aa6551e0b.png)

*Squad wedge (WWII U.S. Army Infantry Rifle Squad). Source: https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

Squad wedge is the first formation that utilises two dimensions, while previous formations were built in 1D. In this situation the leader can either be at the top of the wedge or can stay behind the squad to control all the units. 

**Square**

![image](https://user-images.githubusercontent.com/76696557/103250447-23c3ca80-4974-11eb-9f2a-b6eab885e438.png)

*Square formation in the game Cossacks 3*

Squad square is an outdated defensive formation that is either used in situiations with a big number of units or in order to protect the core of the squad which often is the leader. This formation was often used in situations when the squad got caught in the middle of the battlfield so the enemy could attack from any side. This formation requires symetry in 2 dimensions, while previous formations should be mirrored only in one dimension. There are also several approaches to building this formation and this can depend on squad's current prorities.

![image](https://user-images.githubusercontent.com/76696557/103250629-05aa9a00-4975-11eb-8d68-f5d4d197d6b0.png)

*Square formation with leader being core of the squad in the game Cossacks 3*


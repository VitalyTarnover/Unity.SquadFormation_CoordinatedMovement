### Research.Unity.SquadFormation_CoordinatedMovement
This project was made for research purposes. It includes exploring ways of military squad formations and implementing them in Unity

## Squad formation and coordinated movement in games

One of the main and coolest features that RTS and squad-based games can offer is controlling multiple units at ones. But sometimes controls are not deep enough and leave the player with just a bunch of AI moving closer to the player/leader if the distance between them is too big. 

On one hand, this is not a big deal, the player will handle the problems themselves without any difficulties. An example for that can be iconic series of games: Half-Life. Despite having an ability to organize a squad of 4 units + a leader (Half-Life 2 and episodes), many players prefer to fight alone. Friendly NPCs have some sort of AI but might be chaotic and useless at times. 

![HL2](https://user-images.githubusercontent.com/76696557/103245402-582d8b80-4960-11eb-9923-2c25362abfcd.gif)

*Heavily modded Half-life 2 footage with custom AI. Source: https://www.youtube.com/watch?v=8nKy6YEsfRI&ab_channel=k0noa*

Another more recent example could be Serious Sam 4, which introduces a brand new but hopelessly outdated NPC-companion system. In a game with such a vast amount of enemies versus the player, it could have been awesome to have a well-coordinated squad that can effectively deal with literal waves of monsters, especially when the “Legion” system, that allows thousands of enemies to be active on the level, was used. Instead we get bots which act as if they are random players playing with you in co-op mode, shooting down the closest enemies and keeping up with you. 

![SS4](https://user-images.githubusercontent.com/76696557/103245420-667ba780-4960-11eb-83b8-8e318556f9d8.gif)

*Serious Sam 4 gameplay. Source (Russian): https://www.youtube.com/watch?v=H-Ra4bK7S2E&t=375s&ab_channel=%D0%91%D0%A3%D0%9B%D0%94%D0%96%D0%90%D0%A2%D1%8C*

Even the indie side-project “Serious Sam: The Random Encounter” makes more use of companions allowing to choose which guns and how each one uses them. Moreover, in this game the player controls a small squad of characters.

![SSRE](https://user-images.githubusercontent.com/76696557/103245422-67acd480-4960-11eb-87b1-3f6ecb033c59.gif)

*Serious Sam: The Random Encounter gameplay trailer. Source: https://www.youtube.com/watch?v=7MoS4oZHAcU&ab_channel=DevolverDigital*

On the other hand, many projects and an absolute majority of RTS games must have a proper coordinated movement of units. Some popular mobile games about fantasy wars like Clash of Clans or Lords mobile utilize or could utilize a proper squad formation system for more interesting and spectacular gameplay.

![LM](https://user-images.githubusercontent.com/76696557/103245413-61b6f380-4960-11eb-9ad3-2aac81753b7a.gif)

*Lords mobile gameplay. Source: https://www.youtube.com/watch?v=jf-oJrZY5-U&t=1042s&ab_channel=SergiuHellDragoonHQ*

Today games which involve controlling multiple units should have a simple but working squad formation system. It is always fun to see how your loyal virtual soldiers perfectly execute commands and arrange strategically effective formations. This looks especially awesome from top-down view.

So how are the squad formations built? What formations already exist and are used in both games and life? How is the movement of units organized? Let's find out!

## Squad elements and attributes

Starting from zero, a squad consists of **units** and each unit has its own position in world. Units can also move according to particular rules. Units can be gathered in **groups** which in most cases makes them share the direction of movement, so the group can move all together. **Formation** is a group with special rules and position for every unit. Those rules are usually depending on unit's number or its being odd or even. In most cases formations have a **leader** who can either be an observer or an equal part of the formation just like his fellow units.

Now when we are in a formation there are two main ways of units movement: **units move at the same speed and in the same direction** and **units take the same path as the leader**. The first variant is the most common and it implies that units keep their relative to the leader positions.

![GSMove1](https://www.gamasutra.com/features/19990129/02move01.jpg)

*Units moving at the same speed. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

The "same path" variant means that each unit will follow the next one after it and the last one follows the leader.

![GSMove2](https://www.gamasutra.com/features/19990129/02move02.jpg)

*Units form a line and follow the leader. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

Finally there are 2 more global variables that affect the whole formation: **orientation of formation** and **unit positions scale**.
In this case, orientation means where does the formation face and what affects it. Usually orientation of the whole formation is dictated by orientation of the leader, so all units should keep that in mind.

![GSMove3](https://www.gamasutra.com/features/19990129/02move06.jpg)

*Recalculating formation's orientation. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

Unit positions scale is used to determine how big are intervals between neighboring units thus how much of an area the whole squad covers.

![GSMove3](https://www.gamasutra.com/features/19990129/02move07.jpg)

*Scaling unit positions. Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1*

This were the basic attributes and elements of a squad. There are more advanced adjustments and features but the ones described in this article are enough to make an interesting looking squad that can build different formations.

## Types of squad formations

There are a lot of ways a squad can be formed in. Let's take a look at formations that are used in games and are even practiced in modern infantry action. Each of them has specific purpose and should be used or changed to according to the situation. This is decided by the leader (player perhaps?) on the go. In this article roles of units are ignored or counted as equal.

**Squad column**

![image](https://user-images.githubusercontent.com/76696557/103248911-18b96c00-496d-11eb-9620-002c07013a77.png)

*Squad column (WWII U.S. Army Infantry Rifle Squad). Source: https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

This formation is one of the simplest but it also has specific adjustments:

1) The column is able to move in both **same speed and direction** and **same path** modes.

2) Besides unit positions scale the column also has the "openness" parameter. It determines how far left and right can units be scattered.

3) If formation's orientation is based on orientation of the leader, the whole squad has to regroup each time the leader rotates in order to stay behind the leader. 

![image](https://user-images.githubusercontent.com/76696557/103249179-4bb02f80-496e-11eb-9444-a667015433ce.png)

*More "open" squad column (WWII U.S. Army Infantry Rifle Squad). Source:https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

**Skirmish line** (aka line of skirmishers, aka line)

![image](https://user-images.githubusercontent.com/76696557/103249484-c463bb80-496f-11eb-9694-75f287824405.png)

*Skirmish line (WWII U.S. Army Infantry Rifle Squad). Source: https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

The skirmish line is the first simple formation that requires a balanced unit distribution to keep the symmetry of the formation. The leader of formation acts as the 0th unit, so every next unit gets on the leader's right or left side according to parity of unit's order number. There are variants of this formation where the leader stays behind the squad in order to see if all units follow the formation. This formation is often used in armies at parades and trainings.

**Squad wedge**

![image](https://user-images.githubusercontent.com/76696557/103250084-84ea9e80-4972-11eb-8c18-f78aa6551e0b.png)

*Squad wedge (WWII U.S. Army Infantry Rifle Squad). Source: https://www.youtube.com/watch?v=hGt4BFuKV4A&feature=emab_title&ab_channel=G.I.HistoryHandbook*

Squad wedge is the first formation that utilises two dimensions, while previous formations were built in 1D. In this situation the leader can either be at the top of the wedge or can stay behind the squad to control all the units. 

**Square**

![image](https://user-images.githubusercontent.com/76696557/103250447-23c3ca80-4974-11eb-9f2a-b6eab885e438.png)

*Square formation in the game Cossacks 3*

Squad square is an outdated defensive formation that is either used in situations with a big number of units or in order to protect the core of the squad which often is the leader. This formation was often used in situations when the squad got caught in the middle of the battlefield so the enemy could attack from any side. This formation requires symetry in 2 dimensions, while previous formations should be mirrored only in one dimension. There are also several approaches to building this formation and this can depend on squad's current priorities.

![image](https://user-images.githubusercontent.com/76696557/103250629-05aa9a00-4975-11eb-8d68-f5d4d197d6b0.png)

*Square formation with leader being core of the squad in the game Cossacks 3*

## Implementation

To implement all the theory presented in this article I have created a project in Unity, made a simple "flying" player pawn with a "capture zone" and units that can be "captured", which means they join player's squad. Instead of teleporting to their assigned positions captured units lerp towards their desired positions (called sockets in the code) in the squad.

```C#
public void AddAgentToSquad(UnitMovement unit)
    {
        unit.isInSquad = true;
        unitList.Add(unit);

        GameObject newSocket = Instantiate(socket);

        //bodyParent decides if the unit follows core's orientation or not
        if (bodyParent) newSocket.transform.parent = body.transform;
        else newSocket.transform.parent = transform;

        socketList.Add(newSocket);

        //update formation to recalculate positions for the whole squad
        switch (currentFormation)
        {
            case formation.skirmishLine:
                UpdateLineSockets();
                break;
            case formation.column:
                UpdateColumnSockets();
                break;
            case formation.snakeColumn:
                break;
            case formation.wedge:
                UpdateWedgeSockets();
                break;
            case formation.square:
                UpdateSquareSockets();
                break;
        }        

    }
   
```
    
*Code snippet for adding a new unit to player's squad*



![image](https://user-images.githubusercontent.com/76696557/103293397-2401ab80-49f0-11eb-9c8e-487955a3ddc0.png)

*Player pawn, leader and core of the squad in Unity*

At the moment the player is able to capture units so they join player's squad and give orders to the squad to make it regroup (change formation and formation modes), change position scale and width of the formation, switch movement modes and follow core orientation.

![UnitySquad1](https://user-images.githubusercontent.com/76696557/103294526-501e2c00-49f2-11eb-810d-123cb9e711b1.gif)

*Demonstartion of basic formations in the Unity research project*

Let's take a closer look at what the squad can do and how it works.

**Squad column**

In this formation units are added to the end of the column. 

```C#
    private void UpdateColumnSockets()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            if (i == 0) socketList[0].transform.localPosition = new Vector3(columnWidth, formationHeight, -formationScale);
            else socketList[i].transform.localPosition = new Vector3(columnWidth * (1 - (i % 2 * 2)), formationHeight, (socketList[i - 1].transform.localPosition.z - formationScale));
        }
    }
 ```
 *Code snippet for updating positions of units in squad column. First unit's position is based on leader's position and every next unit is positioned according to the previous unit's position. Z-position describes unit's position scale and X-position - width of the column which is affected by parity of unit's order number*

As described in theory part, the squad in this formation can move in both "same speed and direction" and "same path" modes.

![UnitySquad2](https://user-images.githubusercontent.com/76696557/103295179-b9526f00-49f3-11eb-8388-9546178d0ebc.gif)

*Changing movement modes while in squad column formation*

The squad can follow orientation of the leader, change unit positions scale and openness of the column.

![UnitySquad3](https://user-images.githubusercontent.com/76696557/103297498-bad26600-49f8-11eb-9904-0e3c944e5723.gif)

*Changing squad orientation, scaling unit positions and width of the column*

**Skirmish line**

This formation uses parity of unit's order number to decide whether the new unit should be added to the right or to the left side. In my first attempt I used 2 separate arrays of sockets (one for odd unit positions and one for even) but later on I switched to 1 array and simplified most of the calculations.

```C#
private void UpdateLineSockets()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            socketList[i].transform.localPosition = new Vector3(( formationScale * (i / 2 + 1) * (1 - (i % 2 * 2)) ), formationHeight, 0);
        }
    }
 ```
 
 *Code snippet for updating positions of units in skirmish line. X-position is checked for parity to decide how far away from the center a unit should be and on which side it should be*
 
 The squad in skirmish line formation is able to follow leader's orientation and scale unit positions.

![UnitySquad4](https://user-images.githubusercontent.com/76696557/103300097-63cf8f80-49fe-11eb-9bad-7293aaf6248f.gif)

*Changing formation's orientation and scaling unit positions.*

**Squad wedge**

As mentioned before, the leader in this formation can be either at the top of the wedge or behind it. I decided to stick to leader's being behind the wedge. This formation is also interesting since the leader is not a part of formation pattern, so the calculations should be a little shifted.

```C#
    private void UpdateWedgeSockets()
    {
        int zOrderMult = 0;

        for (int i = 0; i < unitList.Count; i++)
        {
            switch (currentWedgeMode)
            {
                case wedgeMode.defaultWedge:
                    zOrderMult = (unitList.Count) - i - (i % 2);
                    break;
                case wedgeMode.protectiveWedge:
                    zOrderMult = (unitList.Count / 2) - (i - 1) - (i % 2);// -1 moves whole formation forward and if there is only 1 unit - it will not take 0.0 position
                    break;
                case wedgeMode.wideWedge:
                    zOrderMult = (unitList.Count / 2 - ((i + 1) / 2 - 1));// -1 moves whole formation forward and if there is only 1 unit - it will not take 0.0 position
                    break;
                default:
                    break;
            }

             socketList[i].transform.localPosition = new Vector3( (i + (i % 2)) * formationScale * (1 - (i % 2 * 2)), formationHeight, formationScale * zOrderMult );

        }
    }
    
   ```
   
   *Snippet of code for updating positions of units in squad wedge. X-position is different compared to the previous formations since the leader is not taking part in forming formation's pattern. Z-position has 3 different variants so there are 3 sub-formations based on squad wedge* 

Squad wedge made me experiment a little with variables and in the end I came up with 3 modifications of this formation: default (the leader stays at the base of the wedge, left and right sides of wedge form 90 degrees angle), protective (the wedge has the same parameters as default but the leader is kept inside the wedge) and wide (the leader is back at the base of wedge but the left and right sides now form an obtuse angle). As all the other formations, units in squad wedge are capable of changing unit positions scale and formation's orientation.

![UnitySquad5](https://user-images.githubusercontent.com/76696557/103302858-e6f3e400-4a04-11eb-8eec-9c9e9cf2d0bf.gif)

*Changing unit positions scale, types of squad wedge formation and formation's orientation*

**Square**

The most complex formation so far. It takes a flexible counter based on rows and columns to determine how many units should each side have based on the size of the square or on how many layers of units the current number of units can make. After completing each layer the counter is reset and its minimum and maximum values are increased. There is also a tricky while-loop with condition that makes the update function ignore positions that have already been filled.

```C#
private void UpdateSquareSockets()
    {
        int layerNumber = 1;
        int j = -1;
        int k = -1;

        for (int i = 0; i < unitList.Count; i++)
        {
            while (Mathf.Abs(j) != layerNumber && Mathf.Abs(k) != layerNumber)
            {
                j++;

                if (j == layerNumber + 1)
                {
                    k++;
                    j = -layerNumber;
                }
            }        

            socketList[i].transform.localPosition = new Vector3(formationScale * j, formationHeight, formationScale * k);

            j++;

            if (j == layerNumber + 1)
            {
                k++;
                j = -layerNumber;
            }

            if (k == layerNumber + 1)
            {
                layerNumber++;
                j = -layerNumber;
                k = -layerNumber;
            }

        }
        
    }
 ```
 
 *Snippet of code for updating positions of units in square formation. While-loop skips all the positions that have nothing to do with a new layer. After assigning a new socket position the counter is increased and asked if it should start with a new row or with a new layer of units.
 
 ![image](https://user-images.githubusercontent.com/76696557/103306597-35f24700-4a0e-11eb-9bf6-1eed051001cc.png)
 
 *Scheme of the square with positions over each unit when formationScale = 1*
 
 The square has basic abilities of other formations: being able to change formation's orientation and unit position scale.
 
 ![UnitySquare](https://user-images.githubusercontent.com/76696557/103305748-38ec3800-4a0c-11eb-8dc8-ca94cca75678.gif)
 
 *Capturing units, changing unit positions scale and formation's orientation in square*
 
 ## Difficulties and problems
 
 1) One of the biggest issue of games with a large number of units to control is collision of those units. 
 
 >No matter what you do, units will overlap. Unit overlap is unavoidable or, at best, incredibly difficult to prevent in all cases.
 
 Source: https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1
 
 There are both theoretical and practical ways to handle this problem. For example code units in the way so they try not to touch each other if collision is expected. Or there actually are specific rules of regrouping from one formation to another. Another simple method would be forming 2 columns of odd and even units on left and right respectively and then building any formation unit by unit maybe even using a short timer to make it even clearer.
 
 At the moment, my projects switches of collision of units when they join the squad. It's the cheapest but the most unrealistic solution.
 
 2) In order to change formation's orientation the parent of all units is changed from the root of player pawn which doesn't rotate but moves forward/backward and left/right to the visual part which is a child of the root but unlike the parent it does rotate according to the movememnt vector.
 
 ```C#
 private void UpdateMovementAndRotation()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);

        Vector3 desiredLookAtDirection;
        desiredLookAtDirection.x = gameObject.transform.position.x + velocity.x;
        desiredLookAtDirection.z = gameObject.transform.position.z + velocity.z;
        desiredLookAtDirection.y = 1.5f;


        if (velocity.x == 0 && velocity.z == 0)
        {
            body.transform.LookAt(lastLookAtDirection, Vector3.up);
        }
        else
        {
            body.transform.LookAt(desiredLookAtDirection, Vector3.up);
            lastLookAtDirection = desiredLookAtDirection;
        }
    }
    
  ```
  *Snippet of code for updating player pawn's movement and rotation*
  
  But update functions for different formations are reseting local positions every time they are being called. This can be solved with recalculating X- and Z-positions while taking into account squads orientation. X should be then multiplied with cos of leader's orientation converted to suitable value and Z - with sin of the same value. I am aware of this bug.
  
 ## Potential of this research
 
This research is valuable as a fundament for making an RTS-project or even an RTS-third person action game. 
 
Adding some more rules will make this project even more like an independent game. For example, losing a unit on the go and then putting a new one on that free slot already makes visuals fun enough for the game.
 
Another sphere where the results of this research can be used - is graphical user interface design. A lot of games use objects in game space to indicate game information. Such GUIs are called diegetic and spatial. https://www.youtube.com/watch?v=yJCAZom84iE&ab_channel=RobCigna
 
 https://www.youtube.com/watch?v=id2ES1iTTmk&ab_channel=CheckpointTV
 
The simple math behind this project is powerful enough for calculating  positions for a flock of drones: https://www.youtube.com/watch?v=MlFtHuXPbv4&ab_channel=CGTN
 
 ## Conclusion
 
After having some experience in squad based and strategy games, researching how squads are actually formed in real life was very interesting and surprising. I came up with my own implementation of theoretically accurate formation-building algorithm which takes into account some formation adjusting variables, allowing the same formation be different. I also found out possible roles of the squad leader/core which can be either a part of the formation's pattern or an observer controlling all other units. The final version of this research project can be already used as a base for a game which involves building a squad and using different formations in specific situations.

I hope you found this article useful or at least interesting to read through. 

Thank you for your attention!

## References and useful links

Implementing Coordinated Movement - https://www.gamasutra.com/view/feature/3314/coordinated_unit_movement.php?print=1

Coordinated movement in Unity - http://www.jeremiahwarm.com/coordinated-movement.php

Formations of the WWII U.S. Army Infantry Rifle Squad - https://youtu.be/hGt4BFuKV4A

Squad movement and formations in Arma 3 - https://www.youtube.com/watch?v=u9YtqGaLfEA&ab_channel=LastStandGamers

RTS group movement project - https://sandruski.github.io/RTS-Group-Movement/#:~:text=Coordinated%20movement%20is%20the%20result,to%20achieve%20in%20this%20research.

 

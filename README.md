# GDIM32-Final
## Check-In
### Group Devlog
Our pick-up item feature required the raycasting function for detecting whether there is an item in front of the player and getting the item if its in range. It's implemented in the Interact() function in the Player Class which sents out a raycast from the direction the camera is facing, and goes as far as_raycastDistance. We also placed all interactable item in Item layer which allows us to use a layermask with the raycast so it only detects interactable items. If the raycast hits an interactable object, we use _itemHeld = hit.collider.GameObject().GetComponent<Item>() to get the item and call its Interact() function. The Interact function tells the item its picked up, and will send it into a state where it will "follow" an invisible game object thats a child of the player class, ensuring the item moves with the player. The player also saves the Item component so that, when you interact again, you can drop it (or throw it added after playtesting to increase the fun). 

The ability to pick items up and the structure of the script serving this purpase, is necessary in our project because our item interaction is based around physically picking up and carrying items around( This will be built apon for our grabbing mechanice of witches later). The raycast is sent from the camera's transform.forward, ensuring that as long as the item is centered on the player's screen, it'll be picked up. This is intuitive: most the time in real life, we pick up items that are in our view and relatively in front of us. 


### Team Member Zoya McDonnell
Contribution:
- At this phase, I made the UI checklist and clock script, Mainmenu scene and NPC script, built sone of the scene and UI functions. 

Proposal Reflection:
- It was great to get an idea of how my teammates think. I didn’t use the breakdown much for the early halfway point because I could use the knowledge I already have. I looked back at the proposal to find clarity in others’ code.
It was more than detailed enough for the stuff I did which was not as conected to others code yet other then invoking the caldron to check if the item had been dropped for the UI. I think my teammates being as capable as they are makes it easy for me to notice if they diverge from it, as long as we maintain clear communication. So far, we’ve been using Trello and have added a weekly section. I think spacing out time slots so we aren’t online at the same time will help tremendously.


### Team Member Isabel Matsuno
Contribution:
- I mainly built the Player script and prefab. I also made the Item abstract class.
- I actually was practicing doing the player controls in a different project before the proposal to try to figure out the 3D movement as well as how picking up and interacting with items would work. For this project, I ended up with a weird system of having thee main script on the main camera itself, and it is tied with a basic box gameobject (they are NOT parented with one another).

Proposal Reflection: 
- Proposal wasn't quite detailed enough. I usually end up having to figure out how different systems should "talk" to one another while I'm in the midst of coding. The proposal helps me think about it ahead of time, but I usually don't reference it that much while coding.
- I did generally check it to make sure we were adding the right things and thinking about how the final product should be working on the player side.


### Team Member Kristin Zhang
Contribute to the project at this stage:
- Build a Basic Quest System that detects we player bring an item to the Cauldron and sets the winning condition. Setting the GameController Class as singleton and game manager and the Cauldron Class as event sender. Used SceneManager to switch to win screen once the _itemCount >= _totalItemRequired condition is met.
- Added Audio Manager and Background Music Loop.
- Added Light to interactable object.
- Fixed bug where player can not get seconde object after putting first one in Cauldron by attaching the ItemRecieved event to Player Class and setting _hasItem to false once called.
- Added and Idle and Caught animation to fish item.

Reflection:

The break-down activity gives a clear overview on how the system should conmmunicate between each other. It has been useful to help determine which game object to set as singleton and which game object will be the event sender. The proposal activity also helps us to have a better overall understanding of the scope of our design and helps us determine the priority of tasks.


### Assets Used

[Environment models (trees bushes etc)](https://assetstore.unity.com/packages/3d/environments/landscapes/kaykit-forest-nature-pack-for-unity-318400)

[Mushroom models](https://assetstore.unity.com/packages/3d/vegetation/low-poly-mushrooms-pack-205460)

[Cliff models](https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-cliff-pack-67289)

[Animal models](https://assetstore.unity.com/packages/3d/characters/animals/quirky-series-free-animals-pack-178235)

[Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576)

[Background Music by ilyas_ananas](https://ilyas-ananas.itch.io/background-music)


[SCream effect by scottishman](https://pixabay.com/sound-effects/people-screaming-man-389826/)

[grab effect by Lucas_lesc](https://pixabay.com/sound-effects/film-special-effects-grab-clothes-foley-308655/)

[Geoffharvey creepy hollow music](https://pixabay.com/sound-effects/search/geoffharvey-creepy/)



## Final Submission
### Group Devlog
Finite State Machine pattern
NPCs will have states of Idle, Wandering, Pursued, and PickedUP, and each will display different movement behaviors and animation. They can be found mainly in the NPCs and Witch Class.
It helps NPC switch between multiple states. For example, an NPC will switch from Wandering to Pursued when the player comes close, which instead of moving in random direction will run from the direction the player comes from, and back to Wandering if they run enough distance away from the player. The NPC will also switch to PickedUP state if caught by the player and back to  Pursued after being held for a while as a designed escape function. Having a Finite State Machine made it easy to add this many states and keep track of their conditions.
Inheritance & Polymorphism
Witch and AnimalItem class Both inherit from NPCs class, while NPCs and BasicItem inherit from abstract class Item. We use Inheritance because they all have shared functions such as pickUP() and Interact() that allow Player to pick them up by mouse or [E] to put them into the Cauldron. And Witch and AnimalItem both have different states of movement. Therefore, we implemented pickUP() and Interact() in Item class that allow all subclasses to share. Inheritance here saves us time by not needing to recode the same function for a new subclass. As for Polymorphism, we added a Finite State Machine and movement code because unlike BasicItems like Mushrooms NPCs like Witches and Fishes will move around and run away when players come too close. This allows us to add more complex interactions to NPCs and especially Witches while keeping the Item script simple and without causing errors such as making the Mushrooms move as well.


### Isabel Matsuno
Contribution:
- I built more utility into the different children of the item class, in particular the NPC and Witch scripts.
- I tweaked some of the original code taken from one of the demos to make it so they run away from the player when they spot them
- Witches can also break out of your grasp, disable and enable the base item interaction according to quest progression, and have an idle state that makes dialogue easier
- Made a lot of prefabs, decorating the terrain (had to fix colliders for the trees in particular...)
  
### Zoya McDonnell
Contribution:
- Since the check-in, I have done the animation controllers and drawings for both witches in the states: idle, running, and struggling. 
- I also redrew the menu art and music assets, redesigned and placed the UI, and replaced the buttons. 
- I made terrain changes, edited the Witch script, and added animation and sound-effect transitions and functions for different witch states inherited from my old NPC script. 
- Added sprites to NPCs and drew them, and added a crosshair hand icon.

### Team Member Name 3
Put your individual final Devlog here.

## Open-Source Assets
Cite any open-source assets here. Put them in a LIST, and use correctly formatted LINKS.
**Assets used:**
- [Mushroom models](https://assetstore.unity.com/packages/3d/vegetation/low-poly-mushrooms-pack-205460)
- [Cliff models](https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-cliff-pack-67289)
- [Animal models](https://assetstore.unity.com/packages/3d/characters/animals/quirky-series-free-animals-pack-178235)
- [Background Music by ilyas_ananas](https://ilyas-ananas.itch.io/background-music)

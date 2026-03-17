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

## Final Submission
### Group Devlog
Put your group Devlog here.


### Isabel Matsuno
Contribution:
- I built more utility into the different children of the item class, in particular the NPC and Witch scripts.
- I tweaked some of the original code taken from one of the demos to make it so they run away from the player when they spot them
- Witches can also break out of your grasp, disable and enable the base item interaction according to quest progression, and have an idle state that makes dialogue easier
- Made a lot of prefabs, decorating the terrain (had to fix colliders for the trees in particular...)
  
### Zoya McDonnell
Contribution:
Put your individual final Devlog here.
### Team Member Name 3
Put your individual final Devlog here.

## Open-Source Assets
Cite any open-source assets here. Put them in a LIST, and use correctly formatted LINKS.
**Assets used:**
- [Mushroom models](https://assetstore.unity.com/packages/3d/vegetation/low-poly-mushrooms-pack-205460)
- [Cliff models](https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-cliff-pack-67289)
- [Animal models](https://assetstore.unity.com/packages/3d/characters/animals/quirky-series-free-animals-pack-178235)
- [Background Music by ilyas_ananas](https://ilyas-ananas.itch.io/background-music)

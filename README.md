# GDIM32-Final
## Check-In
### Group Devlog
Our pick-up item feature required raycasting function for detecting whether there is a item in front of the player and getting the item if in pickable distance. We used the Raycast concept that it sends out an invisible line to detect collition, which is relevent to detecting whether a game object is present in a speacific direction within a certain distance. We implemented it in the Interacti() function in Player Class, using Physics.Raycast(transform.position,transform.forward,out hit, _raycastDistance,raycastLayers), which sents out a raycast from the player location at direction camra is facing, and goes as far as_raycastDistance. We also placed all interactable item in Item layer which is set to raycastLayers to prevent getting environment assests. If the raycast hits an interactable object, we use _itemHeld = hit.collider.GameObject().GetComponent<Item>() to get the item.


### Team Member Name 1
Put your individual check-in Devlog here.
### Team Member Name 2
Put your individual check-in Devlog here.
### Team Member Kristin Zhang
Contribute to the project at this stage:
- Build a Basic Quest System that detects we player bring an item to the Cauldron and sets the winning condition. Setting the GameController Class as singleton and game manager and the Cauldron Class as event sender. Used SceneManager to switch to win screen once the _itemCount >= _totalItemRequired condition is met.
- Added Audio Manager and Background Music Loop.
- Added Light to interactable object.
- Fixed bug where player can not get seconde object after putting first one in Cauldron by attaching the ItemRecieved event to Player Class and setting _hasItem to false once called.
- Added and Idle and Caught animation to fish item.

Reflection:

The break-down activity gives a clear overview on how the system should conmmunicate between each other. It has been useful to help determine which game object to set as singleton and which game object will be the event sender. The proposal activity also helps us to have a better overall understanding of the scope of our design and helps us determine the priority of tasks.

## Final Submission
### Group Devlog
Put your group Devlog here.


### Team Member Name 1
Put your individual final Devlog here.
### Team Member Name 2
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

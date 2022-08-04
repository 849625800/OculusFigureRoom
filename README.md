# OculusFigureRoom
 An Unity3d project Asset floder of this VrFigureRoom project. It is my first VR program as well as my first Unity program. Therefore, it should be a lot of flaws in it. So... please bear with it LOL.
# How to deploy this project on your local machine?
-  Deploy an unity3d project and config your project to be able to run on oculus quest2 platform.
-  Copy all the files under this project to your Access file.
-  Import the Oculus Intergration from the Unity asset store.
# How to run directly on you Oculus quest?
-  Download the `FigureRoom.apk` file 
-  install the apk with the help of ADB toolkit.
# How to add new Figures to the storage?
- Save your 3d model to `.fbx` format using blender or other 3d modeling softwear.
- put your `.fbx` model under `Resources/GenshinModels`.
- If your model is an `item` other than a humaniod bone, you should rename your model ending with `(道具)`， which helps the program to recognize your model as an Item.(For example, your `weapon.fbx` shoule be renamed to `weapon(道具).fbx`. It just a temporary solution, may be updated later.
- A new model may come with a new icon. You can rename your icon `avatar` and put into the same path of the `.fbx` model.
- Last but not least, Please build the game again after every time you add new figures. Because There is an editor script under the `Editor` floder named `ModelPathWrapper.cs` generating a path of figures which will be used for loading the models.
# What can you do with this small demo?
- It has a controller based locomotion system which enable you to walk around in this sence. You can move your character by using the joystick of your left controller and snap turn your head by pushing the joystick of your right controller left or right.
- It also have a hand tracking based locomotion system. It is not resopnsive so not recommended. Right hand is the trigger of this locomotion system. When right hand thumbs up, and palm toward face, the locomotion system activated. If all fingers closed, disabled. For left hand, a sward finger pose would activate the movement. Palm dowm for moving forward, palm up for moving back,palm toward face for moving right and palm away from face for moving left.
- Player can use the menu above the treasure box to summon(generate) different figures. The humanoid model would be generated on the left side and the item would be generated on the right side. 
- Figures can be also grabbed by both one hand and two hands. When player use two hands to grab the figure, the figure can be rescale by changing the distance of two hands.

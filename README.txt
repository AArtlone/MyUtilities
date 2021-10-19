This project includes a collection of utilities.

You can check Samples folder for the example of how GUI utilities can be used.

Datasheet utility can be also found in the Samples folder and includes a separate README file that explains what that utility does
and provides steps for you to explore it's functionality.

Another Sample that is included in this package is ShowIf. It show how a ShowIfAttribute can be used and what it does.

Other utilities are explained in this README.

------------------------------------------------------------

SceneShortcut
SceneShortcut is a small custom window that allows users to createe shortcuts to their scenes.

In Assets/Resources/Editor right click and create a SceneShortcuts asset. Configure the asset to include desired scenes by entering
a name and referencing a scene asset.

Then access the SceneShortcut window by going to Window/SceneShortcut

------------------------------------------------------------

UtilitiesClass

This class is made include utility methods that can be used while coding to make the life of the programmer easier.


At the moment it includes two methods:

ShuffleList<T>(List<T> listToShuffle) - randomly shuffles and returns a given list
ClearLog() - clears the console for convenience purposes

------------------------------------------------------------

EditorUtilitiesClass

This class is made to include utitlity methods that can be used in the editor.

At the moment it includes two methods:

CreateGameObjectAtZero() - creates an empty GameObject at (0, 0, 0)
CreateSpriteAtZero() - create a Sprite GameObject at (0, 0, 0)

------------------------------------------------------------
IO
IO is a simple system that can be used to save and load game data.

To save the data you need to call IOUtility<T>.SaveData(T data, string name, Action<bool> callback).
Where T is the data class that you want to save,
name is the name of the save data file,
and a callback that you can use for your purposes.

To load tha data you need to call IOUtility<T>.LoadData(string name, Action<T> callback.
Where name is the name of the save data file,
callback is the callback that returns the data class that you can use for your purposes.

------------------------------------------------------------

Singleton
A Singleton class that allows for a convenient way to create a Signleton. This singleton only exists before the scene is destroyed. 
If you need a Singleton that exists after the scene is switched then use PersistentSingleton.
Simply inherit from the Singleton<T> class,
override the Awake method,
and in Awake call SetInstance(this).

------------------------------------------------------------

PersistentSingleton.
A PersistentSingleton.class that allows for a convenient way to create a Signleton that is added to DontDestroyOnLoad.
Simply inherit from the PersistentSingleton<T> class,
override the Awake method,
and in Awake call SetInstance(this).
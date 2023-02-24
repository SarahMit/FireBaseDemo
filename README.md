# FireBaseDemo
 A small example of how to track UI inputs and time from a Unity WebGL Build using Firebase

## In Unity
Make sure to add a folder named `Plugins` which contains a 'jslib' file. This file defines the interface between JavaScript and Unity.

![dynamic_label_screenshot](Readme/plugin.jpg)

The file content looks like this:
```
mergeInto(LibraryManager.library, {

    Hello: function() {
        window.alert("Hello, world!");
    },

    WriteFirebase: function(projectName, userID, scenename, responses, dbindex){
      fireBaseWriter(UTF8ToString(projectName), UTF8ToString(userID), UTF8ToString(scenename), UTF8ToString(responses), UTF8ToString(dbindex));
      //window.fireBaseWriter(UTF8ToString(projectName), UTF8ToString(userID), UTF8ToString(scenename), UTF8ToString(responses), UTF8ToString(dbindex));
    }
});
```

In the inspector of this file make sure WebGL is selected to add it when building the project.

![dynamic_label_screenshot](Readme/pluginsettings.jpg)

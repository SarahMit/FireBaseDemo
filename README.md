# FireBaseDemo
 A small example of how to track UI inputs and time from a Unity WebGL Build using Firebase.
[WebGL Demo](https://visualstories.cs.ovgu.de/demo/UnityFirebase)

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

Build the project as WebGL project.


## In Firebase
Create a new project.
Add an web app to your project.
Under `SDK setup and configuration` select `Configuration`.

![dynamic_label_screenshot](Readme/configuration.jpg)

In the project overview, a firebase hosting website has to be specified for the defined webapp.

![dynamic_label_screenshot](Readme/hostingwebsite.jpg)

In your index.html of the Unity WebGL build replace everything from `Script.onload = ()` to `document.body.appendChild(script);` with the following (you have to adapt it by using the data from you own firebase and depending on what values you want to track):

```
script.onload = () => {
        createUnityInstance(canvas, config, (progress) => {
          progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {
          loadingBar.style.display = "none";
          fullscreenButton.onclick = () => {
            unityInstance.SetFullscreen(1);
          };
        }).then((unityInstance) => {
        window.unityInstance = unityInstance;
        
        const firebaseConfig = {
          apiKey: "AIzaSyB1X7Jir5Vn8eurKzEU2TWacjNg0dmfseE",
          authDomain: "unityfirebasedemo-315d2.firebaseapp.com",
          databaseURL: "https://unityfirebasedemo-315d2-default-rtdb.europe-west1.firebasedatabase.app",
          projectId: "unityfirebasedemo-315d2",
          storageBucket: "unityfirebasedemo-315d2.appspot.com",
          messagingSenderId: "162038407218",
          appId: "1:162038407218:web:2a9ecdfa811a12a2629c2b"
        };

        const db = getDatabase(firebaseApp);
        //const auth = firebaseApp.auth();
        window.fireBaseWriter = function(scenename, id, slidename, seconds, dbIndex) {
          //set(ref(db, "UnityWebGLBuild" + '/' + id), {
          //    slidename: "time",
          //});
          set(ref(db, "UnityWebGLBuild" + '/' +scenename+ "/" + id + '/times/' + dbIndex.toString() + "-" + slidename), {
              seconds: seconds,
          });
        }
      }).catch((message) => {
        alert(message);
      })};
      document.body.appendChild(script);

      var fireBaseWriter = function(scenename, id, slidename, seconds, dbIndex)
      {
          dataBaseIndex = dataBaseIndex + 1;
          window.fireBaseWriter(scenename, id, slidename, seconds, dbIndex)
      }
```

In the `ExperimentLogger` script a function is called which is marked with `Internal`. This function is taken from the jslib file and then executed in the browser as javascript. In the index.html must be added the appropriate counterpart. There is an example `index.html` in the git.

mergeInto(LibraryManager.library, {

    Hello: function() {
        window.alert("Hello, world!");
    },

    WriteFirebase: function(projectName, userID, scenename, responses, time, dbindex){
      fireBaseWriter(UTF8ToString(projectName), UTF8ToString(userID), UTF8ToString(scenename), UTF8ToString(responses), time, dbindex);
      //window.fireBaseWriter(UTF8ToString(projectName), UTF8ToString(userID), UTF8ToString(scenename), UTF8ToString(responses), UTF8ToString(dbindex));
    }
});
"use strict";
var Level;
(function (Level) {
    Level[Level["Kid"] = 15] = "Kid";
    Level[Level["Easy"] = 9] = "Easy";
    Level[Level["Medium"] = 6] = "Medium";
    Level[Level["Hard"] = 3] = "Hard";
})(Level || (Level = {}));
let lvl = "Easy";
if (lvl == "Easy") {
    console.log(`The Level Is ${lvl} And Number Of Seconds Is ${9}`);
}
//# sourceMappingURL=index.js.map
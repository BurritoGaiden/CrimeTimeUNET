requirejs.config({
	"baseUrl": './Resources/www/js',
	"paths": {
		"jquery": '//code.jquery.com/jquery-1.11.3.min',
		"createjs": '//code.createjs.com/createjs-2015.11.26.min', 
	},
});

var timeout = 15; //15 seconds timeout when heartbeat stops
var mobile_offset = 0.75;
var stage_state = "STARTUP"; //replace this with player.state
var w, h, midXOffset, midYOffset;
var canvas, topc, botc;     
var stage, top_stage, bot_stage;
var map;
var map_setup = false;
var currentTile;

var player = {
    "id": 0,
    "username": "Bon",
    "state": 1,
    "ready": false,
    "icon": {},
    "character": {
        "name": "",
        "coords": {
            "x": 0,
            "y": 0,
        },
        "stats": {
            "health": 5,
            "punch": 6,
            "gun": 3,
            "move": 3
        },
    }
}
var players = [];
//Referenced strings
var character_conversion = ["BigBoy", "Graves", "Lulu", "Regards", "Fisticop"];
var characters = {
    "BigBoy" : {
        "name" : "BigBoy",
        "color": "#aba000",
        "icon": {},
        "index": -1
    },
    "Graves" : {
        "name": "Graves",
        "color": "#790000",
        "icon": {},
        "index": -1
    },
    "Lulu" : {
        "name": "Lulu",
        "color": "#8560a8",
        "icon": {},
        "index": -1
    },
    "Regards" : {
        "name": "Regards",
        "color": "#005e20",
        "icon": {},
        "index": -1
    },
    "Fisticop" : {
        "name": "Fisticop",
        "color": "#295194",
        "icon": {},
        "index": -1
    }
};
var borders;

var _extends = function(ChildClass, ParentClass)
{
    var f = function() { };
    f.prototype = ParentClass.prototype;

    // copy child prototype methods in case there are some defined already
    for(var m in ChildClass.prototype)
    {
        f.prototype[m] = ChildClass.prototype[m];
    }

    ChildClass.prototype = new f();
    ChildClass.prototype.constructor = ChildClass;
    ChildClass.prototype._super = ParentClass.prototype;        
};

requirejs(["start"]);
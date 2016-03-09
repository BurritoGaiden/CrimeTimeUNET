var tilesize = 75;
var GameMap = function() {
	this._super.constructor.call(this);
	this.rows = 0;
	this.columns = 0;
	this.maparray;
	this.currentpath = [];
	this.pan = false;
	this.pan_threshold = 1;
}
GameMap.prototype.generate = function(rows, columns){
	var mapobj = this;
	//var mapimg = preload_map.getResult("mapimg");
	//var mapimg_bitmap = new createjs.Bitmap(mapimg);
	//mapimg_bitmap.scaleX = mapimg_bitmap.scaleY = tilesize;
	//this.addChild(mapimg_bitmap);
	var firstTime = true;
	this.rows = rows;
	this.columns = columns;
	this.maparray = createArray(rows, columns);
	for(var row = 0; row < rows; row++){
		for(var col = 0; col < columns; col++){
			var tile = new createjs.Shape();
			if(row % 2 === 0 && col % 2 === 0) tile.graphics.beginFill("DimGray");
			else if(row % 2 !== 0 && col % 2 !== 0) tile.graphics.beginFill("DimGray");
			else tile.graphics.beginFill("Silver");
			tile.graphics.drawRect(0, 0, tilesize, tilesize);
			//tile.alpha = 0.2;
			tile.x = row*tilesize;
			tile.y = col*tilesize;
			mapobj.maparray[row][columns-col] = tile; 
			this.addChild(tile);
			//stage.update();
		}	

	}
	this.addEventListener("click", function(e){
		var selectedTile;
		var obj = map.getObjectUnderPoint(e.stageX-map.x, e.stageY-map.y, 1);
		if(obj == player.icon.getChildAt(0)){
			console.log("selected player");
			var row = player.character.coords.x;
			var col = player.character.coords.z+1;
			selectedTile = map.maparray[row][col];
		} else {
			selectedTile = obj;
		}
		//console.log("CLICK EVENT: " + mapobj.pan)
		if(firstTime) firstTime = false;
		else if(selectedTile !== null && !mapobj.pan) {
			var circle = new createjs.Shape();
			circle.graphics.beginFill("Crimson").drawCircle(0, 0, 10);
			circle.x = e.stageX;
			circle.y = e.stageY;
			stage.addChild(circle);
			createjs.Tween.get(circle, {loop: false})
				.to({alpha: 0, x: e.stageX}, 500, createjs.Ease.getPowOut(2));
			if(currentTile == undefined) {
				var selectBox = new createjs.Shape();
				selectBox.graphics.setStrokeStyle(3).beginStroke("Crimson").drawRect(0, 0, tilesize, tilesize);
				currentTile = selectBox;
				map.addChild(selectBox);
			}
			currentTile.x = selectedTile.x;
			currentTile.y = selectedTile.y;
			stage.update();
			mapobj.tileSelect(selectedTile.x, selectedTile.y);
		}
	})
	this.on("mousedown", function(e){
		mapobj.pan = false;
		//console.log("DOWN EVENT: " + mapobj.pan);
		this.parent.addChild(this);
		this.offset = {x: this.x - e.stageX, y: this.y - e.stageY};
	});
	this.on("pressmove", function(e){
		var mapobj = this;
		mapobj.pan = true;
		//console.log("PAN EVENT BEFORE: " + mapobj.pan);
		var x_disp = Math.abs(e.stageX + this.offset.x - this.x);
		var y_disp = Math.abs(e.stageY + this.offset.y - this.y);
		//console.log(x_disp + " " + y_disp);
		if(x_disp < mapobj.pan_threshold && y_disp < mapobj.pan_threshold) mapobj.pan = false;
		//console.log("PAN EVENT AFTER: " + mapobj.pan)
		this.x = e.stageX + this.offset.x;
		this.y = e.stageY + this.offset.y;
		stage.update();
	});
	this.x = 10;
	this.y = -h/2;
}

GameMap.prototype.tileSelect = function (x, y){
	var mapobj = this;
	$.post("ControlInput", 
		{
			username: player.username,
			command: "SelectTile", 
			x: (x)/tilesize, 
		 	z: (mapobj.columns - (y)/tilesize - 1)
		},
		function(data, status){
			try{
				data = JSON.parse(data);
				mapobj.clearPath();
				for(var i = 0; i < data.path.length; i++){
					var col = data.path[i].z+1;
					var row = data.path[i].x;
					mapobj.maparray[row][col].graphics.clear().beginFill("Red").drawRect(0, 0, tilesize, tilesize);
					mapobj.currentpath.push({"tile": mapobj.maparray[row][col], "col": col, "row": row});

				}
			} catch(err) {
				console.log(err);
			}
			stage.update();
		});
	
}

GameMap.prototype.clearPath = function(newpath) {
	var mapobj = this;
	for(var j = 0; j < mapobj.currentpath.length; j++){
		var row = mapobj.currentpath[j].row;
		var col = mapobj.currentpath[j].col-1;
		var ptile = mapobj.currentpath[j].tile;
		if(row % 2 === 0 && col % 2 === 0) ptile.graphics.clear().beginFill("DimGray").drawRect(0, 0, tilesize, tilesize);
		else if(row % 2 !== 0 && col % 2 !== 0) ptile.graphics.clear().beginFill("DimGray").drawRect(0, 0, tilesize, tilesize);
		else ptile.graphics.clear().beginFill("Silver").drawRect(0, 0, tilesize, tilesize);
	}
	mapobj.currentpath = [];
	stage.update();
}
_extends(GameMap, createjs.Container);

/* thanks yckart and mr. crumley on SO */
function createArray(length) {
    var arr = new Array(length || 0),
        i = length;

    if (arguments.length > 1) {
        var args = Array.prototype.slice.call(arguments, 1);
        while(i--) arr[length-1 - i] = createArray.apply(this, args);
    }

    return arr;
}
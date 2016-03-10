var curr_data = "";
var hb_lock = false;
define(["jquery", "preloader", "createjs", "map", "uilib"], function($) {
    $(function() {
        $(document).ready(function(){
        	preloader.init();
			init();
		});
		
        function init(){
			hud = document.getElementById("hud");
			canvas = document.getElementById("canvas");
			
			stage = new createjs.Stage(canvas);
			hud_stage = new createjs.Stage(hud);
			//hud_stage.addEventListener("reconnect", reconnect);
			createjs.Touch.enable(stage);
			createjs.Touch.enable(hud_stage);
			//createjs.Ticker.setFPS(60);
			//createjs.Ticker.addEventListener("tick", tick);
			//createjs.Ticker.addEventListener("tick", stage);
			canvas.width = hud.width = window.innerWidth
			canvas.height = hud.height = window.innerHeight;

			UI.width = canvas.width;
			UI.height = canvas.height;

			w = canvas.width;
			h = canvas.height;
			if(h <= 500) mobile_offset = 0.75; //TODO think of a better way to do this
			else mobile_offset = 1;

			preloader.preload.on("complete", showStartup);
			preloader.preload.loadManifest(preloader.startup_manifest);
		}

		//Function to call heartbeat

		function setupStartupBg(){
			var bg = new createjs.Shape();
			var startPopup = new createjs.Shape();
			bg.graphics.beginFill("#000").drawRect(0,0,w,h);
			startPopup.graphics.beginFill("DimGray").drawRoundRect(w/16, h/16, w - w/8, h - h/8, 5);
			var borderimg = preloader.preload.getResult("startupBorderImg");
			borders = new createjs.Container();
			var borderBitmap = new createjs.Bitmap(borderimg);
			var border_offset = 20;
			for(i = 1; i <= 4; i++){
				var border = borderBitmap.clone();
				border.rotation = (90*i)%360;
				var popupwidth = w - w/8;
				var popupheight = h - h/8;
				var bounds = border.getBounds();
				switch(i){
					//top-left (0,0)
					case 1: //top-right
						border.x = popupwidth - border_offset;
						break;
					case 3: //bottom-left
						border.y = popupheight - border_offset;
						break;
					case 2: //bottom-right
						border.x = popupwidth - border_offset;
						border.y = popupheight - border_offset;
						break;
				}
				border.scaleX = border.scaleY = mobile_offset;
				borders.addChild(border);
			}
			borders.x = w/16 + border_offset/2;
			borders.y = h/16 + border_offset/2;
			hud_stage.addChild(bg, startPopup);
		}

		function showStartup(){
			console.log(mobile_offset);
			console.log(w + " " + h);
			setupStartupBg();
			var startupScreen = new createjs.Container();
			startupScreen.name = "startupScreen";
			var usernameDOMField = document.getElementById("usernameInput");
			var usernameField = new createjs.DOMElement(usernameDOMField);
			usernameField.x = w/2 - 250;
			usernameField.y = (h - h/8 + h/16)/2;
			
			var title = new createjs.Text("HeistNight");
			title.font = '60px DAYPBL'
			title.color = "white";
			title.x = w/2 - (title.getBounds().width/2);
			title.y = h/16 + 10;
			var prompt = new createjs.Text("Your name, please:");
			prompt.font = "60px great-vibes";
			prompt.color = "white";
			prompt.x = w/2 - (prompt.getBounds().width/2);
			prompt.y = usernameField.y - 100*mobile_offset;
			var join_button_settings = {width:300, height:75, radius:5, color:"Grey"};
			var join_button_text_settings = {text:"Join the Heist!", size:30, font:"DAYPBL", color:"white"};
			var join_button = new Button(join_button_settings, join_button_text_settings);
			join_button.x = w/2 - 150;
			join_button.y = usernameField.y + 100*mobile_offset;
			usernameDOMField.onkeypress = function(e){
				if (!e) e = window.event;
				var keyCode = e.keyCode || e.which;
				if (keyCode == '13'){
					registerPlayer();
					return false;
				}
			}
			join_button.on("click", registerPlayer);
				
			var bracketimg = preloader.preload.getResult("startupBracketImg");
			var brackets = new createjs.Container();
			var bracketBitmap = new createjs.Bitmap(bracketimg);
			for(i = 0; i < 2; i++){
				var bracket = bracketBitmap.clone();
				bracket.rotation = i*180;
				switch(i){
					case 1:
						bracket.x = 500 + 2*100*mobile_offset; //600 is length of usernameField
						bracket.y = bracket.image.height;
						break;
				}
				bracket.y -= bracket.image.height/2;
				brackets.addChild(bracket);
				if(mobile_offset == 0.5) bracket.scaleY = bracket.scaleX = 0.75;
			}
			brackets.x = usernameField.x - 100*mobile_offset;
			brackets.y = (h - h/8)/2 + h/16;
			startupScreen.addChild(brackets, title, prompt, join_button, usernameField);
			hud_stage.addChild(borders, startupScreen);
			hud_stage.update();
		}
		function loadPlayers(){
			//get request data on all players
			//if(id != this player) add to players array
			//update player(s) data
		}

		var CharacterSelectIcon = function() {
			this._super.constructor.call(this);
			this.bg = new createjs.Shape();
			this.selected = false;
		}
		CharacterSelectIcon.prototype.generateIcon = function(character) {
			this.bg.graphics.beginFill("Gray").drawCircle(0, 0, 90);
			var character_icon_img = preloader.preload_icon.getResult("characterSelectIcon" + character);
			var character_icon_bitmap = new createjs.Bitmap(character_icon_img);
			character_icon_bitmap.x -= (character_icon_bitmap.image.width)/2;
			character_icon_bitmap.y -= (character_icon_bitmap.image.height)/2;
			switch(character){
				case 1: //Graves
					character_icon_bitmap.x -= 7/4;
					character_icon_bitmap.y -= 8/2;
					break;
				case 2: //Lulu
					character_icon_bitmap.y += 6/2;
					break;
				case 4: //Fisticop
					character_icon_bitmap.y -= 22/2;
					break;
			}
			// var character_details_img = preload.getResult("detailsIcon");
			// var character_details_bitmap = new createjs.Bitmap(character_details_img);
			// character_details_bitmap.on("click", displayCharacterDetail, i);
			// character_details_bitmap.x = character_icon_bitmap.x;
			// character_details_bitmap.y = ??????? Not sure if put on icon corner, or below it, or on top, etcetc...
			this.addChild(this.bg, character_icon_bitmap); //REMEMBER TO PUT BACK IN CHARACTER DETAILS
			//add event listener here that if someone has already selected this character, disable it/grey it out
			this.scaleX = this.scaleY = mobile_offset - 0.15;
			this.x -= this.getBounds().width/2;
			this.name = character;
		}
		_extends(CharacterSelectIcon, createjs.Container);

		function showCharacterSelect(){
			hud_stage.removeChild(hud_stage.getChildByName("startupScreen"));
			var charSelectScreen = new createjs.Container();
			charSelectScreen.name = "charSelectScreen";
			var promptText1 = new createjs.Text("Choose your", "50px DAYPBL", "white");
			var promptText2 = new createjs.Text("character", "50px DAYPBL", "white");
			promptText1.x = w/2 - promptText1.getBounds().width/2;
			promptText1.y = h/16;
			promptText2.x = w/2 - promptText2.getBounds().width/2;
			promptText2.y = promptText1.y + promptText1.getBounds().height/2 + 10;

			var ready_button_settings = {width:200, height:75, radius:5, color:"Grey"};
			var ready_button_text_settings = {text:"Ready", size:30, font:"DAYPBL", color:"white"};
			var ready_button;

			var characters = new createjs.Container();
			for(character = 4; character >= 0; character--){
				var character_icon = new CharacterSelectIcon();
				character_icon.generateIcon(character);
				
				character_icon.on("click", function(){
					var icon = this;
					//sent POST request to select
					$.post("ControlInput", 
					{
						username: player.username,
						command: "ChooseCharacter",
						character: character_conversion[this.name],
					},
						function(data, status){
							console.log("SELECTED CHARACTER " + character_conversion[icon.name]);
							for(var i=0; i<characters.children.length;i++){
								var other = characters.children[i];
								if(other!=icon){
									other.selected = false;
									other.bg.graphics.clear().beginFill("Gray").drawCircle(0, 0, 90);
								}
							}
							console.log(icon.selected);
							if(icon.selected) {
								icon.selected = false;
								icon.bg.graphics.clear().beginFill("Gray").drawCircle(0, 0, 90);
							}
							else {
								icon.selected = true;
								icon.bg.graphics.clear().beginFill("White").drawCircle(0, 0, 90);
							}
							if(player.ready){
								$.post("ControlInput", 
								{
									username: player.username,
									command: "ToggleReady",
								},
									function(data, status){
										console.log("Success! Data: " + data);
										var color = "DarkGray";
										var txt_color = "White";
										player.ready = !player.ready;
										if(player.ready) {
											color = "Gray";
											txt_color = "DarkGray";
										} else {
											color = "DarkGray";
											txt_color = "White";
										}
										ready_button.highlight(color, txt_color);
										//ready_box.graphics.beginFill(color).drawRoundRect(0, 0, 200, 75, 5);
										//ready_button_text.color = txt_color;
										hud_stage.update();
								})
							}
							hud_stage.update();
					})
				})
				var fisticop_icon;
				if(character == 4) {
					fisticop_icon = character_icon;
					var bounds = character_icon.getBounds();
					console.log(character_icon.getBounds());
					character_icon.x = w/2;
					character_icon.y = h/2;
					character_icon.scaleX = character_icon.scaleY = mobile_offset;
				}
				else if(character % 2 == 0){ //left side icons
					character_icon.x = fisticop_icon.x - fisticop_icon.getBounds().width/2 - 150*mobile_offset;
					if(character/2 == 0) character_icon.y = promptText2.y + 125*mobile_offset;
					else if (character/2 == 1) character_icon.y = promptText2.y + 145*2.1*mobile_offset;
				} else { //right side icons
					character_icon.x = fisticop_icon.x + fisticop_icon.getBounds().width/2 + 150*mobile_offset;
					if(character == 1) character_icon.y = promptText2.y + 125*mobile_offset;
					else if (character == 3) character_icon.y = promptText2.y + 145*2.1*mobile_offset;
				} 
				characters.addChild(character_icon);
			}

			ready_button = new Button(ready_button_settings, ready_button_text_settings);
			ready_button.x = w/2 - 100;
			ready_button.y = characters.getBounds().height + 100*mobile_offset;
			ready_button.on("click", function(){
				$.post("ControlInput", 
				{
					username: player.username,
					command: "ToggleReady",
				},
					function(data, status){
						console.log("Success! Data: " + data);
						var color = "DarkGray";
						var txt_color = "White";
						player.ready = !player.ready;
						if(player.ready) {
							color = "Gray";
							txt_color = "DarkGray";
						} else {
							color = "DarkGray";
							txt_color = "White";
						}
						ready_button.highlight(color, txt_color);
						hud_stage.update();
				})
			})

			charSelectScreen.addChild(promptText1, promptText2, characters, ready_button);
			hud_stage.addChild(charSelectScreen);
			hud_stage.update();
		}
		var hb;
		function registerPlayer() {
			var usernameDOMField = document.getElementById("usernameInput");
			player.username = usernameDOMField.value.trim();
			//var encodedName = encodeURI(player.username);
			$.post("PlayerRegister", player.username,
				function(data, status){
					if(data == "fail"){
						//show popup to 'try again' eventually
					} else {
						console.log("Data: " + data + " Status: " + status);
						usernameDOMField.style.visibility = "hidden";	
						heartbeat();
						hb = setInterval(heartbeat, 500);
					}
				});
			// if(detectmob()){
			// 	var doc = window.document;
		// 			var docEl = doc.documentElement;
			// 	var requestFullScreen = docEl.requestFullscreen || docEl.mozRequestFullScreen || docEl.webkitRequestFullScreen || docEl.msRequestFullscreen;
			// 	var cancelFullScreen = doc.exitFullscreen || doc.mozCancelFullScreen || doc.webkitExitFullscreen || doc.msExitFullscreen;

			// 	if(!doc.fullscreenElement && !doc.mozFullScreenElement && !doc.webkitFullscreenElement && !doc.msFullscreenElement) {
			// 	  requestFullScreen.call(docEl);
			// 	}
			// 	else {
			// 	  cancelFullScreen.call(doc);
			// 	}
			// } 
		}

		function tick(e){
			//TODO: make this performance efficient for mobile devices
			//hud should only need to update when an event triggers
			stage.update();
		}

		function heartbeat() {
			if(hb_lock) {
				console.log("HB LOCKED!");
				return;
			}
			else hb_lock = true;
			$.post("MapState", 
			{
				username: player.username,
			},
				function(data, status){
					//console.log("Success! Data: " + data);
					try {
						if(curr_data != data){
							data = JSON.parse(data);
							//Redirect client to appropriate screen
							//TODO LATER: Write functions to update player stats
							if(data.state == 2 && player.state != 2) {
								preloader.preload_icon.on("complete", showCharacterSelect);
								preloader.preload_icon.loadManifest(preloader.charSelect_manifest);
							}
							if(data.state == 3 && player.state != 3) {
								//do stuff
							}
							if((data.state == 6 || data.state == 4 || data.state == 5)) {
								var stat_change = !(JSON.stringify(player.character.stats) == JSON.stringify(data.myCharacter.stats));
								player.character = data.myCharacter;
								if(map_setup == false){
									if(!preloader.preload_icon.loaded){
										preloader.preload_icon.on("complete", function(){
											initOtherPlayers();
											getMapData(data);
										});
										preloader.preload_icon.loadManifest(preloader.charSelect_manifest);
									}
									else{
										initOtherPlayers();
										getMapData(data);
									}

								}
								else {
									if(player.stat_ui.name != undefined && stat_change) {
										console.log("Player stat change!");
										player.stat_ui.update(player);
									}
									movePlayerTo(player.character.coords);
									for(var c in characters){
										if(c == player.character.name) continue;
										var character = characters[c];
										map.removeChildAt(character.index);
										character.index = -1;	
									}
									for(var i = 0; i < data.characters.length; i++){
										if(data.characters[i].username != player.username){
											updatePlayer(data.characters[i]);
										}	
									}
								}
							}
							player.state = data.state;
							curr_data = data;
						}
						
						
					} catch(err) {
						console.log(err + " " + err.lineNumber);
					}
			})
			.fail(function() {
				setTimeout(function(){
					clearInterval(hb);
				}, timeout*1000);
			})
			.always(function() {
				hb_lock = false;
			});
			stage.update();
		}
		function initOtherPlayers(){
			for(var c in characters){
				var character = characters[c];
				character.icon = new createjs.Container();
				character.icon.name = "Player";
				var icon = new createjs.Shape();
				icon.graphics.beginFill(character.color).drawCircle(0, 0, tilesize/3);
				character.icon.addChild(icon);	
			}
		}
		function spawnPlayerAt(coords){
			var pchar = characters[player.character.name];
			player.icon = new createjs.Container();
			player.icon.name = "Player";
			var icon = new createjs.Shape();
			icon.graphics.beginFill(pchar.color).drawCircle(0, 0, tilesize/3);
			player.icon.addChild(icon);
			var row = coords.x;
			var col = coords.z+1;
			player.icon.x = pchar.icon.x = map.maparray[row][col].x + tilesize/2;
			player.icon.y = pchar.icon.y = map.maparray[row][col].y + tilesize/2;
			console.log(row + " " + col);
			var child = map.addChild(player.icon);
			pchar.index = map.getChildIndex(child);
			map.x = (w - 2*tilesize*row)/2;
			map.y = (h - 2*tilesize*(map.columns-col))/2;
			map.updateCache();
		}

		function updatePlayer(p) {
			console.log("Updating " + p.name);
			var pchar = characters[p.name];
			var row = p.coords.x;
			var col = p.coords.z+1;
			pchar.icon.x = map.maparray[row][col].x + tilesize/2;
			pchar.icon.y = map.maparray[row][col].y + tilesize/2;
			if(pchar.index == -1){
				var child = map.addChild(pchar.icon);
				pchar.index = map.getChildIndex(child);
			}
			map.updateCache();
		}

		function movePlayerTo(coords){
			//var icon = player.icon.getChildAt(0);
			//icon.graphics.clear().beginFill("Pink").drawCircle(0, 0, tilesize/3);
			var row = coords.x;
			var col = coords.z+1;
			player.icon.x = map.maparray[row][col].x + tilesize/2;
			player.icon.y = map.maparray[row][col].y + tilesize/2;
			//console.log(map.currentpath.length);
		}

		function getMapData(characterData){
			var bg = new createjs.Shape();
			bg.graphics.beginFill("#000").drawRect(0,0,w,h);
			stage.addChild(bg);
			map_setup = true;
			var mapdata;
			$.post("ControlInput", {
				username: player.username,
				command: "GetMapData",
			},
				function(data, status){
					console.log(data);
					data = JSON.parse(data);
					map_manifest = [
						{src: data.imagePath, id: "mapimg", type: "image"}
					];

					map = new GameMap();
					map.generate(data.width, data.height);
					spawnPlayerAt(player.character.coords);
					for(var i = 0; i < characterData.characters.length; i++){
						if(characterData.characters[i].username != player.username){
							players.pop(characterData.characters[i]);
							updatePlayer(characterData.characters[i]);
						}	
					}
					setup();
				}
			)
			
			return mapdata;
		}

		function setup(){
			hud_stage.clear();
			hud_stage.removeAllChildren();
			hud_stage.removeAllEventListeners();
			hud_stage.update();
			var top = new createjs.Shape();
			var bot = new createjs.Shape();
			var bg = new createjs.Shape();
			bg.graphics.beginFill("#000").rect(0, 0, w, h);
			bg.alpha = 0.01;
			top.graphics.beginFill("Gray").drawRect(0, 0, w, h/5);
			bot.graphics.beginFill("Gray").drawRect(0, h-(h/5), w, h/5);
			stage.addChild(map);
			hud_stage.addEventListener("click", toMap);
			hud_stage.addEventListener("mousedown", toMap);
			hud_stage.addEventListener("pressmove", toMap);

			var txtSize = 32;
			if(w < 500) txtSize = 28;

			var action_button_settings = {width:150, height:75, radius:5, color:"DimGrey"};
			var action_button_text_settings = {text:"Move", size:txtSize, font:"DAYPBL", color:"white"};
			var action_button = new Button(action_button_settings, action_button_text_settings);

			action_button.scaleX = action_button.scaleY = mobile_offset;
			action_button.x = (w - action_button_settings.width - 20)*mobile_offset;
			action_button.y = (h - action_button_settings.height - 20)*mobile_offset;
			action_button.on("click", function(){
				//send post
				console.log("C ommitting movements");
				$.post("ControlInput", 
					{
					username: player.username,
					command: "CommitMove", 
				}, function(data, status) {
					console.log(data);
					map.clearPath();
					map.currentpath = [];
				});
				map.clearPath();
				console.log("cleared");
			})
			hud_stage.addChild(bg);
			//hud_stage.addChild(top);
			hud_stage.addChild(bot);
			hud_stage.addChild(action_button);
			setupBotHud();
			//setupTopHud();
		}

		function toMap(e){
			map.dispatchEvent(e);
		}

		function resize(){
			//TODO: update w/h values and tell canvas to rerender.
			//console.log("resized!");
			console.log("resize!");
			if(stage_state != "STARTUP") {
				hud_stage.clear();
				hud_stage.removeAllEventListeners();
				stage.removeAllEventListeners();
				stage.clear();
				createjs.Touch.enable(stage);
				createjs.Touch.enable(hud_stage);
				stage.canvas.width = hud_stage.canvas.width = window.innerWidth;
				stage.canvas.height = hud_stage.canvas.height = window.innerHeight;
				w = window.innerWidth;
				h = window.innerHeight;
				if(h <= 500) mobile_offset = 0.5;
				else mobile_offset = 1;
				setupStartupBg();
				//setup();
			}
		}

		function setupBotHud(){
			var player_icon = new createjs.Container();
			var icon_bg = new createjs.Shape();
			var index = character_conversion.indexOf(player.character.name);
			var character_icon_img = preloader.preload_icon.getResult("characterSelectIcon" + index);
			var character_icon_bitmap = new createjs.Bitmap(character_icon_img);
			icon_bg.graphics.beginFill("DimGray").drawCircle(0, 0, 100);
			character_icon_bitmap.scaleX = character_icon_bitmap.scaleY = 100/90;
			character_icon_bitmap.x -= ((character_icon_bitmap.image.width)/2) * 100/90;
			character_icon_bitmap.y -= ((character_icon_bitmap.image.height)/2) * 100/90;
			switch(index){
				case 1: //Graves
					character_icon_bitmap.x -= 7/4;
					character_icon_bitmap.y -= 8/2;
					break;
				case 2: //Lulu
					character_icon_bitmap.y += 6/2;
					break;
				case 4: //Fisticop
					character_icon_bitmap.y -= 22/2;
					break;
			}
			player_icon.addChild(icon_bg, character_icon_bitmap);
			player_icon.x = 120;
			player_icon.y = h-(h/5) + 50;
			//add mask for player sprite/portrait
			// var player_icon_img_bitmap = new createjs.Bitmap(player_icon_img);
			// player_icon_img_bitmap.x = 32;
			// player_icon_img_bitmap.y = h-(h/5) - 35;
			// player_icon_img_bitmap.scaleX = player_icon_img_bitmap.scaleY = 0.35;

			var player_stats = new Stats(player);
			player_stats.x = 230;
			player_stats.y = h-(h/5);
			player.stat_ui = player_stats;
			//TODO: Should have bottom section of HUD be it's own container, then child
			//everything that's setup here. Makes positioning items easiers
			hud_stage.addChild(player_icon, player_stats);
			hud_stage.update();
		}

		function detectmob() { 
			 if( navigator.userAgent.match(/Android/i)
			 || navigator.userAgent.match(/webOS/i)
			 || navigator.userAgent.match(/iPhone/i)
			 || navigator.userAgent.match(/iPad/i)
			 || navigator.userAgent.match(/iPod/i)
			 || navigator.userAgent.match(/BlackBerry/i)
			 || navigator.userAgent.match(/Windows Phone/i)
			 ){
			    return true;
			  }
			 else {
			    return false;
			  }
		}
    });
});

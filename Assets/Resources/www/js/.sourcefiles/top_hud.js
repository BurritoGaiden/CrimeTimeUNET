/* saved in separate js file in case we need it in the future */
function setupTopHud(){
			//loadPlayers();
			var teamicons = [];
			if(player.character.name == "Fisticop"){
				for(m = 0; m < players.length; m++){
					var member_display = new createjs.Container();
					member_display.x = m*(w/5);
					member_display.y = (h/5)/2 - 10;
					var member_icon = new createjs.Shape();
					member_icon.graphics.beginFill("DimGray").drawCircle(0, 0, 40, 40);
					member_icon.x = 50;
					var member_stats = new createjs.Container();
					var name = new createjs.Text("player " + (m+1), "18px cinzel", "Black");
					var health = new createjs.Container();
					var range = new createjs.Container();
					var melee = new createjs.Container();
					var movespeed = new createjs.Container();
					generatePips(health, 2, false);
					generatePips(range, 2, false);
					generatePips(melee, 2, false);
					generatePips(movespeed, 2, false);
					member_stats.x = 100;
					member_stats.y = -40;
					health.y = 25;
					range.y = health.y + 15;
					melee.y = range.y + 15;
					movespeed.y = melee. y + 15;
					member_stats.addChild(name, health, range, melee, movespeed);
					member_display.addChild(member_icon, member_stats);
					hud_stage.addChild(member_display);
				}
			}
			else{
				for(m = 0; m < players.length; m++){
					var member_display = new createjs.Container();
					member_display.x = m*(w/5 - 20);
					member_display.y = (h/5)/2 - 10;
					var member_icon = new createjs.Shape();
					member_icon.graphics.beginFill("DimGray").drawCircle(0, 0, 40, 40);
					member_icon.x = 50;
					var member_stats = new createjs.Container();
					var name = new createjs.Text("player " + (m+1), "18px Impact", "Black");
					var health = new createjs.Container();
					var range = new createjs.Container();
					var melee = new createjs.Container();
					var movespeed = new createjs.Container();
					generatePips(health, 2, false);
					generatePips(range, 2, false);
					generatePips(melee, 2, false);
					generatePips(movespeed, 2, false);
					member_stats.x = 100;
					member_stats.y = -40;
					health.y = 25;
					range.y = health.y + 15;
					melee.y = range.y + 15;
					movespeed.y = melee. y + 15;
					member_stats.addChild(name, health, range, melee, movespeed);
					member_display.addChild(member_icon, member_stats);
					hud_stage.addChild(member_display);
					hud_stage.update();
				}
				var seperator = new createjs.Shape();
				seperator.graphics.beginFill("DimGray").drawRoundRect(0, 0, 4, (h/5) - 20, 2);
				var seperator_settings = seperator.clone();
				seperator.x = w - 2*(w/5) - 30;
				seperator.y = 10;
				seperator_settings.x = w - (w/5) + 100;
				seperator_settings.y = 10;
				var fisticop_display = new createjs.Container();
				fisticop_display.x = 3*(w/5) + 30;
				fisticop_display.y = (h/5)/2 - 10;
				var fisticop_icon = new createjs.Shape();
				fisticop_icon.graphics.beginFill("DimGray").drawCircle(0, 0, 40, 40);
				fisticop_icon.x = 0;
				
				var fisticop_stats = new createjs.Container();
				var name = new createjs.Text("player " + 5, "18px Impact", "Black");
				var health = new createjs.Container();
				var range = new createjs.Container();
				var melee = new createjs.Container();
				var movespeed = new createjs.Container();
				generatePips(health, 2, false);
				generatePips(range, 2, false);
				generatePips(melee, 2, false);
				generatePips(movespeed, 2, false);
				fisticop_stats.x = 50;
				fisticop_stats.y = -40;
				health.y = 25;
				range.y = health.y + 15;
				melee.y = range.y + 15;
				movespeed.y = melee. y + 15;
				fisticop_stats.addChild(name, health, range, melee, movespeed);
				fisticop_display.addChild(fisticop_icon, fisticop_stats);
				hud_stage.addChild(seperator, fisticop_display, seperator_settings);
			}
			hud_stage.update();
		}
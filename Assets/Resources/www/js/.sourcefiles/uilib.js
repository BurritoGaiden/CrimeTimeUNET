/**
 *	Keeps track of UI information
 *	UPDATE AT THE VERY BEGINNING OF CODE SO SCREEN INFO
 *	IS ACCURATE
 *	
 * 	TODO: should also have preloader info?? maybe???
 */
var UI = {
	width: 0,
	height: 0
}

/**
 * 	Titlecard code
 * 	Constructor: Generates the base of the titlecard
 *	TODO
 */
var Screen = function(){
	var screen_bg = new createjs.Shape();
	var screen_fg = new createjs.Shape();
	screen_bg.graphics.beginFill("#000").drawRect(0, 0, UI.width, UI.height);
	screen_fg.graphics.beginFill("DimGray").drawRoundRect(
		UI.width/16, UI.height/16, 
		UI.width - UI.width/8, UI.height - UI.height/8, 5);
	var borderimg = preloader.preload.getResult("startupBorderImg");
	borders = new createjs.Container();
	var borderBitmap = new createjs.Bitmap(borderimg);
	var border_offset = 20;
	for(i = 1; i <= 4; i++){
		var border = borderBitmap.clone();
		border.rotation = (90*i)%360;
		var popupwidth = UI.width - UI.width/8;
		var popupheight = UI.height - UI.height/8;
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
	this.addChild(screen_bg, screen_fg);
	return screen;
};

/**
 * 	Adds title to titlecard
 */
Screen.prototype.addTitle = function(text_settings) {
	//TODO
};

/**
 *	Adds description to titlecard
 */ 
Screen.prototype.addDesc = function(text_settings) {
	//TODO
};

/**
 * Adds brackets to title card
 */
Screen.prototype.addBrackets = function() {
	//TODO
};

_extends(Button, createjs.Container);

/**
 * box_settings {w, h, r, color}
 * text_settings {text, size, font, color}
 */
var Button = function(box_settings, text_settings){
	//If certain settings don't exist, then set defaults
	box_settings = box_settings || {};
	text_settings = text_settings || {};

	box_settings['width'] = text_settings['width'] || 100;
	box_settings['height'] = text_settings['height'] || 100;
	box_settings['radius'] = text_settings['radius'] || 5;
	box_settings['color'] = text_settings['color'] || "Grey";

	text_settings['text'] = text_settings['text'] || "";
	text_settings['size'] = text_settings['size'] || "10px";
	text_settings['font'] = text_settings['font'] || "DAYPBL";
	text_settings['color'] = text_settings['color'] || "white";

	var button_box = new createjs.Shape();
	var button_text = new createjs.Text(
		text_settings.text, 
		text_settings.size + " " + text_settings.font,
		text_settings.color);
	button_box.graphics.beginFill(box_settings.color).drawRoundRect(
		0, 0, 
		box_settings.width, 
		box_settings.height, 
		box_settings.radius);

	button_text.x = box_settings.width/2 - button_text.getBounds().width/2;
	button_text.y = box_settings.height/2 - button_text.getBounds().height/2;

	this.addChild(button_box, button_text);
	return this;
};

_extends(Button, createjs.Container);
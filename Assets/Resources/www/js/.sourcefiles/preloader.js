preloader = {
	preload: {},
	preload_icon: {},
	preload_map: {},
	init: function(){
		console.log(this);
		this.preload = new createjs.LoadQueue(true, "./Resources/www/");
		this.startup_manifest = [
			{src: "sprites/titleborder.png", id: "startupBorderImg", type: "image"},
			{src: "sprites/titlebracket.png", id: "startupBracketImg", type: "image"}
		];
		this.preload_icon = new createjs.LoadQueue(true, "./Resources/www/sprites/icons_placeholder/");
		this.charSelect_manifest = [
			{src: "characterSelectIcon0.png", id: "characterSelectIcon0", type: "image"},
			{src: "characterSelectIcon1.png", id: "characterSelectIcon1", type: "image"},
			{src: "characterSelectIcon2.png", id: "characterSelectIcon2", type: "image"},
			{src: "characterSelectIcon3.png", id: "characterSelectIcon3", type: "image"},
			{src: "characterSelectIcon4.png", id: "characterSelectIcon4", type: "image"},
		];
		this.preload_map = new createjs.LoadQueue(true, "");
	}
}
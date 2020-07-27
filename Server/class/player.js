var shortID = require("shortid");
var vector2 = require("./vector2.js");
var vector3 = require("./vector3.js");
var color = require("./color.js");
module.exports = class player {
	constructor() {
		this.name = "";
		this.id = shortID.generate();
		this.position = new vector2();
		this.scale = new vector3();
		this.color = new color();
		this.score = 0;
		this.kills = 0;
	}
}
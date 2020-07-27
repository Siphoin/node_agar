module.exports = class vector2 {
	constructor(X = "", Y = "") {
		this.x = X;
		this.y = Y;
	}

	Magnutude() {
		return Math.sqrt((this.x * this.x) + (this.y * this.y));
	}

	Normalize() {
		var mag = Magnutude();
		return new vector2(this.x / mag, this.y / mag);
	}

	Distance(vector = vector2) {
		var dist = new vector2();
		dist.x = vector.x - this.x;
		dist.y = vector.y - this.y;
		return dist.Magnutude();
	}

	Log() {
		return "{" + this.x + "," + this.y + "}";
	}
}
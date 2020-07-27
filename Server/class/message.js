var shortID = require("shortid");
module.exports = class message {
	constructor() {
		this.id = shortID.generate();
		this.dataMessage = "";
	}
}
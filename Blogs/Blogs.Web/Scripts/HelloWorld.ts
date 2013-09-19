// Module
module Ajax {

    // Class
    export class Loader  {
        // Constructor
        constructor () { }

        getFoo() {
            return 1;
        }
    }

}
var ans = new Ajax.Loader().getFoo();
console.log("Successfully called TypeScript code " + ans);
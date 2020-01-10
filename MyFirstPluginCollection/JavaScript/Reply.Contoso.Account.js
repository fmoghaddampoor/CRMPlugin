/*Hint: Define namespace for functions if its not a duplicated using type of namespad is undefined*/

//Namespace Reply.contoso.account functions
if (typeof (Reply.contoso.account) === "undefined") {
    Reply.contoso.account = {
        btnAlert_OnClick: function () {
            console.log("On click is called");    
            Actioncaller.call();
        }
    };
}
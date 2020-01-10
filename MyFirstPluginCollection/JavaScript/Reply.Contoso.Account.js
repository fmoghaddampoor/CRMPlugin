/*Hint: Define namespace for functions if its not a duplicated using type of namespad is undefined*/

//Namespace reply functions
if (typeof (Reply) === "undefined") {
    Reply = {

    };
}

//Namespace Reply.contoso functions
if (typeof (Reply.contoso) === "undefined") {
    Reply.contoso = {
         
    };
}

//Namespace Reply.contoso.account functions
if (typeof (Reply.contoso.account) === "undefined") {
    Reply.contoso.account = {
        OnClick: function () {
            console.log("On click is called");      
        }
    };
}
/*Hint: Define namespace for functions if its not a duplicated using type of namespace is undefined*/


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
        btnAlert_OnClick: function () {
            console.log("On click is called");    
            Actioncaller.call();
        }
    };
}
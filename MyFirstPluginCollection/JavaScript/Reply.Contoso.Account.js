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
            var data =
            {
                //Account comes from input process arguments for entity reference type
                "Account": {
                    //.acccount is the logial name of account table
                    "@odata.type": "Microsoft.Dynamics.CRM.account",
                    //account_id is primary key of account entity
                    //The id of entity comes with {} so we need to replace them
                    "accountid": Xrm.Page.data.entity.getId().replace('{', '').replace('}', '')
                }
            }

            // called if action caller is failed
            function onError()
            {
                window.Xrm.Navigation.openAlertDialog({ text: error.error.message });
            }

            // called if action caller s successful
            function onSuccess()
            {

            }
            // call action
            Actioncaller.call("cr1b8_myprocess", data, null, onError, onSuccess);
        }
    };
}
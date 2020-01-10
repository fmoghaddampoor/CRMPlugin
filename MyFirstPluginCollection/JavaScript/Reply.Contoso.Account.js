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

        // Primary control option pass execution context to this
        // function in crm if executed from ribbon workbench
        btnAlert_OnClick: function (executionContext) {
            console.log("On click is called");  
            var formContext = executionContext.getFormContext();
            var data =
            {
                //Account comes from input process arguments for entity reference type
                "Account": {
                    //.acccount is the logial name of account table
                    "@odata.type": "Microsoft.Dynamics.CRM.account",
                    //account_id is primary key of account entity
                    //The id of entity comes with {} so we need to replace them
                    "accountid": formContext.data.entity.getId().replace('{', '').replace('}', '')
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
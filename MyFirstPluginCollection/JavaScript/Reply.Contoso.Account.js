/*Hint: Define namespace for functions if its not a duplicated using type of namespace is undefined*/
//Test

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
        btnAlert_OnClick: function (primaryControl) {
            console.log("On click is called");  
            /*The debugger statement invokes any available debugging functionality,
            * such as setting a breakpoint.If no debugging functionality is available,
            * this statement has no effect.*/
            debugger;

            // Data should be passed to the acction caller call function
            var data =
            {
                //Account comes from input process arguments for entity reference type
                "Account": {
                    //.acccount is the logial name of account table
                    "@odata.type": "Microsoft.Dynamics.CRM.account",
                    //account_id is primary key of account entity
                    //The id of entity comes with {} so we need to replace them
                    "accountid": primaryControl.data.entity.getId().replace('{', '').replace('}', '')
                }
            }

            // Called if action caller is failed
            function onError(error)
            {
                window.Xrm.Navigation.openAlertDialog({ text: error.error.message });
            }

            // Called if action caller is successful
            function onSuccess(response)
            {
                var entityFormOptions = {};
                entityFormOptions["entityName"] = "account";
                entityFormOptions["entityId"] = response.accountid;
                //Open the enitity  
                Xrm.Navigation.openForm(entityFormOptions);
            }

            // Call action
            ActionCaller.call("cr1b8_myprocess", data, null, onError, onSuccess);
        }
    };
}
if (typeof (ActionCaller) === "undefined") {
    var ActionCaller = {};
}

if (typeof (ActionCaller.WEBAPI_Version) === "undefined") {
    ActionCaller.WEBAPI_Version = "v9.0";
}

ActionCaller.call = function (actionName, actionData, target, onError, onSuccess) {

    /*The debugger statement invokes any available debugging functionality,
     * such as setting a breakpoint.If no debugging functionality is available,
     * this statement has no effect.*/
    debugger;

    var requestName = actionName;
    if (target !== null) {
        var entityId = target.id;
        var logicalName = target.setLogicalname;
        requestName = logicalName + "(" + entityId.replace("{", "").replace("}", "") + ")/Microsoft.Dynamics.CRM." + actionName;
    }

    var url = Xrm.Page.context.getClientUrl() + "/api/data/" + ActionCaller.WEBAPI_Version + "/" + requestName;
    
    var req = new XMLHttpRequest();
    req.open("POST", url, true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");
    

    req.onreadystatechange = function () {
        
        if (req.readyState === 4 && (req.status === 500 || req.status === 400)) {
            var responseErr = JSON.parse(req.response);
            onError(responseErr);
        }
        else if (req.readyState === 4) {
            if (req.response) {
                debugger;
                var response = JSON.parse(req.response);
                onSuccess(response);
            }
            else {
                debugger;
                onSuccess(response);
            }
            
        }
    }

    req.send(window.JSON.stringify(actionData));  
}






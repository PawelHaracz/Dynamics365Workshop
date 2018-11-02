var prefix = "bry_"
function UpdateVersion(lookupName, entityName) {
    var windowOptions = {
        openInNewWindow: true
    };
    var parameters = {};
    Xrm.Page.data.entity.attributes.forEach(function (attribute, index) {
        if (attribute.getName().startsWith(prefix)) {
            if (attribute.getValue() === null) return;
            if (attribute.getAttributeType() === "lookup") {
                var lookup = attribute.getValue()[0];
                parameters[attribute.getName()] = lookup.id.replace("{", "").replace("}", "");
                parameters[attribute.getName() + "name"] = lookup.name;
            } else
                parameters[attribute.getName()] = attribute.getValue();
        }
    });
    parameters[lookupName] = Xrm.Page.data.entity.getId().replace("{", "").replace("}", "");
    parameters[lookupName + "name"] = Xrm.Page.data.entity.getPrimaryAttributeValue();
    Xrm.Utility.openEntityForm(entityName, null, parameters, windowOptions);
}

var objs = { id: 'Hello', style: 'background-color: red; width: 64px; height: 64px;' };
var objs2 = { id: 'world', style: 'background-color: green; width: 128px; height: 64px;' };
window.onload = function(){
    var item = document.getElementById("genetest");

    GenerateElement("p", objs, item);
    GenerateElement("p", objs2, null);
}

function GenerateElement(ele, attributes, parent){
    var item = document.createElement(ele);
    Object.keys(attributes).forEach(function(key){
        item.setAttribute(key, attributes[key]);
    });

    if(parent){
        parent.appendChild(item);
    }

    return item;
}
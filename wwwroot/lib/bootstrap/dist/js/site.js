window.showMessage = function (text) {
    alert(text);
}

let dotNetHelper = null;

window.registerDotNetHelper = function (helper) {
    dotNetHelper = helper;
}

window.callDotNetFromJs = function () {
    if (dotNetHelper) {
        dotNetHelper.invokeMethodAsync(
            'ReceiveMessage',
            'JS успішно викликав C# метод'
        );
    }
}
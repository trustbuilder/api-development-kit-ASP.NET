function checkPushAjax(checkURL) {
    $.ajax({
        type: "GET",
        cache: false,
        url: checkURL,
        data: '',
        success: function (data) {

            console.log(data);
            var response = $.parseJSON(data);

            console.log(response);

            if (response.result == 'OK') {
                $('#MainContent_sendToMobile').hide();
                $('#success').show();
            }
            
            else if (response.result == 'WAITING') {
                startPushListener(checkURL)
            }
            
            else if (response.result == 'NOK') {
                $('#MainContent_sendToMobile').hide();
                $('#authenticationError').show();
            }
        }
    });
}

function startPushListener(checkURL) {
    setTimeout(function () {
        checkPushAjax(checkURL);
    }, 500);
}


$(document).ready(function() {
    $('#sendPush').live("click", function () {
        $('#MainContent_sendToMobile').hide();
        $('#success').hide();
        $('#authenticationError').hide();
    });
});
var url;
var urlPref;
var cancelTrip = function (reqID) {

    var succMsg = '', alertMsg = '';
  
    if (reqID.indexOf(',') != -1) {

        succMsg = "You have cancelled selected trip";
        alertMsg = "Do you want to cancel selected trip(s) ?";
    }
    else
    {
        succMsg = "You have cancelled this trip";
        alertMsg = "Do you want to cancel this trip?";
    }

    var pageURL = $(location).attr("href");
  
    if (confirm(alertMsg)) {

        $.ajax({
            url: urlPref +'/Common/CancelTripByUser',
            type: "POST",
            data: { 'ReqID': reqID, 'URL': pageURL },
            contenttype: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Result == true) {
                    window.location = pageURL;
                    alert(succMsg);
                   
                }
                else {
                    alert("We are unable to process your request. Please try after some time.");
                }
            },
            error: function (err) {
                alert("Error::Not Responding");
            }
        });
    }
}

$(document).ready(function () {

    $('#cbCheckAll').on('click', function () {
        checkUnCheck();
    });

    $('.checkbox').on('click', function () {
        countTotalChecked();
    });

});

var checkUnCheck = function () {
    var status = $('#cbCheckAll').is(':checked', true) ? true : false;
    $(".checkbox").each(function () {
        if (status) {
            $(".checkbox").prop('checked', true);
            manageDeleteAllButton('btn btn-default', 'btn btn-danger', 'default');
        }
        else {
            $(".checkbox").prop('checked', false);
            manageDeleteAllButton('btn btn-danger', 'btn btn-default', 'not-allowed');
        }

    })
}
var countTotalChecked = function () {
    var status = $('#cbCheckAll').is(':checked', true) ? true : false;
    var countChecked = 0, totalCheckBoxes = 0;
    $(".checkbox").each(function () {
        if ($(this).is(':checked', true)) {
            countChecked = countChecked + 1;
        }
        totalCheckBoxes = totalCheckBoxes + 1;
    })

    if (countChecked > 0 && (countChecked < totalCheckBoxes)) {
        $("#cbCheckAll").prop('checked', false);
        manageDeleteAllButton('btn btn-default', 'btn btn-danger', 'default');
    }
    else if (countChecked > 0 && countChecked == totalCheckBoxes) {
        $("#cbCheckAll").prop('checked', true);
        manageDeleteAllButton('btn btn-default', 'btn btn-danger', 'default');
    }
    else if (countChecked == 0) {
        $("#cbCheckAll").prop('checked', false);
        manageDeleteAllButton('btn btn-danger', 'btn btn-default', 'not-allowed');
    }

}

var manageDeleteAllButton = function (removeClass, addClass, cursor_value) {
    $("#btnCancelTrip").removeClass(removeClass);
    $("#btnCancelTrip").addClass(addClass);
    $("#btnCancelTrip").css('cursor', cursor_value);
    if (cursor_value != 'default') {
        $("#btnCancelTrip").attr('disabled', true);
        $("#btnCancelTrip").css('title', 'No trip(s) seleted to delete');
    }
    else {
        $("#btnCancelTrip").removeAttr("disabled");
        $("#btnCancelTrip").css('title', 'Click here to delete');
    }
}
var cancelSelectedRequest = function () {
    var Ids = '';
    $(".checkbox").each(function () {
        if ($(this).is(':checked', true)) {
            if (Ids != '') {
                Ids += ",";
            }
            Ids += $(this).attr('id');
        }

    })

    if(Ids.trim()=="")
    {
        alert('Please select trip(s) to delete.');
    }
    else
    {
        cancelTrip(Ids);
    }
}





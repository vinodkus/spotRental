var urlPref;
$(document).ready(function () {
    getPrimaryList();
    $('#btnReject').click(function () {
        rejectTrip();
    });
    $('#btnAssign').click(function () {
        assignVehicleSubmit();
    });

});
var getPrimaryList = function () {
    $.ajax({
        url: urlPref + '/UserBucket/RenderSupervisorApprovalList',
        type: "GET",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            if (data) {
                $('#divPerformAction').html('');
                $('#divPerformAction').html(data);
            }
            else {
                $('#divPerformAction').html('');
            }
        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });
}
var getApprovedList = function () {
    $.ajax({
        url: urlPref+'/UserBucket/RenderSupervisorApprovedList',
        type: "GET",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            if (data) {
                $('#divApproved').html('');
                $('#divApproved').html(data);
            }
            else {
                $('#divApproved').html('');
            }
        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });
}
var getRejectedList = function () {
    $.ajax({
        url: urlPref +  '/UserBucket/RenderSupervisorRejectedList',
        type: "GET",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            if (data) {
                $('#divRejected').html('');
                $('#divRejected').html(data);
            }
            else {
                $('#divRejected').html('');
            }
        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });
}
var approveTrip = function (reqID) {
    if (confirm('Are you sure to approve this trip request?')) {
        $.ajax({
            url: urlPref +  '/UserBucket/ApprovedTrip',
            type: "GET",
            data: { 'ReqID': reqID },
            contenttype: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Result == true) {
                    getPrimaryList();
                    alert("Trip approved successfully.");
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
var rejectTrip = function (reqID) {
    if (confirm('Are you sure to reject this trip request?')) {
        $.ajax({
            url: urlPref +  '/UserBucket/RejectTripBySupervisor',
            type: "GET",
            data: { 'ReqID': reqID },
            contenttype: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Result == true) {
                    getPrimaryList();
                    alert("Trip rejected successfully.");
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
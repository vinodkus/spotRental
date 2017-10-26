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
        url: urlPref +  '/UserBucket/RenderVendorBucketForAccept',
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
var acceptTrip = function (reqID) {
    if (confirm('Are you sure to accept this trip request?')) {
        $.ajax({
            url: urlPref +  '/UserBucket/AcceptTrip',
            type: "GET",
            data: { 'ReqID': reqID },
            contenttype: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Result == true) {
                    getPrimaryList();
                    alert("Trip accepted successfully.");
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
var rejectTrip = function () {
    var reasonId = $('#ddlReason').val();
    var reamrks = $('#txtRemarks').val();
    var reqID = $('#hdnRequestID').val();
    if (reasonId == "" || reasonId == "0") {
        alert('Please select reason to reject this trip.');
        return false;
    }
    else {
        $.ajax({
            url: urlPref +  '/UserBucket/RejectTrip',
            type: "GET",
            data: { 'ReqID': reqID, 'StatusCode': 'RJ', 'ReasonID': reasonId, 'ReasonRemarks': reamrks },
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
var rejectTripCnf = function (reqID) {
    if (confirm('Are you sure to reject this trip request?')) {
        $('#divRejectTrip').modal('toggle');
        bindReason('VEN', '#ddlReason');
        $('#hdnRequestID').val(reqID);
    }
}
var getAcceptedList = function () {
    $.ajax({
        url: urlPref +  '/UserBucket/RenderAcceptedVendorBucket',
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
        url: urlPref +  '/UserBucket/RenderRejectedVendorBucket',
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
var assignVehicle = function (reqID, type) {
    $('#divAssignVehicleDriver').modal('toggle');
    $('#hdnRequestID_VEH').val(reqID);
    $('#btnAssign').attr("disabled", true);
    $('#aviewDriverDIS').css('display', 'none');
    $('#aviewVehicleDIS').css('display', 'none');
    $('#aviewGuardDIS').css('display', 'none');
    if (type != undefined || type != null) {
        $('#hdnVehicleAssignType').val('1'); // If re-assign of vehicle and driver
        $('#heading').text('Re-Assign Vehicle and Driver');
        $('#divReason_REASSIGN').css('display', 'block');
        $('#divReasonRemark_REASSIGN').css('display', 'block');
        bindReason('VEN', '#ddlReason_REASSIGN');
    }
    else {
        $('#hdnVehicleAssignType').val('0'); // If normal assign of vehicle and driver
        $('#heading').text('Assign Vehicle and Driver');
        $('#divReason_REASSIGN').css('display', 'none');
        $('#divReasonRemark_REASSIGN').css('display', 'none');
    }
    bindVehicleForVendor('#ddlVehicle');
    bindDriverAndGuardForVendor('#ddlDriver', '#ddlGuard')
}
var assignVehicleSubmit = function (reqID) {

    var assignType = $('#hdnVehicleAssignType').val();
    var reqID = $('#hdnRequestID_VEH').val();
    var reasonID = $('#ddlReason_REASSIGN').val();
    var reasonRemarks = $('#txtRemarks_REASSIGN').val();


    var vehID = $('#ddlVehicle').val();
    var driverID = $('#ddlDriver').val();
    var GuardID = $('#ddlGuard').val();

    if (assignType == "1" && (reasonID == null || reasonID == "0")) {
        alert('Invalid request/trip.');
    }
    else if (reqID == null || reqID == "0") {
        alert('Invalid request/trip.');
    }
    else if (vehID == null || vehID == "0") {
        alert('Please select vehicle.');
    }
    else if (driverID == null || driverID == "0") {
        alert('Please select driver.');
    }
    else {

        var statusCode = "";
        if (assignType == "1")
        {
            statusCode = "VRAD";
        }
        else
        {
            statusCode = "VAD";
        }

        $.ajax({
            url: urlPref +  '/UserBucket/AssignVehicleAndDriver',
            type: "GET",
            data: { 'ReqID': reqID, 'StatusCode': statusCode, 'VehicleID': vehID, 'DriverID': driverID, 'GuardID': GuardID, 'ReasonID': reasonID, 'ReasonRemarks': reasonRemarks },
            contenttype: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Result == true) {
                    getAcceptedList();
                    alert("Vehicle assigned successfully.");
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
var bindVehicleForVendor = function (cntrlID) {
    $.ajax({
        url: urlPref +  '/Common/GetAllVehicleListForVendor',
        type: "GET",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            listitems = '<option value=0>--SELECT VEHICLE--</option>';
            if (data.Result == true) {

                $('#hdnVehicleDetails').val(JSON.stringify(data));
                $.each(data.List.listVehicle, function (key, value) {
                    listitems += '<option value=' + "'" + value.VehicleID + "'" + '>' + ' [' + value.RegistrationNo + '] - ' + value.VehicleType + '</option>';
                });
                $(cntrlID).empty();
                $(cntrlID).append(listitems);
            }
            else {
                $('#hdnVehicleDetails').val('');
                $(cntrlID).empty();
                $(cntrlID).append(listitems);
            }

        },
        error: function (err) {
            $('#hdnVehicleDetails').val('');
            alert("Error::Not Responding");
        }
    });
}
var bindDriverAndGuardForVendor = function (cntrlID_Driver, cntrlID_Guard) {
    $.ajax({
        url: urlPref +  '/Common/GetAllDriverVehicleForVendor',
        type: "GET",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            listitemsDriver = '<option value=0>--SELECT DRIVER--</option>';
            listitemsGuard = '<option value=0>--SELECT Guard--</option>';
            if (data.Result == true) {
                $('#hdnDriverDetails').val(JSON.stringify(data));
                $.each(data.List.listDriverGuard, function (key, value) {
                    if (value.EmpType == "D") {
                        listitemsDriver += '<option value=' + "'" + value.DriverGuardId + "'" + '>' + value.FirstName + ' ' + value.LastName + '</option>';
                    }
                    else if (value.EmpType == "G") {
                        listitemsGuard += '<option value=' + "'" + value.DriverGuardId + "'" + '>' + value.FirstName + ' ' + value.LastName + '</option>';
                    }
                });
                $(cntrlID_Driver).empty();
                $(cntrlID_Driver).append(listitemsDriver);

                $(cntrlID_Guard).empty();
                $(cntrlID_Guard).append(listitemsGuard);

            }
            else {
                $(cntrlID_Driver).empty();
                $(cntrlID_Driver).append(listitemsDriver);

                $(cntrlID_Guard).empty();
                $(cntrlID_Guard).append(listitemsGuard);
            }

        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });
}
var hideshowViewVehicle = function () {
    if ($('#ddlVehicle').val() == "0") {
        $('#aviewVehicle').css('display', 'none');
        $('#aviewVehicleDIS').css('display', 'block');
        $('#btnAssign').attr("disabled", true);
    }
    else {
        $('#aviewVehicle').css('display', 'block');
        $('#aviewVehicleDIS').css('display', 'none');

        if ($('#ddlDriver').val() != "0") {
            $('#btnAssign').removeAttr('disabled');
        }
        else {
            $('#btnAssign').attr("disabled", true);
        }
    }
}
var hideshowViewDriver = function () {
    if ($('#ddlDriver').val() == "0") {
        $('#aviewDriver').css('display', 'none');
        $('#aviewDriverDIS').css('display', 'block');
        $('#btnAssign').attr("disabled", true);
    }
    else {
        $('#aviewDriver').css('display', 'block');
        $('#aviewDriverDIS').css('display', 'none');

        if ($('#ddlVehicle').val() != "0") {
            $('#btnAssign').removeAttr('disabled');
        }
        else {
            $('#btnAssign').attr("disabled", true);
        }
    }
}
var hideshowViewGuard = function () {
    if ($('#ddlGuard').val() == "0") {
        $('#aviewGuard').css('display', 'none');
        $('#aviewGuardDIS').css('display', 'block');
    }
    else {
        $('#aviewGuard').css('display', 'block');
        $('#aviewGuardDIS').css('display', 'none');
    }
}
var showVehicleDetails = function () {

    var vehID = $('#ddlVehicle').val();
    var data = JSON.parse($('#hdnVehicleDetails').val());
    $.each(data.List.listVehicle, function (key, value) {
        if (value.VehicleID == vehID) {
            $('#divViewVehicleDetails').modal('toggle');
            $('#lblRegistrationNo').text(value.RegistrationNo);
            $('#lblCapcity').text(value.Capacity);
            $('#lblDriverName').text(value.DriverName);
            $('#lblLocationCode').text(value.LocCode);
            if (value.Source != null && value.Source != "") {

                $('#lblPendingTrip').text('[From]: ' + value.Source + '    [To]: ' + value.Destination + '    [PickupTime]:' + value.PickTime);
                $('#lblPendingTrip').css('color', 'red');
            }
            else {
                $('#lblPendingTrip').text('Not available');
                $('#lblPendingTrip').css('color', 'green');
            }

        }
    });
}
var showDriverDetails = function () {

    var driverID = $('#ddlDriver').val();
    var data = JSON.parse($('#hdnDriverDetails').val());
    $.each(data.List.listDriverGuard, function (key, value) {
        if (value.DriverGuardId == driverID) {
            $('#divViewDriverDetails').modal('toggle');
            $('#lblDriverName_View').text(value.FirstName + ' ' + value.LastName);
            $('#lblMobileNo').text(value.MobileNo);
            $('#lblDriverAddress').text(value.Address);
        }
    });
}
var showGuardDetails = function () {

    var GuardID = $('#ddlGuard').val();
    var data = JSON.parse($('#hdnDriverDetails').val());
    $.each(data.List.listDriverGuard, function (key, value) {
        if (value.DriverGuardId == GuardID) {
            $('#divViewGuardDetails').modal('toggle');
            $('#lblGuardName_View').text(value.FirstName + ' ' + value.LastName);
            $('#lblMobileNoGuard').text(value.MobileNo);
            $('#lblDriverAddressGuard').text(value.Address);
        }
    });
}
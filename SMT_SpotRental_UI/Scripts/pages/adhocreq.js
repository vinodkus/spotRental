var iCntrlCount;
var urlPref;
$(document).ready(function () {
    renderRouteDetail();
    iCntrlCount = 0;
    $('#btnSubmitAdHocRequest').click(function () {
        submitAdHocRequest();
    });
});
var renderRouteDetail = function () {

    var smallBoxColors = ['#00c0ef', '#7b3cea', '#8cef5f', '#337ab7', '#f1e613', '#f16452'];
    var rand = function () {
        return Math.floor(Math.random() * 6);
    };
    var randomColor = function () {
        return smallBoxColors[rand()];
    };

    $.ajax({
        url: urlPref +  '/UserBucket/RenderRouteDetail',
        type: "POST",
        contenttype: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            $("#divRoutDetails").append(data);
            $('#divEstimatedAmountBox_' + iCntrlCount).css('background-color', randomColor())
            generateTime(iCntrlCount);
            bindRentalCity('#ddlRentalCity_' + iCntrlCount);
            bindwithGoogleAPI('RentalCityNameOther_' + iCntrlCount);
            bindwithGoogleAPI('SourceOtherName_' + iCntrlCount);
            bindwithGoogleAPI('DestinationOtherName_' + iCntrlCount);
            $(function () {
                $('.datepicker').datepicker({
                    format: 'mm-dd-yyyy',
                    startDate: '+d',
                    autoclose: true
                });
            });
            iCntrlCount++;
        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });

}
var deleteRouteDetail = function (routeCount) {
    if (routeCount != 0) {
        $("#divRoute_" + routeCount).remove();
    }
}
var generateTime = function (cntrl) {
    for (hrs = 0; hrs <= 23; hrs++) {
        for (min = 0; min < 60; min += 15) {
            $("#ddlReportingTime_" + cntrl).append($("<option></option>").val((hrs.toString().length == 1 ? ('0' + hrs) : hrs) + ':' + (min.toString().length == 1 ? ('0' + min) : min)).html((hrs.toString().length == 1 ? ('0' + hrs) : hrs) + ':' + (min.toString().length == 1 ? ('0' + min) : min)));
        }
    }
}
var bindSourceDestination = function (cntrl) {

    var rentalCityName = $('#ddlRentalCity_' + cntrl).val();
    var carType = $('#ddlCarTypeControl_' + cntrl).val();
    var routeType = $('#ddlRouteTypeControl_' + cntrl).val();
    if (routeType == "0" && carType == "0") {
        alert('Please select route type and car type.');
        return false;
    }
    else if (routeType == "0") {
        alert('Please select route type.');
        return false;
    }
    else if (carType == "0") {
        alert('Please select car type.');
        return false;
    }
    else if (rentalCityName == "Other") {
        var listitems = '<option value=0>--Select--</option><option value=Other>Other</option>'
        $('#ddlSourceControl_' + cntrl).empty();
        $('#ddlSourceControl_' + cntrl).append(listitems);

        $('#ddlDestinationControl_' + cntrl).empty();
        $('#ddlDestinationControl_' + cntrl).append(listitems);

    }
    else {
        var Amount = 0;
        $.ajax({
            url: urlPref +  '/UserBucket/GetSourceAndDestinationWithRate',
            type: "GET",
            data: { 'RentalCityName': rentalCityName, 'CarType': carType, 'RouteType': routeType },
            contenttype: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {

                if (data.Result == true) {
                    var listitems = '<option value=0>--Select--</option>'
                    $.each(data.List.locationList, function (key, value) {
                        listitems += '<option value=' + "'" + value.LocationName + "'" + '>' + value.LocationName + '</option>';
                        Amount = value.Rate;
                    });

                    $('#ddlSourceControl_' + cntrl).empty();
                    $('#ddlSourceControl_' + cntrl).append(listitems);

                    $('#ddlDestinationControl_' + cntrl).empty();
                    $('#ddlDestinationControl_' + cntrl).append(listitems);

                }
                showAmountAndCity(cntrl, Amount);
            },
            error: function (err) {
                alert("Error::Not Responding");
            }
        });
    }
}
var bindDestination = function (cntrl) {
    var sourceId = $('#ddlSourceControl_' + cntrl).val();
    var $options = $('#ddlSourceControl_' + cntrl + ' > option').clone();
    var rentalCity = $('#ddlRentalCity_' + cntrl).val();
    $('#ddlDestinationControl_' + cntrl + ' > option').remove();
    $('#ddlDestinationControl_' + cntrl).append($options);

    if (sourceId != '0' && sourceId != '--Select--') {
        $('#ddlDestinationControl_' + cntrl + '  option[value="' + sourceId + '"]').remove();
        $('#divSourceOther_' + cntrl).css('display', 'none');
        $('#divSourceLandMark_' + cntrl).css('display', 'none');
    }
    if (rentalCity == 'Other' && sourceId == 'Other') {
        $('#ddlDestinationControl_' + cntrl + ' > option').remove();
        $('#ddlDestinationControl_' + cntrl).append('<option value=0>--Select--</option><option value=Other>Other</option>');
        $('#divSourceOther_' + cntrl).css('display', 'block');
        $('#divSourceLandMark_' + cntrl).css('display', 'block');
    }
    else {
        $('#divSourceOther_' + cntrl).css('display', 'none');
        $('#divSourceLandMark_' + cntrl).css('display', 'none');
    }
}
var showhideOtherCarType = function (cntrl) {

    $('#divCarTypeOther_' + cntrl).css('display', $('#ddlCarTypeControl_' + cntrl).val() == "Other" || $('#ddlCarTypeControl_' + cntrl).val() == "SUV" ? 'block' : 'none');
    $('#divCarTypeNameOther_' + cntrl).css('display', $('#ddlCarTypeControl_' + cntrl).val() == "Other" || $('#ddlCarTypeControl_' + cntrl).val() == "SUV" ? 'block' : 'none');

}
var showhideOtherDestination = function (cntrl) {
    $('#divDestinationOther_' + cntrl).css('display', $('#ddlDestinationControl_' + cntrl).val() == "Other" ? 'block' : 'none');
    $('#divDestinationLandMark_' + cntrl).css('display', $('#ddlDestinationControl_' + cntrl).val() == "Other" ? 'block' : 'none');
}
var showAmountAndCity = function (cntrl, amount) {

    if ($('#ddlRentalCity_' + cntrl).val() != "0" && $('#ddlRentalCity_' + cntrl).val() != "Other") {
        $('#lblYourLocationControl_' + cntrl).text($('#ddlRentalCity_' + cntrl).val());
        $('#colrightAmmountFooter_' + cntrl).css('display', 'block');
        $('#divAmountFooterMSG_' + cntrl).css('display', 'none');

        if (amount != "0") {
            $('#EstimatedAmount_' + cntrl).text(amount);
            $('#lblAmmountNo_' + cntrl).text('');

        }
        else {
            $('#lblAmmountNo_' + cntrl).text('In this particular city, We do not have contracted rate, so rack rate will be applicable.');
        }
    }
    else {
        $('#divAmountFooterMSG_' + cntrl).css('display', 'block');
        $('#colrightAmmountFooter_' + cntrl).css('display', 'none');
        $('#lblYourLocationControl_' + cntrl).text('Other');
        $('#lblAmmountNo_' + cntrl).text('In this particular city, We do not have contracted rate, so rack rate will be applicable.');
        //------------Show other city box----------//
        $('#divRentalCityOther_' + cntrl).css('display', 'block');

    }
}
var showEmployeeDetails = function () {
    var empID = $('#txtEmployeeId').val();
    if (empID.trim().length > 3) {
        var empData = getEmployeeDetails(empID);
        if (empData != undefined && empData.Result == true) {
            $('#txtEmployeeName').val(empData.List.userList[0].UserName);
            $('#txtMobileNo').val(empData.List.userList[0].MobileNo);
            $('#txtEmailId').val(empData.List.userList[0].EmailID);
            $('#txtCredit').val(empData.List.userList[0].CreditCard);
            $('#DDLGender').val(empData.List.userList[0].Gender != null ? empData.List.userList[0].Gender : 'M');
            $('#txtHomeAddress').val(empData.List.userList[0].HomeAddress);
            $('#txtDesignation').val(empData.List.userList[0].DesignationName);
        }
        else {
            alert('Employee does not exist in database/ wrong employee id entered.');
            $('#txtEmployeeName').val('');
            $('#txtMobileNo').val('');
            $('#txtEmailId').val('');
            $('#txtCredit').val('');
            $('#DDLGender').val('M');
            $('#txtHomeAddress').val('');
            $('#txtDesignation').val('');
        }
    }
    else {
        $('#txtEmployeeName').val('');
        $('#txtMobileNo').val('');
        $('#txtEmailId').val('');
        $('#txtCredit').val('');
        $('#DDLGender').val('M');
        $('#txtHomeAddress').val('');
        $('#txtDesignation').val('');
    }
}
var submitAdHocRequest = function () {
    if (validateSubmit()) {
        var AdhocRequestEntity = new Array();
        var RouteType, CarType, RentalCity, ReportingDate, ReportingTime, Source, Destination, EstimatedCost, LandMarkSource = '', LandMarkDestination = '';
        for (iCount = 0; iCount < iCntrlCount; iCount++) {
            if ($('#ddlRouteTypeControl_' + iCount).val() != undefined && $('#ddlRouteTypeControl_' + iCount).val() != '0') {

                RouteType = $('#ddlRouteTypeControl_' + iCount).val();
                CarType = $('#ddlCarTypeControl_' + iCount).val();
                if (CarType == "Other" || CarType == "SUV") {
                    CarType = $('#CarTypeNameOther_' + iCount).val();
                }

                RentalCity = $('#ddlRentalCity_' + iCount).val();
                if (RentalCity == "Other") {
                    RentalCity = $('#RentalCityNameOther_' + iCount).val();
                }

                ReportingDate = $('#ReportingDate_' + iCount).val();
                ReportingTime = $('#ddlReportingTime_' + iCount).val();
                Source = $('#ddlSourceControl_' + iCount).val();

                if (Source == "Other") {
                    Source = $('#SourceOtherName_' + iCount).val();
                    LandMarkSource = $('#SourceLandMark_' + iCount).val();
                }

                Destination = $('#ddlDestinationControl_' + iCount).val();
                if (Destination == "Other") {
                    Destination = $('#DestinationOtherName_' + iCount).val();
                    LandMarkDestination = $('#DestinationLandMark_' + iCount).val();

                }
                EstimatedCost = $('#EstimatedAmount_' + iCount).text();
                var request = {
                    'RouteType': RouteType,
                    'CarType': CarType,
                    'RentalCity': RentalCity,
                    'ReportingDate': ReportingDate,
                    'ReportingTime': ReportingTime,
                    'Source': Source,
                    'Destination': Destination,
                    'EstimatedCost': EstimatedCost,
                    'LandMarkSource': LandMarkSource,
                    'LandMarkDestination': LandMarkDestination
                };
                AdhocRequestEntity.push(request);
            }
            else {
                continue;
            }
        }


        var request = { requestList: AdhocRequestEntity }



        var UserDetails = {
            UserName: $('#txtEmployeeName').val(),
            EmployeeID: $('#txtEmployeeId').val(),
            MobileNo: $('#txtMobileNo').val(),
            EmailID: $('#txtEmailId').val(),
            PaymentMode: $('#ddlPayMentMode').val(),
            Remarks: $('#txtRemarks').val(),
            RequestBy: '',
            HomeAddress: $('#txtHomeAddress').val(),
            IsOfficialTrip: $("#cbTripType").is(":checked") == true ? "Y" : "N"
        }
       
        $('.enableLoader').show();
        $.ajax({
            url: urlPref +  '/UserBucket/CreateAdhocRequest',
            contentType: 'application/json;utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify({ requestList: AdhocRequestEntity, userDetails: UserDetails }),
            success: function (data) {
                $('.enableLoader').hide();
                if (data.Result == true) {
                    alert('Your adhoc request submited successfully.');
                }
                else if (data.Result == false && data.Msg == "DUPLICATE") {
                    alert('This user has already booking for given time slot or before or after 2 HRS.');
                }
                else {
                    alert('Error: ' + data.Msg);
                }

               
            },
            error: function (err) {
                $('.enableLoader').hide();
                alert("Error::Not Responding " + err.responseText);
            }
        });
        //---------------------------------------
    }
}
var validateSubmit = function () {
    if ($('#txtEmployeeId').val().trim() == "") {
        alert('Please enter employee code/id.');
        $('#txtEmployeeId').addClass("error");
        return false;
    }
    else if ($('#txtEmployeeName').val().trim() == "") {
        alert('Please enter employee name.');
        $('#txtEmployeeName').addClass("error");
        return false;
    }
    else if ($('#txtMobileNo').val().trim() == "") {
        alert('Please enter mobile number.');
        $('#txtMobileNo').addClass("error");
        return false;
    }
    else if ($('#txtMobileNo').val().trim().length < 10) {
        alert('Please enter valid mobile number.');
        $('#txtMobileNo').addClass("error");
        return false;
    }
    else if ($('#txtEmailId').val().trim() == "") {
        alert('Please enter email address.');
        $('#txtEmailId').addClass("error");
        return false;
    }
    else if ($('#txtHomeAddress').val().trim() == "") {
        alert('Please enter home address.');
        $('#txtHomeAddress').addClass("error");
        return false;
    }
    else if ($('#txtRemarks').val().trim() == "") {
        alert('Please enter remarks.');
        $('#txtRemarks').addClass("error");
        return false;
    }
    else {
        var result = true;
        for (iCount = 0; iCount < iCntrlCount; iCount++) {
            $('#ddlRouteTypeControl_' + iCount).removeClass("error");
            $('#ddlCarTypeControl_' + iCount).removeClass("error");
            $('#ddlRentalCity_' + iCount).removeClass("error");
            $('#RentalCityNameOther_' + iCount).removeClass("error");
            $('#ddlSourceControl_' + iCount).removeClass("error");
            $('#ddlDestinationControl_' + iCount).removeClass("error");
            $('#ReportingDate_' + iCount).removeClass("error");
            $('#SourceOtherName_' + iCount).removeClass("error");
            $('#DestinationOtherName_' + iCount).removeClass("error");
            $('#CarTypeNameOther_' + iCount).removeClass("error");

        }
        for (iCount = 0; iCount < iCntrlCount; iCount++) {
            if ($('#divRoute_' + iCount) != undefined && $('#divRoute_' + iCount) != null) {
                if ($('#ddlRouteTypeControl_' + iCount).val() == "0") {
                    alert('Please select route type.');
                    $('#ddlRouteTypeControl_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if ($('#ddlCarTypeControl_' + iCount).val() == "0") {
                    alert('Please select car type.');
                    $('#ddlCarTypeControl_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if (($('#ddlCarTypeControl_' + iCount).val() == "Other" || $('#ddlCarTypeControl_' + iCount).val() == "SUV") && $('#CarTypeNameOther_' + iCount).val().trim() == "") {
                    alert('Please specify other car type.');
                    $('#CarTypeNameOther_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if ($('#ddlRentalCity_' + iCount).val() == "0") {
                    alert('Please select rental city.');
                    $('#ddlRentalCity_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if ($('#ddlRentalCity_' + iCount).val() == "Other" && $('#RentalCityNameOther_' + iCount).val().trim() == "") {
                    alert('Please enter rental city name.');
                    $('#RentalCityNameOther_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if ($('#ReportingDate_' + iCount).val().trim() == "") {
                    alert('Please enter pickup date.');
                    $('#ReportingDate_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if ($('#ddlSourceControl_' + iCount).val() == "0") {
                    alert('Please select source.');
                    $('#ddlSourceControl_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if ($('#ddlSourceControl_' + iCount).val() == "Other" && $('#SourceOtherName_' + iCount).val().trim() == "") {
                    alert('Please enter source name.');
                    $('#SourceOtherName_' + iCount).addClass("error");
                    result = false;
                    break;
                }
                else if ($('#ddlDestinationControl_' + iCount).val() == "Other" && $('#DestinationOtherName_' + iCount).val().trim() == "") {
                    alert('Please enter destination name.');
                    $('#DestinationOtherName_' + iCount).addClass("error");
                    result = false;
                    break;
                }

            }
        }// For loop end

        return result;
    }
    return true;
}
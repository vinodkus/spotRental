var _defFromDate, _defToDate;
var urlPref;
$(document).ready(function () {
    $(function () {
        $('.datepicker').datepicker({
            format: 'mm-dd-yyyy',
            autoclose: true
        });
    });
    $('#btnSearch').click(function () {
        searchRequest();
    });
    $('#btnCancel').click(function () {
        cancleTrip();
    });
    $('#txtFromDate').val(_defFromDate);
    $('#txtToDate').val(_defToDate);
    bindTripStatus();
});
var bindTripStatus = function () {
    $.ajax({
        url: urlPref +  '/Common/GetTripStatusList',
        type: "GET",
        data: { 'DisplayFor': 'ALL' },
        contenttype: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            var listitems = '<option value=0>--Select--</option>'
            if (data.Result == true) {
              
                $.each(data.List.listTripStatus, function (key, value) {
                    listitems += '<option value=' + "'" + value.StatusCode + "'" + '>' + value.StatusName + '</option>';
                });
                listitems += '<option value=-1>ALL</option>'

            }
            else
            {
                listitems += '<option value=-1>ALL</option>'
            }

            $('#ddlStatus').empty();
            $('#ddlStatus').append(listitems);
        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });
}
var searchRequest = function () {
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    var statusCode = $('#ddlStatus').val();
    var emailID = $('#txtEmailId').val();
    if (fromDate == "")
    {
        alert('Please select [FROM] date.');
        return false;
    }
    else if (toDate == "") {
        alert('Please select [TO] date.');
        return false;
    }
    else {
        $.ajax({
            url: urlPref +  '/UserBucket/GetAdhocRequestList',
            type: "GET",
            data: { 'FromDate': fromDate, 'ToDate': toDate, 'EmailID': emailID, 'StatusCode': statusCode },
            contenttype: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                if (data) {
                    $('#searchResult').html('');
                    $('#searchResult').html(data);
                }
                else
                {
                    $('#searchResult').html('');
                }
            },
            error: function (err) {
                alert("Error::Not Responding");
            }
        });
    }
}
var cancellTripConf = function (reqID) {
    if (confirm('Are you sure to cancel this trip?')) {
        $('#divCancelTrip').modal('toggle');
        bindReason('SPOT', '#ddlReason');
        $('#hdnRequestID').val(reqID);
    }
}
var cancleTrip = function () {
    var reasonId = $('#ddlReason').val();
    var reamrks = $('#txtRemarks').val();
    var reqID = $('#hdnRequestID').val();
    if (reasonId == "" || reasonId == "0") {
        alert('Please select reason to cancel this trip.');
        return false;
    }
    else {
        $.ajax({
            url: urlPref +  '/UserBucket/CancelTrip',
            type: "GET",
            data: { 'ReqID': reqID, 'StatusCode': 'CN', 'ReasonID': reasonId, 'Remarks': reamrks },
            contenttype: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                if (data.Result == true) {
                    alert('Trip cancelled successfully.');
                    $('#hdnRequestID').val('0');
                    $('#txtRemarks').val('');
                    $('#divCancelTrip').modal('hide');
                    searchRequest();
                }
                else {
                    alert('There are some issue in cancelling trip.');
                }
            },
            error: function (err) {
                alert("Error::Not Responding");
            }
        });
    }
}

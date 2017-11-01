/*--------------------------------------------------------------------------------------  
 *------------------------SPOT RENTAL manageUsersList.js-----------------------------------------
 *  
 * manageUsersList.js is jquery file manage roles functionalitis.
 *
 * @Author      Vinod Kumar 
 * @Email      <vkumar@ictrlbiz.com>
 * @Copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/

var urlPref;
$(document).ready(function () {
    bindAllLocationItems();
    bindDataTablesProp();
    $('#btnAddLocation').on('click', function () { $('#btnAddLocation').text().trim() == 'Save Location' ? addLocationDetails() : updateLocationDetails(); });
    $(document).on('keydown', '#txtLocationCode,#txtShortName', function (e) {
        debugger;
        if ((e.keyCode >= 65 && e.keyCode <= 90) || (e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode == 20) || (e.keyCode == 16) || (e.keyCode == 8) || (e.keyCode == 46))
            return true
        else
            return false;
        
    });

    $(document).on('keydown', '#txtEmailId', function (e) {
        if (e.keyCode == 32) return false;
    });    
});
var blockSpecialChar =function(e) {
    var k = e.keyCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));
}
var ValidateEmail = function () {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test($('#txtEmailId').val().trim())) {
        return (true)
    }
    alert("You have entered an invalid email address!")
    return (false)
}
var bindDataTablesProp = function () {
    $(function () {
        $('#tblLocations').dataTable({
            stateSave: true,
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': 'no-sort'
            }]
        });
    });
}
var bindAllLocationItems = function () {
    $('.enableLoader').show();
    $.ajax({
        type: "POST",
        url: urlPref + '/Access/BindLocation',
        success: function (data) {
            $('#divLocationList').html('');
            $('#divLocationList').html(data);
            bindDataTablesProp();
            $('.enableLoader').hide();
        },
        error: function (xhr) {
            $('.enableLoader').hide();
            alert('Error: No data found!');
        }
    });
}
var showPopupForEditLocation = function (LocationCode, LocationName, ShortName, City, EmailId, Active, Visible) {

    $('#divAddEditLocation').modal('toggle');
    $('#btnAddLocation').text('Update Location');
    $('#heading').text('Update Location')
    $('#hdnLocationCodeForUpdate').val(LocationCode);
    $('#txtLocationCode').val(LocationCode);
    $('#txtLocationCode').prop('readonly', 'readonly');
    $('#txtLocationName').val(LocationName);
    $('#txtShortName').val(ShortName);
    $('#txtCity').val(City);
    $('#txtEmailId').val(EmailId);
    isLocationVisible == 1 ? $('#cbVisible').bootstrapToggle('on') : $('#cbVisible').bootstrapToggle('off');
    LocationActive == 1 ? $('#cbStatus').bootstrapToggle('on') : $('#cbStatus').bootstrapToggle('off');
}
var showPopupForAddLocation = function () {
    $('#divAddEditLocation').modal('toggle');
    $('#heading').text('Add Location')
    $('#btnAddLocation').text('Save Location');
    $('#txtLocationCode').removeAttr('readonly');

    resetFields();
}
var addLocationDetails = function () {
    if (validateLocationDetails()) {
        var LocationCode = $('#txtLocationCode').val();
        var LocationName = $('#txtLocationName').val();
        var ShortName = $('#txtShortName').val();
        var City = $('#txtCity').val();
        var EmailId = $('#txtEmailId').val();
        var Visible = $("#cbVisible").is(":checked") == true ? 'Y' : 'N';
        var Status = $("#cbStatus").is(":checked") == true ? 'Y' : 'N';
        $.ajax({
            type: "POST",
            data: { 'LocationCode': LocationCode, 'LocationName': LocationName, 'ShortName': ShortName, 'City': City, 'Status': Status, 'Visible': Visible, 'EmailId': EmailId },
            url: urlPref + '/Access/AddNewLocation',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Location added successfully!');
                    $('#divAddEditLocation').modal('hide');
                    $('#hdnLocationCodeForUpdate').val('0');
                    bindAllLocationItems();
                }
                else {
                    alert('' + data.Result);
                }
            },
            error: function (xhr) {


                alert('Error: No data found!');
            }
        });
    }
}
var updateLocationDetails = function () {
    if (validateLocationDetails()) {
        var LocationCode = $('#txtLocationCode').val();
        var LocationName = $('#txtLocationName').val();
        var ShortName = $('#txtShortName').val();
        var City = $('#txtCity').val();
        var EmailId = $('#txtEmailId').val();
        var Visible = $("#cbVisible").is(":checked") == true ? "Y" : "N";
        var Status = $("#cbStatus").is(":checked") == true ? "Y" : "N";


        $.ajax({
            type: "POST",
            data: { 'LocationCode': LocationCode, 'LocationName': LocationName, 'ShortName': ShortName, 'City': City, 'Status': Status, 'Visible': Visible, 'EmailId': EmailId },
            url: urlPref + '/Access/UpdateNewLocation',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Location updated successfully!');
                    $('#divAddEditLocation').modal('hide');
                    $('#hdnLocationCodeForUpdate').val('0');
                    bindAllLocationItems();
                }
                else {
                    alert('' + data.Result);
                }
            },
            error: function (xhr) {

                //console.log('xhr ' + xhr);
                alert('Error: No data found!');
            }
        });
    }
}
var validateLocationDetails = function () {
    if ($('#txtLocationCode').val().trim() == '') {
        alert('Please enter location code!');
        $('#txtLocationCode').focus();
        return false;
    }
    else if ($('#txtLocationName').val().trim() == '') {
        alert('Please enter location name!');
        $('#txtLocationName').focus();
        return false;
    }
    else if ($('#txtShortName').val().trim() == '') {
        alert('Please enter chort name!');
        $('#txtShortName').focus();
        return false;
    }
    else if ($('#txtCity').val().trim() == '') {
        alert('Please enter city name!');
        $('#txtCity').focus();
        return false;
    }
    else if ($('#txtEmailId').val().trim() == '') {
        alert('Please enter email id!');
        $('#txtEmailId').focus();
        return false;
    }
    else if ($('#txtEmailId').val().trim().length > 0)
    {
        if (ValidateEmail()) {
            return true;
        }
        else {
            return false;
        }
    }
    return true;
}
var resetFields = function () {
    $('#txtLocationCode').val('');
    $('#txtLocationName').val('');
    $('#txtShortName').val('');
    $('#txtCity').val('');
    $('#txtEmailId').val('');
    $('#cbVisible').bootstrapToggle('on')
    $('#cbStatus').bootstrapToggle('on')
}

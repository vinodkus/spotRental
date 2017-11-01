/*--------------------------------------------------------------------------------------  
 *------------------------SPOT RENTAL manageUsersList.js-----------------------------------------
 *  
 * manageUsersList.js is jquery file manage roles functionalitis.
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @Copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/

var urlPref;
$(document).ready(function () {
    bindAllUsers();
    bindDataTablesProp();
    $('#btnUpdate').on('click', function () { if ($('#hdnUserIdForUpdate').val() == "0") { AddUser(); } else { updateUser(); } });
    checkNumeric('#MobileNo');
});
var bindDataTablesProp = function () {
    $(function () {
        $('#tblUsers').dataTable({
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': 'no-sort'
            }]
        });
    });
}
var bindAllUsers = function () {
    $('.enableLoader').show();
    $.ajax({
        type: "POST",
        url: urlPref + '/Access/BindUsers',
        success: function (data) {
            $('#divUserList').html('');
            $('#divUserList').html(data);
            $('[data-toggle="tooltip"]').tooltip();
            bindDataTablesProp();
            $('.enableLoader').hide();
        },
        error: function (xhr) {
            $('.enableLoader').hide();
            alert('Error: No data found!');
        }
    });
}
var resetPassword = function (emailID) {
    swal({
        title: "Want to reset password?",
        text: "Current password for this user will be reseted!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, reset it!",
        cancelButtonText: "No, cancel please!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
function (isConfirm) {
    if (isConfirm) {
        $('.enableLoader').show();
        $.ajax({
            type: "GET",
            data: { 'EmailID': emailID },
            url: urlPref + '/Access/ResetPassword',
            success: function (data) {
                $('.enableLoader').hide();
                if (data.Result == true) {
                    swal("Done!", "Password has been reseted and send on registered email.", "success");
                }
                else if (data.Message) {
                    swal("Here's a message!", data.Message);
                }
                else {
                    swal("Here's a message..", "Connection lost with the server!!");
                }
            },
            error: function (xhr) {
                $('.enableLoader').hide();
                swal("Error!!", "Unable to connect.");
            }
        });

    }
    else {
        swal("Cancelled", "Password could not be reseted.", "error");
    }
});
}
var showPopupForAdd = function () {
    $('#divAddEditUser').modal('toggle');
    $('#hdnUserIdForUpdate').val('0');
    $('#UserName').val('');
    $('#UserName').attr('disabled', false);
    $('#EmailID').val('');
    $('#MobileNo').val('');

    $('#Address').val('');
    bindBaseOffice('#ddlLocation');
    bindAllVendors('#ddlVendor');
    $("#divRoles").html('');
    var allRoles = getAllRolesAsString();
    arr = allRoles.split('|');
    $.each(arr, function (n, val) {
        rArr = val.split('-');
        if (rArr.length == 2) {
            $("#divRoles").append('<input type="checkbox" onchange=showVendors("rbtn_' + rArr[0] + '") id="rbtn_' + rArr[0] + '"  value="' + rArr[1] + '" >' + rArr[1] + '</input> &nbsp;');
        }
    });
    $('#divName').css('display', 'none');
    $('#divData').css('display', 'none');
}
var showPopupForEdit = function (userID, userName, emailID, officeLocation, gender, mobileNo, address, vendorID, userRoleNames) {
    $('#divAddEditUser').modal('toggle');
    $('#hdnUserIdForUpdate').val(userID);
    bindBaseOffice('#ddlLocation');
    $('#UserName').val(userName);
    $('#UserName').attr('disabled', true);
    $('#EmailID').val(emailID);
    $('#MobileNo').val(mobileNo);
    $('#ddlLocation').val(officeLocation);
    $('#Address').val(address);
    if (vendorID != undefined && vendorID != null && vendorID != 0) {
        bindAllVendors('#ddlVendor');
        $('#ddlVendor').val(vendorID);
    }
    //-Genrate all available active roles//
    $("#divRoles").html('');
    var allRoles = getAllRolesAsString();
    arr = allRoles.split('|');
    $.each(arr, function (n, val) {
        rArr = val.split('-');
        if (rArr.length == 2) {
            $("#divRoles").append('<input type="checkbox" onchange=showVendors("rbtn_' + rArr[0] + '") id="rbtn_' + rArr[0] + '"  value="' + rArr[1] + '" >' + rArr[1] + '</input> &nbsp;');
        }
    });
    var arrUserRoles = userRoleNames.split('$');
    $.each(arrUserRoles, function (n, val) {

        $('input:checkbox[value="' + val + '"][id*=rbtn_]').prop("checked", true);
        if (val == 'Vendor') {
            $('#divName').css('display', 'block');
            $('#divData').css('display', 'block');
        }

    });


}
var showVendors = function (e) {
    //Hide show vendor div............
    if ($('#' + e).is(':checked') && $('#' + e).val() == 'Vendor') {
        $('#divName').css('display', 'block');
        $('#divData').css('display', 'block');
    }
    else if (!$('#' + e).is(':checked') && $('#' + e).val() == 'Vendor') {
        $('#divName').css('display', 'none');
        $('#divData').css('display', 'none');
    }
}
var getCheckedRoleIds = function () {
    var RoleIds = "";
    $('input[type=checkbox][id*=rbtn_]').each(function () {
        RoleIds += (this.checked ? this.id.split('_')[1] + "," : "");
    });

    return RoleIds;
}
var updateUser = function () {

    var RoleIds = getCheckedRoleIds();
    if (validateUser(RoleIds)) {
        var req = {
            'UserID': $('#hdnUserIdForUpdate').val(),
            'EmailID': $('#EmailID').val(),
            'MobileNo': $('#MobileNo').val(),
            'OfficeLocation': $('#ddlLocation').val(),
            'Gender': $('#cbGender').is(":checked") == true ? "M" : "F",
            'HomeAddress': $('#Address').val(),
            'VendorID': $('#ddlVendor').val(),
            'RoleIds': RoleIds,
        };

        $('.enableLoader').show();
        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref + '/Access/UpdatePortalUser',
            success: function (data) {
                $('.enableLoader').hide();
                if (data.Result == true) {
                    alert(data.Message);
                    bindAllUsers();
                }

            },
            error: function (xhr) {
                $('.enableLoader').hide();
                swal("Error!!", "Unable to connect.");
            }
        });
    }
}
var AddUser = function () {

    var RoleIds = getCheckedRoleIds();
    if (validateUser(RoleIds)) {
        var req = {
            'UserName': $('#UserName').val(),
            'EmailID': $('#EmailID').val(),
            'MobileNo': $('#MobileNo').val(),
            'OfficeLocation': $('#ddlLocation').val(),
            'Gender': $('#cbGender').is(":checked") == true ? "M" : "F",
            'HomeAddress': $('#Address').val(),
            'VendorID': $('#ddlVendor').val(),
            'RoleIds': RoleIds,
        };

        $('.enableLoader').show();
        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref + '/Access/AddPortalUser',
            success: function (data) {
                $('.enableLoader').hide();
                if (data.Result == true) {
                    alert(data.Message);
                    bindAllUsers();
                }

            },
            error: function (xhr) {
                $('.enableLoader').hide();
                swal("Error!!", "Unable to connect.");
            }
        });
    }
}
var validateUser = function (RoleIds) {
    if ($('#UserName').val().trim() == "") {
        alert('Please enter user name.');
        $('#UserName').focus();
        return false;
    }
    else if ($('#UserName').val().trim().length < 5) {
        alert('Invalid user name.');
        $('#UserName').focus();
        return false;
    }
    else if (!validateEmail($('#EmailID').val().trim())) {
        alert('Please enter valid email id.');
        $('#EmailID').focus();
        return false;
    }
    else if (!validateMobile($('#MobileNo').val().trim())) {
        alert('Please enter valid mobile no.');
        $('#MobileNo').focus();
        return false;
    }
    else if ($('#ddlLocation').val() == "0") {
        alert('Please select office location.');
        $('#ddlLocation').focus();
        return false;
    }
    else if ($('#Address').val().trim().length < 10) {
        alert('Please enter valid address.');
        $('#Address').focus();
        return false;
    }
    else if (RoleIds.trim() == "") {
        alert('Please select atleast one role.');
        return false;
    }
    return true;
}

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
                else {
                    swal("Here's a message!", data.Message);
                }
            },
            error: function (xhr) {
                $('.enableLoader').hide();
                swal("Error!!", "Unable to connect.");
            }
        });
        swal("Done!", "Password has been reseted and send on registered email.", "success");
    }
    else {
        swal("Cancelled", "Password could not be reseted.", "error");
    }
});
}
var showPopupForEdit=function(userID,UserName,EmailID,OfficeLocation,Gender,MobileNo)
{
    $('#divAddEditUser').modal('toggle');   
    $('#hdnUserIdForUpdate').val(userID);
    bindBaseOffice('#ddlLocation');


    $('#UserName').val(UserName);
    $('#EmailID').val(EmailID);
    $('#MobileNo').val(MobileNo);
    $('#ddlLocation').val(OfficeLocation);
    $('#Address').val(userID);
   
}
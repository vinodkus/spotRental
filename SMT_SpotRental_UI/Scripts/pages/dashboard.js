var tempPass;
var urlPref;
$(document).ready(function () {
    if (tempPass != null && tempPass != 'undefined' && tempPass == 'True') {
        $('#divChangePassword').modal('toggle');
    }
    $('#btnChangePassword').click(function () {
        changePassword();
    });
});
var openChngPWDPopup=function()
{
    $('#divChangePassword').modal('toggle');
}
var changePassword = function () {

    if (validatePassword()) {
        var pwd = $("#txtNewPassword").val().trim();
        $.ajax({
            cache: false,
            type: "GET",
            url: urlPref +  '/Dashboard/ChangePassword',
            data: { 'strPwd': pwd },
            dataType: "json",
            success: function (data) {
                if (data.Result == true) {
                    alert('Password has been changed successfully.');
                    $('#divChangePassword').modal('toggle');
                }
                else {
                    alert(data.Message);
                }
            },
            error: function (xhr) {
                alert("Critical Error!. Failed to call the server.");
            }
        });
    }
}
var validatePassword = function () {
    var pwd = $("#txtNewPassword").val().trim();
    var confPass = $("#txtConfirmPassword").val().trim();
    if (pwd == '') {
        alert('Please enter new password');
        return false;
    }
    if (pwd.length < 6) {
        alert('New password length must be greater than or equal to 6');
        return false;
    }
    else if (confPass == '') {
        alert('Please  confirm your password');
        return false;
    }
    else if (pwd != confPass) {
        alert('Your new password and confirm password do not matched');
        return false;
    }
    return true;
}
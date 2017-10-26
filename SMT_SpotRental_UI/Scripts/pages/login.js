/*--------------------------------------------------------------------------------------  
 * -------------------------SPOT RENTAL login.js----------------------------------------
 *  
 *login.js is jquery file to use for login functionality of application
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/
var urlPref;
$(document).ready(function () {
   
    $("#btnSearchSupervisor").click(function () {
        getSupervisorList();
    });
    $("#btnEmployeeRegister").click(function () {
        registerEmployee();
       
    });
    $("#btnSendPassword").click(function () {
        forgetPassword();
    });
    $("#anEmpReg").click(function () {
        openEmployeeRegiPopup();
    });
    
});
var openEmployeeRegiPopup=function()
{
    $('#divEmployeeRegistration').modal('show');
    bindDesignation('#Designation');
    bindBaseOffice('#BaseOffice');
}
var enableDisableSearchCriteria = function (cntlID, txtID) {
    $(txtID).val('');
    $(txtID).prop("disabled", !$('#' + cntlID).prop('checked'));
}
var getSupervisorList = function () {
    if (validateSupervisorSearch()) {
        $.ajax({
            url: urlPref + '/Common/SupervisorList',
            type: "POST",
            data: { 'FName': $('#txtFName_S').val(), 'LName': $('#txtLName_S').val(), 'MobileNo': $('#txtMobileNo_S').val() },
            success: function (res) {
                $("#divShowList").html(res);
            },
            error: function (err) {
                alert('Error');
            }
        });
    }
}
var validateSupervisorSearch = function () {
    if ($('#txtFName_S').val().trim() == '' && $('#txtLName_S').val().trim() == '' && $('#txtMobileNo_S').val().trim() == '') {
        alert('Please enter atleast one criteria.');
        return false;
    }
    else if ($('#cbFName').prop('checked') && $('#txtFName_S').val().trim() == '') {
        alert('Please enter first name.');
        return false;
    }
    else if ($('#cbLName').prop('checked') && $('#txtLName_S').val().trim() == '') {
        alert('Please enter last name.');
        return false;
    }
    else if ($('#cbMobile').prop('checked') && $('#txtMobileNo_S').val().trim() == '') {
        alert('Please enter mobile number.');
        return false;
    }
    else {
        return true;
    }
}
var fillSupervisorID = function (empID) {
    $("#divShowList").html('');
    $("#divSearchSupervisor").modal('toggle');
    $("#txtSupervisorID").val(empID);
}
var registerEmployee = function () {
    if (validateEmployee()) {
        var empModel = {
            FName: $.trim($("#txtFName").val()),
            MName: $.trim($("#txtMName").val()),
            LName: $.trim($("#txtLName").val()),
            EmployeeCode: $.trim($("#txtEmployeeCode").val()),
            MobileNo: $.trim($("#txtMobileNo").val()),
            EmailID: $.trim($("#txtEmailId").val()),
            Gender: $.trim($("#DDLGender").val()),
            OfficeLocation: $.trim($("#BaseOffice").val()),
            DesignationID: $.trim($("#Designation").val()),
            SupervisorID: $.trim($("#txtSupervisorID").val()),
            CostCenter: $.trim($("#txtCostCenter").val()),
            HomeAddress: $.trim($("#txtHomeAddress").val()),
            CreditCard: $.trim($("#txtCreditCard").val())
        };
        $('.enableLoader').show();
        $.ajax({
            cache: false,
            type: "POST",
            url: urlPref + '/Account/AddEmployee',
            data: empModel,
            dataType: "json",
            success: function (data) {
                $('.enableLoader').hide();
                if (data.IsDuplicate) {
                    alert('Message: ' + data.Message);
                }
                else if (data.Result == true) {
                    alert('Message: ' + data.Message);
                }
                else {
                    alert('Message: ' + data.Message);
                }
            },
            error: function (xhr) {
                $('.enableLoader').hide();
                alert("Critical Error!. Failed to call the server.");
            }
        });
    }
}
var validateEmployee = function () {
    if ($.trim($("#txtFName").val()) == '') {
        alert("Please enter first name.");
        $("#txtFName").focus();
        return false;
    }
    else if ($.trim($("#txtLName").val()) == '') {
        alert("Please enter last name.");
        $("#txtLName").focus();
        return false;
    }
    else if ($.trim($("#txtEmployeeCode").val()) == '') {
        alert("Please enter employee code.");
        $("#txtEmployeeCode").focus();
        return false;
    }
    else if ($.trim($("#txtMobileNo").val()) == '') {
        alert("Please enter mobile number.");
        $("#txtMobileNo").focus();
        return false;
    }
    else if (validateMobile(($.trim($("#txtMobileNo").val()))) == false) {
        alert("Please enter valid mobile number.");
        $("#txtMobileNo").focus();
        return false;
    }
    else if ($.trim($("#DDLGender").val()) == '0') {
        alert("Please select gender.");
        $("#DDLGender").focus();
        return false;
    }
    else if ($.trim($("#txtEmailId").val()) == '') {
        alert("Please enter email id.");
        $("#txtEmailId").focus();
        return false;
    }
    else if (validateEmail(($.trim($("#txtEmailId").val()))) == false) {
        alert("Please enter valid email id.");
        $("#txtEmailId").focus();
        return false;
    }
    else if ($.trim($("#BaseOffice").val()) == '0') {
        alert("Please select base office/location.");
        $("#BaseOffice").focus();
        return false;
    }
    else if ($.trim($("#txtCreditCard").val()) == '') {
        alert("Please enter credit card details.");
        $("#txtCreditCard").focus();
        return false;
    }
    else if ($.trim($("#Designation").val()) == '') {
        alert("Please select designation.");
        $("#Designation").focus();
        return false;
    }
    else if ($.trim($("#txtSupervisorID").val()) == '') {
        alert("Please enter supervisor id.");
        $("#Designation").focus();
        return false;
    }
    else if ($.trim($("#txtHomeAddress").val()) == '') {
        alert("Please enter home address.");
        $("#txtHomeAddress").focus();
        return false;
    }
    else {
        return true;
    }
}
var forgetPassword = function () {
    var empIdEmailId = $("#txtEmailID_FP").val().trim();
    if (empIdEmailId != "" || empIdEmailId > 4) {
        $.ajax({
            cache: false,
            type: "GET",
            url: urlPref + '/Account/ForgetPassword',
            data: { 'loginCredentials': empIdEmailId },
            dataType: "json",
            success: function (data) {
                if (data.Result == true) {
                    alert('We have send your password to your registered email id.');
                    $('#divForgetPassword').modal('toggle');
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
    else {
        alert("Please enter valid email Id/employee id to get your password.");
    }
}
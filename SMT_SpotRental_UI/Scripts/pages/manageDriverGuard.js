/*--------------------------------------------------------------------------------------  
 * -------------------------SPOT RENTAL # manageDriverGuard.js----------------------------------------
 *  
 *manageDriverGuard.js is jquery file to handle all functionalies related to vendors like add,
 * edit, view and validate 
 *
 * @Author      Vinod Kumar 
 * @Email      <vkumar@ictrlbiz.com>
 * @copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/
var urlPref;
$(document).ready(function () {
    bindBaseOffice('#ddlSite');
    bindAllVendors('#ddlVendorCode');
    $('#btnSearch').on('click', function () { searchDriverGuard(); });
    //$('#btnAdd').on('click', function () { $('#hdnVendorIDToUpdate').val('0'); $('#btnAddVendor').text('Add Vendor'); $('#addVendor').text('Add/Register Vendor'); openModelToAddVendor(); });
    $('#btnAdd').on('click', function () { if (checkBaseDriverGuard()) { openModelToAddDriverGuard(); } });
    $('#btnAddDriverGuard').on('click', function () { $('#btnAddDriverGuard').text().trim() == 'Add Driver/Guard' ? addDriverGuardDetails() : updateDriverGuardDetails(); });

    $('#btnReset').on('click', function () { resetFields(); })

    $(document).on('keydown', '#txtFirstName', function (e) {
        if (e.keyCode == 32) return false;
    });
    $(document).on('keydown', '#txtLastName', function (e) {
        if (e.keyCode == 32) return false;
    });
    $(document).on('keydown', '#txtFatherName', function (e) {
        if (e.keyCode == 32) return false;
    });

});
var checkBaseDriverGuard = function () {
    if ($('#ddlSite').val() == "0")
    {
        alert('Please select site!');
        $('#ddlLocation').focus();
        return false;
    }
    else if ($('#ddlVendorCode').val() == "0") {
        alert('Please select vendor!');
        $('#ddlVendorCode').focus();
        return false;
    }
    return true;
}
var openModelToAddDriverGuard = function () {
    $('#heading').text('Add/Register Driver/Guard');
    $('#btnAddDriverGuard').text('Add Driver/Guard');
    resetFields();
    $('#divAddUpdateDriverGuard').modal('show');
    $(function () {
        $('.datepicker').datepicker({
            format: 'mm-dd-yyyy',
            startDate: '-0d',
            autoclose: true
        });
        //$('.datepicker').datepicker({ // Back date
        //    format: 'mm-dd-yyyy', 
        //    endDate: 'd',
        //    autoclose: true
        //});
        bindAllVendors('#ddlVendor');
        $('#ddlVendor').val($("#ddlVendorCode").val());
        $('#ddlVendor').prop("disabled", "disabled");
        
    });   
    checkNumeric('#txtPINCode');
    checkNumeric('#txtContactNumber');  
}
var openModelToUpdateDriverGuard = function (driverGuardId) {    
    var empType = $('#hdnEmpType_' + driverGuardId).val();
    var dg = empType == "D" ? "Driver" : "Guard";
    $('#heading').text('Update ' + dg);    
    $('#btnAddDriverGuard').text('Update '+dg);
    $('#divAddUpdateDriverGuard').modal('show');
    $(function () {
        $('.datepicker').datepicker({
            format: 'mm-dd-yyyy',
            startDate: '-0d',
            autoclose: true
        });
        bindAllVendors('#ddlVendor');
        $('#ddlVendor').val($("#ddlVendorCode").val());
        $('#ddlVendor').prop("disabled", "disabled");
        
    });
    bindFieldsForUpdate(driverGuardId);
    checkNumeric('#txtPINCode');
    checkNumeric('#txtContactNumber');
}
var deleteDriverGuardId;
var DriverGuard;
var openModelToDeleteDriverGuard = function (driverGuardId,empType,fullName) {
    
    deleteDriverGuardId = driverGuardId;
    $('#divDeleteDriverGuard').modal('show');
    DriverGuard = $.trim(empType) == "D" ? "Driver" : "Guard";
    var msg = DriverGuard + ' of ID:' + driverGuardId+'';
    $('#spanDriverGuardId').text(msg);
    $('#spanDriverGuard').text(DriverGuard);
    $('#spanDriverGuardFullName').text(fullName);
}
var deleteDriverGuard = function () {
    var req =
            {
                'DriverGuardId': deleteDriverGuardId
            };

    $.ajax({
        type: "POST",
        data: { 'request': req },
        url: urlPref + '/MasterItems/DeleteDriverGuard',
        success: function (data) {
            if (data && data.Result == "TRUE") {
                alert(DriverGuard+ ' deleted successfully!');
                searchDriverGuard();
            }
            else {
                alert(data.Result);
            }
        },
        error: function (xhr) {
            alert('Error: unable to delete ' + DriverGuard + ' details. Please try after some time!');
        },
        complete: function () {
            $('#divDeleteDriverGuard').modal('toggle');
        }
    });

}
var bindFieldsForUpdate = function (driverGuardId) {
    $('#hdnLocalCode').val($('#hdnLocCode_' + driverGuardId).val());
    $('#txtFirstName').val($('#hdnFirstName_' + driverGuardId).val());
    $('#txtLastName').val($('#hdnLastName_' + driverGuardId).val());
    $('#txtFatherName').val($('#hdnFatherName_' + driverGuardId).val());
    $('#txtDOB').val($('#hdnDOB_' + driverGuardId).val() == '01-01-0001' ? '' : $('#hdnDOB_' + driverGuardId).val());
    $('#txtPermanentAddress').val($('#hdnPermanentAddress_' + driverGuardId).val());
    $('#txtPINCode').val($('#hdnPinCode_' + driverGuardId).val());
    $('#txtEmployeeId').val($('#hdnEmpCode_' + driverGuardId).val());
    $('#txtHeight').val($('#hdnHeight_' + driverGuardId).val());
    $('#txtWeight').val($('#hdnWeight_' + driverGuardId).val());
    $('#txtColorSkin').val($('#hdnSkinColor_' + driverGuardId).val());
    $('#ddlMaritalStatus').val($('#hdnMaritalStatus_' + driverGuardId).val() == null || $('#hdnMaritalStatus_' + driverGuardId).val() == '' ? '0' : $('#hdnMaritalStatus_' + driverGuardId).val());
    $('#txtEducation').val($('#hdnEducation_' + driverGuardId).val());
    $('#txtContactPerson').val($('#hdnContactPerson_' + driverGuardId).val());
    $('#txtContactNumber').val($('#hdnMobileNo_' + driverGuardId).val());
    $('#txtPresentAddress').val($('#hdnPresentAddress_' + driverGuardId).val());
    $('#ddlVendor').val($('#hdnVendorId_' + driverGuardId).val() == null || $('#hdnVendorId_' + driverGuardId).val() == '' ? '0' : $('#hdnVendorId_' + driverGuardId).val());
    $('#txtLicenceBeltNumber').val($('#hdnLicenceNo_' + driverGuardId).val());
    $('#txtLicenceBeltExpiryDate').val($('#hdnLicenceBeltExpiryDate_' + driverGuardId).val() == '01-01-0001' ? '' : $('#hdnLicenceBeltExpiryDate_' + driverGuardId).val());
    $('#txtJoiningDate').val($('#hdnDOJ_' + driverGuardId).val() == '01-01-0001' ? '' : $('#hdnDOJ_' + driverGuardId).val());
    $('#txtLeavingDate').val($('#hdnDOL_' + driverGuardId).val() == '01-01-0001' ? '' : $('#hdnDOL_' + driverGuardId).val());
    $('#ddlEmployeeType').val($('#hdnEmpType_' + driverGuardId).val() == null || $('#hdnEmpType_' + driverGuardId).val() == '' ? '0' : $('#hdnEmpType_' + driverGuardId).val());
    $('#txtBadgeNumber').val($('#hdnBadgeNo_' + driverGuardId).val());
    $('#txtBadgeExpiryDate').val($('#hdnBadgeExpiryDate_' + driverGuardId).val() == '01-01-0001' ? '' : $('#hdnBadgeExpiryDate_' + driverGuardId).val());
    $('#hdnFingerPrint_' + driverGuardId).val() == "Y" ? $('#cbFingerPrint').bootstrapToggle('on') : $('#cbFingerPrint').bootstrapToggle('off')
    $('#hdnActive_' + driverGuardId).val() == "Y" ? $('#cbActive').bootstrapToggle('on') : $('#cbActive').bootstrapToggle('off')
    $('#hdnLeftFingerPrint_' + driverGuardId).val() == "Y" ? $('#cbLeftFingerPrint').bootstrapToggle('on') : $('#cbLeftFingerPrint').bootstrapToggle('off')
    $('#hdnRightFingerPrint_' + driverGuardId).val() == "Y" ? $('#cbRightFingerPrint').bootstrapToggle('on') : $('#cbRightFingerPrint').bootstrapToggle('off')
    $('#hdnPoliceVerification_' + driverGuardId).val() == "Y" ? $('#cbpvr').bootstrapToggle('on') : $('#cbpvr').bootstrapToggle('off')
    
    $('#hdnAddVerification_' + driverGuardId).val() == "Y" ? $('#cbavr').bootstrapToggle('on') : $('#cbavr').bootstrapToggle('off')
    $('#hdnDriverGuardID').val($('#hdnDriverGuardId_' + driverGuardId).val());
}
var validateDriverGuardDetails = function () {
    if ($('#txtJoiningDate').val().length > 0 && $('#txtLeavingDate').val().length > 0)
    {
        var doj = new Date($('#txtJoiningDate').val());
        var dol = new Date($('#txtLeavingDate').val());
        if (dol < doj)
        {
            alert('Date of joining should be smaller than Date of Leaving');
            return false;
        }
    }
    if ($('#txtFirstName').val().trim() == '') {
        alert('Please enter first name!');
        $('#txtFirstName').focus();
        return false;
    }
    else if ($('#txtLastName').val().trim() == '') {
        alert('Please enter last name!');
        $('#txtLastName').focus();
        return false;
    }
    else if ($('#txtPermanentAddress').val().trim() == '') {
        alert('Please enter permanent address!');
        $('#txtPermanentAddress').focus();
        return false;
    }
    else if ($('#txtEmployeeId').val().trim() == '') {
        alert('Please enter Employee Id!');
        $('#txtEmployeeId').focus();
        return false;
    }
    else if ($('#txtContactPerson').val().trim() == '') {
        alert('Please enter contact person!');
        $('#txtContactPerson').focus();
        return false;
    }
    else if ($('#txtContactNumber').val().trim() == '') {
        alert('Please enter contact number!');
        $('#txtContactNumber').focus();
        return false;
    }
    else if ($('#txtLicenceBeltNumber').val().trim() == '') {
        alert('Please enter Licence/Belt number!');
        $('#txtLicenceBeltNumber').focus();
        return false;
    }
    else if ($('#txtLicenceBeltExpiryDate').val().trim() == '') {
        alert('Please enter Licence/Belt expiry date!');
        $('#txtLicenceBeltExpiryDate').focus();
        return false;
    }
    else if ($('#ddlEmployeeType').val() == '-1') {
        alert('Please select employee type!');
        $('#ddlEmployeeType').focus();
        return false;
    }
    return true;
}
var addDriverGuardDetails = function () {
    
    if (validateDriverGuardDetails())
    {
        var req = {
            'VendorId': $('#ddlVendor').val(),
            'EmpCode': $('#txtEmployeeId').val(),
            'LocCode': $('#ddlSite').val(),
            'FirstName': $('#txtFirstName').val(),
            'LastName': $('#txtLastName').val(),
            'FatherName': $('#txtFatherName').val(),
            'DOB': $('#txtDOB').val(),
            'Address': $('#txtPermanentAddress').val(),
            'PinCode': $('#txtPINCode').val(),
            'Height': $('#txtHeight').val(),
            'Weight': $('#txtWeight').val(),
            'SkinColor': $('#txtColorSkin').val(),
            'MaritalStatus': $('#ddlMaritalStatus').val(),
            'Education': $('#txtEducation').val(),
            'ContactPerson': $('#txtContactPerson').val(),
            'MobileNo': $('#txtContactNumber').val(),
            'ContactPersonAddress': $('#txtPresentAddress').val(),
            'LicenceNo': $('#txtLicenceBeltNumber').val(),
            'LicenceExpiryDate': $('#txtLicenceBeltExpiryDate').val(),
            'DOJ': $('#txtJoiningDate').val(),
            'DOL': $('#txtLeavingDate').val(),
            'EmpType': $('#ddlEmployeeType').val(),
            'BadgeNo': $('#txtBadgeNumber').val(),
            'BadgeExpiryDate': $('#txtBadgeExpiryDate').val(),
            'FingerPrint': $("#cbFingerPrint").is(":checked") == true ? "Y" : "N",
            'Active': $("#cbActive").is(":checked") == true ? "Y" : "N",
            'LeftFingerPrint': $("#cbLeftFingerPrint").is(":checked") == true ? "Y" : "N",
            'RightFingerPrint': $("#cbRightFingerPrint").is(":checked") == true ? "Y" : "N",
            'PoliceVerification': $("#cbpvr").is(":checked") == true ? "Y" : "N",
            'AddVerification': $("#cbavr").is(":checked") == true ? "Y" : "N"
        };
        var dg = $('#ddlEmployeeType').val() == "D" ? "Driver" : "Guard";
        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref + '/MasterItems/AddDriverGuard',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert(dg+' added successfully!');
                    searchDriverGuard();
                }
                else {
                    alert(data.Result);
                }
            },
            error: function (xhr) {
                alert('Error: unable to add Driver/Guard details. Please try after some time!');
            }
        });
    }
}
var viewDriverGuardDetails = function (driverGuardId) {
    
    $('#divDriverGuardDetails').modal('toggle');
    $('#lblFirstName').text($('#hdnFirstName_' + driverGuardId).val());
    $('#lblLastName').text($('#hdnLastName_' + driverGuardId).val());
    $('#lblLicenceNo').text($('#hdnLicenceNo_' + driverGuardId).val());
    $('#lblStatus').text($('#hdnActive_' + driverGuardId).val() == "Y" ? "Active" : "De-Active");
    $('#lblEmployeeId').text($('#hdnEmpCode_' + driverGuardId).val());
    $('#lblEmployeeType').text($('#hdnEmpType_' + driverGuardId).val()=="D"?"Driver":"Guard");
    $('#lblPermanentAddress').text($('#hdnPermanentAddress_' + driverGuardId).val());

    $('#lblHeight').text($('#hdnHeight_' + driverGuardId).val());
    $('#lblWeight').text($('#hdnWeight_' + driverGuardId).val());
    $('#lblSkinColor').text($('#hdnSkinColor_' + driverGuardId).val());
    $('#lblMaritalStatus').text($('#hdnMaritalStatus_' + driverGuardId).val());
    $('#lblEducation').text($('#hdnEducation_' + driverGuardId).val());
    $('#lblContactPerson').text($('#hdnContactPerson_' + driverGuardId).val());
    $('#lblContactNumber').text($('#hdnMobileNo_' + driverGuardId).val());
    $('#lblPresentAddress').text($('#hdnPresentAddress_' + driverGuardId).val());
    $('#lblLicenceBeltExpiryDate').text($('#hdnLicenceBeltExpiryDate_' + driverGuardId).val());
    $('#lblDOB').text($('#hdnDOB_' + driverGuardId).val());
    $('#lblDOJ').text($('#hdnDOJ_' + driverGuardId).val());
    $('#lblDOL').text($('#hdnDOL_' + driverGuardId).val());
    $('#lblBadgeNumber').text($('#hdnBadgeNo_' + driverGuardId).val());
    $('#lblBadgeExpiryDate').text($('#hdnBadgeExpiryDate_' + driverGuardId).val());
    $('#lblFingerPrint').text($('#hdnFingerPrint_' + driverGuardId).val());    
    $('#lblLeftFingerPrintLegible').text($('#hdnLeftFingerPrint_' + driverGuardId).val());
    $('#lblRightFingerPrintLegible').text($('#hdnRightFingerPrint_' + driverGuardId).val());
    $('#lblPoliceVerificationReceived').text($('#hdnPoliceVerification_' + driverGuardId).val());
    $('#lblAddressVerificationReceived').text($('#hdnAddVerification_' + driverGuardId).val());    
    $('#lblVendorCode').text($('#hdnVendorId_' + driverGuardId).val());

    $('#lblSiteName').text($('#hdnLocationName_' + driverGuardId).val());

    $('#lblVendorName').text($('#hdnVendorName_' + driverGuardId).val());
    var empType = $('#hdnEmpType_' + driverGuardId).val();
    var dg = empType == "D" ? "Driver" : "Guard";
    $('.modal-title').text('View '+dg+' Details')
}
var updateDriverGuardDetails = function () {
    var empType = $('#hdnEmpType_' + $('#hdnDriverGuardID').val()).val();
    var dg = empType == "D" ? "Driver" : "Guard";

    if (validateDriverGuardDetails()) {
        var req = {
            'DriverGuardId': $('#hdnDriverGuardID').val(),
            'VendorId': $('#ddlVendor').val(),
            'EmpCode': $('#txtEmployeeId').val(),
            'LocCode':$('#hdnLocalCode').val(),
            'FirstName': $('#txtFirstName').val(),
            'LastName': $('#txtLastName').val(),
            'FatherName': $('#txtFatherName').val(),
            'DOB': $('#txtDOB').val(),
            'Address': $('#txtPermanentAddress').val(),
            'PinCode': $('#txtPINCode').val(),
            'Height': $('#txtHeight').val(),
            'Weight': $('#txtWeight').val(),
            'SkinColor': $('#txtColorSkin').val(),
            'MaritalStatus': $('#ddlMaritalStatus').val(),
            'Education': $('#txtEducation').val(),
            'ContactPerson': $('#txtContactPerson').val(),
            'MobileNo': $('#txtContactNumber').val(),
            'ContactPersonAddress': $('#txtPresentAddress').val(),
            'LicenceNo': $('#txtLicenceBeltNumber').val(),
            'LicenceExpiryDate': $('#txtLicenceBeltExpiryDate').val(),
            'DOJ': $('#txtJoiningDate').val(),
            'DOL': $('#txtLeavingDate').val(),
            'EmpType': $('#ddlEmployeeType').val(),
            'BadgeNo': $('#txtBadgeNumber').val(),
            'BadgeExpiryDate': $('#txtBadgeExpiryDate').val(),
            'FingerPrint': $("#cbFingerPrint").is(":checked") == true ? "Y" : "N",
            'Active': $("#cbActive").is(":checked") == true ? "Y" : "N",
            'LeftFingerPrint': $("#cbLeftFingerPrint").is(":checked") == true ? "Y" : "N",
            'RightFingerPrint': $("#cbRightFingerPrint").is(":checked") == true ? "Y" : "N",
            'PoliceVerification': $("#cbpvr").is(":checked") == true ? "Y" : "N",
            'AddVerification': $("#cbavr").is(":checked") == true ? "Y" : "N"
        };
        
        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref + '/MasterItems/UpdateDriverGuard',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert(dg+' updated successfully!');
                    searchDriverGuard();
                }
                else {
                    alert(data.Result);
                }
            },
            error: function (xhr) {
                alert('Error: unable to update Driver/Guard details. Please try after some time!');
            }
        });
    }
}
var resetFields = function () {
    
        $('#txtEmployeeId').val('');
        $('#txtFirstName').val('');
        $('#txtLastName').val('');
        $('#txtFatherName').val('');
        $('#txtDOB').val('');
        $('#txtPermanentAddress').val('');
        $('#txtPINCode').val('');
        $('#txtHeight').val('');
        $('#txtWeight').val('');
        $('#txtColorSkin').val('');
        $('#ddlMaritalStatus').val('Single');
        $('#txtEducation').val('');
        $('#txtContactPerson').val('');
        $('#txtContactNumber').val('');
        $('#txtPresentAddress').val('');
        $('#ddlVendorCodeDriverGuard').val('');
        $('#txtLicenceBeltNumber').val('');
        $('#txtLicenceBeltExpiryDate').val('');
        $('#txtJoiningDate').val('');
        $('#txtLeavingDate').val('');
        $('#ddlEmployeeType').val('-1');
        $('#txtBadgeNumber').val('');
        $('#txtBadgeExpiryDate').val('');
        $('#cbFingerPrint').bootstrapToggle('off')
        $('#cbActive').bootstrapToggle('off')
        $('#cbLeftFingerPrint').bootstrapToggle('off')
        $('#cbRightFingerPrint').bootstrapToggle('off')
        $('#cbpvr').bootstrapToggle('off')
        $('#cbavr').bootstrapToggle('off')
}
var searchDriverGuard = function () {
    $.ajax({
        type: "POST",
        data: { 'LocCode': $('#ddlSite').val(), 'VendorCode': $('#ddlVendorCode').val()},
        url: urlPref + '/MasterItems/SearchDriverGuard',
        success: function (data) {
            $('#divInfo').css('display', 'none');
            $('#divDriverGuardList').html('');
            $('#divDriverGuardList').html(data);
        },
        error: function (xhr) {
            alert('Error: No data found!');

        }
    });
}
var openDocumentUploadPopup = function (driverGuardId) {
    
    $('#hdnDriverIdForUploadDocs').val(driverGuardId);
    $('#divUploadDocument').modal('toggle');
    var DriverGuardId = $('#hdnDriverIdForUploadDocs').val();
    getUploadedDocs(0, DriverGuardId);
    // rest actions are in uploadDocuments.js 
}
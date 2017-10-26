/*--------------------------------------------------------------------------------------  
 * -------------------------SPOT RENTAL # manageVehicles.js----------------------------------------
 *  
 *manageVehicles.js is jquery file to handle all functionalies related to vehicle like add,
 * edit, view and validate.  
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/
var urlPref;
$(document).ready(function () {
    bindBaseOffice('#ddlLocation');
    $('#btnSearch').on('click', function () { searchVehicles(); });
    $('#btnAddVehicle').on('click', function () { openModelToAddVendor(); });
    $('#btnAddVehicleDetails').on('click', function () { $('#btnAddVehicleDetails').text().trim() == 'Add Vehicle' ? addVehicleDetails() : updateVehicleDetails(); });
});
var searchVehicles = function () {
    $.ajax({
        type: "POST",
        data: { 'LocCode': $('#ddlLocation').val(), 'VehicleNo': $('#txtVehicleNo').val(), 'RegistrationNo': $('#txtRegistrationNo').val().trim() },
        url: urlPref +  '/MasterItems/SearchVehicles',
        success: function (data) {
            $('#divInfo').css('display', 'none');
            $('#divVehicleList').html('');
            $('#divVehicleList').html(data);
        },
        error: function (xhr) {
            alert('Error: No data found!');

        }
    });
}
var viewVehicleDetails = function (vehicleID) {
    $('#divViewVehicleDetails').modal('toggle');

    $("#lblVehicleNo").text($('#hdnVehicleNo_' + vehicleID).val());
    $("#lblVehicleType").text($('#hdnVehicleType_' + vehicleID).val());
    $("#lblCapacity").text($('#hdnCapacity_' + vehicleID).val());

    $("#lblDriverName").text($('#hdnDriverName_' + vehicleID).val());
    $("#lblStatus").text($('#hdnActive_' + vehicleID).val());
    $("#lblRegistrationNo").text($('#hdnRegistrationNo_' + vehicleID).val());

    $("#lblBillingName").text($('#hdnBillingPlanName_' + vehicleID).val());
    $("#lblLocationName").text($('#hdnLocationName_' + vehicleID).val());
    $("#lblVendorName").text($('#hdnVendorName_' + vehicleID).val());

    $("#lblKMLimit").text($('#hdnKMLimit_' + vehicleID).val());

    $("#lblGPSInstalled").text($('#hdnIsGPSInstalled_' + vehicleID).val());
    $("#lblGPSInstallDate").text($('#hdnGPSInstallDate_' + vehicleID).val());
    $("#lblGPSInstallBy").text($('#hdnGPSInstalledBy_' + vehicleID).val());


    $("#lblSubVendor").text($('#hdnSubVendor_' + vehicleID).val());
    $("#lblRCYear").text($('#hdnRC_Year_' + vehicleID).val());
    $("#lblINSExpiryDate").text($('#INS_Expiry_Date' + vehicleID).val());


    $("#lblFITCRTExpiryDate").text($('#Fit_Crt_Expiry_Date' + vehicleID).val());
    $("#lblPermitExpiryDate").text($('#Permit_Expiry_Date' + vehicleID).val());
    $("#lblEffectiveDate").text($('#hdnEffectiveDate_' + vehicleID).val());


    $("#lblModOn").text($('#hdnModOn_' + vehicleID).val());
    $("#lblCreatedBy").text($('#hdnCreatedBy_' + vehicleID).val());
    $("#lblODOReading").text($('#hdnODOReading_' + vehicleID).val());


    $("#lblRemarks").text($('#hdnRemarks_' + vehicleID).val());
    $("#lblOWNAttached").text($('#hdnOwnAttached_' + vehicleID).val());
    viewVehicleHistoryDetails(vehicleID);
}
var viewVehicleHistoryDetails = function (vehicleID) {
    $.ajax({
        type: "POST",
        cache: false,
        data: { 'VehicleId': vehicleID },
        url: urlPref + '/MasterItems/GetVehicleHistoryDetails',
        success: function (data) {
            $('#divVehicleHistory').html('');
            $('#divVehicleHistory').html(data);
        }
    })
}
var showMoreVehicleRecords = function (moreLink, moreTable) {
  
    var linkId = $('#' + moreLink);
    var moreTableId = $('#' + moreTable);
    if (moreTableId.css("display") == "block") {
        linkId.addClass('glyphicon-plus-sign');
        linkId.removeClass('glyphicon-minus-sign');
        linkId.css("color", "green");
        moreTableId.css("display", "none");
    }

    else {
        linkId.removeClass('glyphicon-plus-sign');
        linkId.addClass('glyphicon-minus-sign');
        linkId.css("color", "red");
        moreTableId.css("display", "block");
    }


}
var openModelToAddVendor = function () {
    resetFields();
    $('#heading').text('Add/Register Vehicle');
    makeFieldDisableEnable(false);
    $('#hdnVehicleID').val('0');
    $('#btnAddVehicleDetails').text('Add Vehicle');
    bindBaseOffice('#ddlLocationForVehicle');
    $('#divAddUpdateVehicle').modal('show');
    $(function () {
        $('.datepicker').datepicker({
            format: 'mm-dd-yyyy',
            startDate: '-0d',
            autoclose: true
        });
    });
    bindVehicleTypes('#ddlVehicleType');
    bindAllVendors('#ddlVendor');

}
var openModelToUpdateVendor = function (vehicleID) {
    $('#heading').text('Update Vehicle');
    bindBaseOffice('#ddlLocationForVehicle');
    $('#divAddUpdateVehicle').modal('show');
    $(function () {
        $('.datepicker').datepicker({
            format: 'mm-dd-yyyy',
            startDate: '-0d',
            autoclose: true
        });
    });
    bindVehicleTypes('#ddlVehicleType');
    bindAllVendors('#ddlVendor');

    bindFieldsForUpdate(vehicleID);
    $('#hdnVehicleID').val(vehicleID);
    $('#btnAddVehicleDetails').text('Update Vehicle');
}
var addVehicleDetails = function () {
    if (validateVehicle()) {
        var req = {
            'LocCode': $('#ddlLocationForVehicle').val(),
            'VehicleTypeID': $('#ddlVehicleType').val(),
            'VendorID': $('#ddlVendor').val(),
            'VehicleNo': $('#txtVehicleNo_V').val(),
            'RegistrationNo': $('#txtRegistrationNo_V').val(),
            'BillingPlanId': $('#ddlBillPlan').val(),
            'EffectiveDate': $('#txtEffectiveDate').val(),
            'INS_Expiry_Date': $('#txtInsuranceDate').val(),
            'ManufacturerDate': $('#txtManufacturerDate').val(),
            'KMLimit': $('#txtKMLimit').val(),
            'SubVendor': $('#txtSubVendor').val(),
            'Active': $('#cbStatus').is(":checked") == true ? "Y" : "N",
            'IsGPSInstalled': $('#cbIsGPSIntalled').is(":checked") == true ? "Y" : "N",
            'GPSInstalledBy': $('#ddlGPSInstalledBy').val(),
            'GPSInstallDate': $('#txtGPSInstalledDate').val(),
            'Fit_Crt_Expiry_Date': $('#txtFitnessCertExpiryDate').val(),
            'RC_Year': $('#txtRCYear').val(),
            'Permit_Expiry_Date': $('#txtPermitExpiryDate').val(),
            'PUC_Expiry_Date': $('#txtPUCExpiryDate').val(),
            'ODO_Reading': $('#txtCurrentOdometer').val(),
            'Own_Attached': $('#cbOwened').is(":checked") == true ? "O" : "A"
        };

        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref +  '/MasterItems/AddVehicle',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('vehicle added successfully!');
                    SearchVendors();
                }
                else {
                    alert(data.Result);
                }
            },
            error: function (xhr) {
                alert('Error: unable to add vehicle details. Please try after some time!');
            }
        });
    }
}
var bindFieldsForUpdate = function (vehicleID) {

    $('#ddlLocationForVehicle').val($('#hdnLocationCode_' + vehicleID).val());
    $('#ddlVehicleType').val($('#hdnVehicleTypeID_' + vehicleID).val());
    $('#ddlVendor').val($('#hdnVendorId_' + vehicleID).val());
    $('#txtVehicleNo_V').val($('#hdnVehicleNo_' + vehicleID).val());
    $('#txtRegistrationNo_V').val($('#hdnRegistrationNo_' + vehicleID).val());
    $('#ddlBillPlan').val($('#hdnBillingPlanId_' + vehicleID).val() == null || $('#hdnBillingPlanId_' + vehicleID).val() == '' ? '0' : $('#hdnBillingPlanId_' + vehicleID).val());
    $('#txtEffectiveDate').val($('#hdnEffectiveDate_' + vehicleID).val() == '01-01-0001' ? '' : $('#hdnEffectiveDate_' + vehicleID).val());
    $('#txtInsuranceDate').val($('#hdnINS_Expiry_Date_' + vehicleID).val() == '01-01-0001' ? '' : $('#hdnINS_Expiry_Date_' + vehicleID).val());
    $('#txtManufacturerDate').val($('#hdnManufacturerDate_' + vehicleID).val() == '01-01-0001' ? '' : $('#hdnManufacturerDate_' + vehicleID).val());
    $('#txtKMLimit').val($('#hdnKMLimit_' + vehicleID).val());
    $('#txtSubVendor').val($('#hdnSubVendor_' + vehicleID).val());
    $('#cbStatus').val($('#hdnValidTo_' + vehicleID).val());
    makeFieldDisableEnable(true);
    $('#cbIsGPSIntalled').val($('#hdnIsGPSInstalled_' + vehicleID).val());
    $('#ddlGPSInstalledBy').val($('#hdnGPSInstalledBy_' + vehicleID).val());
    $('#txtGPSInstalledDate').val($('#hdnGPSInstallDate_' + vehicleID).val() == '01-01-0001' ? '' : $('#hdnGPSInstallDate_' + vehicleID).val());
    $('#txtFitnessCertExpiryDate').val($('#hdnFit_Crt_Expiry_Date_' + vehicleID).val() == '01-01-0001' ? '' : $('#hdnFit_Crt_Expiry_Date_' + vehicleID).val());
    $('#txtRCYear').val($('#hdnRC_Year_' + vehicleID).val());
    $('#txtPermitExpiryDate').val($('#hdnPermit_Expiry_Date_' + vehicleID).val() == '01-01-0001' ? '' : $('#hdnPermit_Expiry_Date_' + vehicleID).val());
    $('#txtPUCExpiryDate').val($('#hdnPUC_Expiry_Date_' + vehicleID).val() == '01-01-0001' ? '' : $('#hdnPUC_Expiry_Date_' + vehicleID).val());
    $('#txtCurrentOdometer').val($('#hdnODO_Reading_' + vehicleID).val());
    $('#cbOwened').val($('#hdnOwn_Attached_' + vehicleID).val());
    $('#txtRemarks').val($('#hdnRemarks_' + vehicleID).val());

}
var makeFieldDisableEnable = function (status) {
    $('#ddlLocationForVehicle').attr('disabled', status);
    $('#ddlVehicleType').attr('disabled', status);
    $('#ddlVendor').attr('disabled', status);
    $('#txtVehicleNo_V').attr('disabled', status);
    $('#txtRegistrationNo_V').attr('disabled', status);
    $('#cbActive').attr('disabled', status);
}
var updateVehicleDetails = function () {

    if (validateVehicle()) {
        var req = {
            'VehicleID': $('#hdnVehicleID').val(),
            'LocCode': $('#ddlLocationForVehicle').val(),
            'VehicleTypeID': $('#ddlVehicleType').val(),
            'VendorID': $('#ddlVendor').val(),
            'VehicleNo': $('#txtVehicleNo_V').val(),
            'RegistrationNo': $('#txtRegistrationNo_V').val(),
            'BillingPlanId': $('#ddlBillPlan').val(),
            'EffectiveDate': $('#txtEffectiveDate').val(),
            'INS_Expiry_Date': $('#txtInsuranceDate').val(),
            'ManufacturerDate': $('#txtManufacturerDate').val(),
            'KMLimit': $('#txtKMLimit').val(),
            'SubVendor': $('#txtSubVendor').val(),
            'Active': $('#cbStatus').is(":checked") == true ? "Y" : "N",
            'IsGPSInstalled': $('#cbIsGPSIntalled').is(":checked") == true ? "Y" : "N",
            'GPSInstalledBy': $('#ddlGPSInstalledBy').val(),
            'GPSInstallDate': $('#txtGPSInstalledDate').val(),
            'Fit_Crt_Expiry_Date': $('#txtFitnessCertExpiryDate').val(),
            'RC_Year': $('#txtRCYear').val(),
            'Permit_Expiry_Date': $('#txtPermitExpiryDate').val(),
            'PUC_Expiry_Date': $('#txtPUCExpiryDate').val(),
            'ODO_Reading': $('#txtCurrentOdometer').val(),
            'Own_Attached': $('#cbOwened').is(":checked") == true ? "O" : "A"
        };

        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref +  '/MasterItems/UpdateVehicle',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('vehicle updated successfully!');
                    $('#hdnVehicleID').val('0');
                    SearchVendors();
                }
                else {
                    alert(data.Result);
                }
            },
            error: function (xhr) {
                alert('Error: unable to update vehicle details. Please try after some time!');
            }
        });
    }
}
var validateVehicle = function () {
    if ($('#ddlLocationForVehicle').val() == "" || $('#ddlLocationForVehicle').val() == "0") {
        alert('Please select location of vehicle!');
        $('#ddlLocationForVehicle').focus();
        return false;
    }
    else if ($('#ddlVehicleType').val() == '' || $('#ddlVehicleType').val() == '0') {
        alert('Please select vehicle type!');
        $('#ddlVehicleType').focus();
        return false;
    }

    else if ($('#ddlVendor').val() == '' || $('#ddlVendor').val() == '0') {
        alert('Please select vendor/transporter!');
        $('#ddlVendor').focus();
        return false;
    }
    else if ($('#txtVehicleNo_V').val().trim().length < 4) {
        alert('Please enter valid vehicle no.');
        $('#txtVehicleNo_V').focus();
        return false;
    }
    else if ($('#txtRegistrationNo_V').val().trim().length < 8) {
        alert('Please enter valid registration no.');
        $('#txtRegistrationNo_V').focus();
        return false;
    }
    else if ($('#txtEffectiveDate').val() == '') {
        alert('Please enter effective date!');
        $('#txtEffectiveDate').focus();
        return false;
    }
    else if ($('#txtManufacturerDate').val() == '') {
        alert('Please enter manufacturer date!');
        $('#txtManufacturerDate').focus();
        return false;
    }
    else if ($('#txtKMLimit').val() == '') {
        alert('Please enter KM limit!');
        $('#txtKMLimit').focus();
        return false;
    }
    else if ($('#txtSubVendor').val() == '') {
        alert('Please enter Sub- Vendor!');
        $('#txtSubVendor').focus();
        return false;
    }
    else if ($('#ddlGPSInstalledBy').val() == '' || $('#ddlGPSInstalledBy').val() == '0') {
        alert('Please select GPS installed by!');
        $('#ddlGPSInstalledBy').focus();
        return false;
    }
    else if ($('#txtRCYear').val() == '' || $('#txtRCYear').val() == '0' || $('#txtRCYear').val().trim().length != 4) {
        alert('Please enter valid RC Year!');
        $('#txtRCYear').focus();
        return false;
    }
    else if ($('#txtPermitExpiryDate').val() == '') {
        alert('Please enter All india permit expiry date!');
        $('#txtPermitExpiryDate').focus();
        return false;
    }
    else if ($('#txtPUCExpiryDate').val() == '') {
        alert('Please enter PUC expiry date!');
        $('#txtPUCExpiryDate').focus();
        return false;
    }
    else if ($('#txtCurrentOdometer').val() == '') {
        alert('Please enter current odo-meter!');
        $('#txtCurrentOdometer').focus();
        return false;
    }
    else if ($('#txtCurrentOdometer').val() == '') {
        alert('Please enter current odo-meter!');
        $('#txtCurrentOdometer').focus();
        return false;
    }

    return true;
}
var resetFields = function () {
    $('#ddlLocationForVehicle').val('0');
    $('#ddlVehicleType').val('0');
    $('#ddlVendor').val('0');
    $('#txtVehicleNo_V').val('');
    $('#txtRegistrationNo_V').val('');
    $('#ddlBillPlan').val('0');
    $('#txtEffectiveDate').val('');
    $('#txtInsuranceDate').val('');
    $('#txtManufacturerDate').val('');
    $('#txtKMLimit').val('');
    $('#txtSubVendor').val('');
    $('#cbStatus').attr("checked", true);
    $('#cbIsGPSIntalled').attr("checked", true);
    $('#ddlGPSInstalledBy').val('');
    $('#txtGPSInstalledDate').val('');
    $('#txtFitnessCertExpiryDate').val('');
    $('#txtRCYear').val('');
    $('#txtPermitExpiryDate').val('');
    $('#txtPUCExpiryDate').val('');
    $('#txtCurrentOdometer').val('');
    $('#cbOwened').attr("checked", true);
}
var openDocumentUploadPopup=function(vehicleID)
{
    $('#hdnVehicleIdForUploadDocs').val(vehicleID);
    $('#divUploadDocument').modal('toggle');
    var VehicleID = $('#hdnVehicleIdForUploadDocs').val();
    getUploadedDocs(VehicleID);
    // rest actions are in uploadDocuments.js 
}

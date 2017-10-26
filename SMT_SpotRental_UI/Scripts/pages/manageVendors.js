/*--------------------------------------------------------------------------------------  
 * -------------------------SPOT RENTAL # manageVendor.js----------------------------------------
 *  
 *manageVendor.js is jquery file to handle all functionalies related to vendors like add,
 * edit, view and validate 
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/
var urlPref ;
$(document).ready(function () {
    bindBaseOffice('#ddlLocation');
    $('#btnSearch').on('click', function () { SearchVendors(); });
    $('#btnAdd').on('click', function () { $('#hdnVendorIDToUpdate').val('0'); $('#btnAddVendor').text('Add Vendor'); $('#addVendor').text('Add/Register Vendor'); openModelToAddVendor(); });
    $('#btnAddVendor').on('click', function () { if ($('#hdnVendorIDToUpdate').val() == '0') { addVendorDetails(); } else { updateVendorDetails() } });
    $(document).on('keydown', '#txtMobileNo', function (e) {
        if (e.keyCode == 32) return false;
    });
    $(document).on('keydown', '#txtFirstName', function (e) {
        if (e.keyCode == 32) return false;
    });
    $(document).on('keydown', '#txtLastName', function (e) {
        if (e.keyCode == 32) return false;
    });


});
var SearchVendors = function () {
    $.ajax({
        type: "POST",
        data: { 'LocCode': $('#ddlLocation').val(), 'MobileNo': $('#txtMobileNo').val(), 'FName': $('#txtFirstName').val().trim(), 'LName': $('#txtLastName').val().trim() },
        url: urlPref +  '/MasterItems/SearchVendors',
        success: function (data) {
            $('#divInfo').css('display', 'none');
            $('#divVendorList').html('');
            $('#divVendorList').html(data);
        },
        error: function (xhr) {
            alert('Error: No data found!');

        }
    });
}
var changeVendorStatus = function (vendID) {
    $.ajax({
        type: "POST",
        data: { 'VendorID': vendID },
        url: urlPref +  '/MasterItems/ChangeVendorStatus',
        success: function (data) {
            if (data && data.Result == "TRUE") {
                alert('Vendor updated successfully!');
                SearchVendors();
            }
            else {
                alert(data.Result);
            }
        },
        error: function (xhr) {
            alert('Error: No data found!');
        }
    });
}
var openModelToAddVendor = function () {
    resetFields();
    bindBaseOffice('#ddlLocationForVendor');
    $('#divAddUpdateVendor').modal('show');
    $(function () {
        $('.datepicker').datepicker({
            format: 'mm-dd-yyyy',
            startDate: '-0d',
            autoclose: true
        });
    });
    $(document).on('keydown', '#txtFName', function (e) {
        if (e.keyCode == 32) return false;
    });
    $(document).on('keydown', '#txtMName', function (e) {
        if (e.keyCode == 32) return false;
    });
    $(document).on('keydown', '#txtLName', function (e) {
        if (e.keyCode == 32) return false;
    });
    checkNumeric('#txtMobileNo_V');
    checkNumeric('#txtPhone');
    checkNumeric('#txtZIPCode');
}
var validateVendorDetails = function () {

    if ($('#ddlLocationForVendor').val() == "" || $('#ddlLocationForVendor').val() == "0") {
        alert('Please select location of vendor!');
        $('#ddlLocationForVendor').focus();
        return false;
    }
    else if ($('#txtShortName').val().trim() == '') {
        alert('Please enter short name!');
        $('#txtShortName').focus();
        return false;
    }
    else if ($('#txtCompanyName').val().trim() == '') {
        alert('Please enter company name!');
        $('#txtCompanyName').focus();
        return false;
    }
    else if ($('#txtPrefix').val().trim() == '') {
        alert('Please enter prefix for vendor!');
        $('#txtPrefix').focus();
        return false;
    }
    else if ($('#txtFName').val().trim() == '') {
        alert('Please enter first name!');
        $('#txtFName').focus();
        return false;
    }
    else if ($('#txtLName').val().trim() == '') {
        alert('Please enter last name!');
        $('#txtLName').focus();
        return false;
    }
    else if ($('#txtAddress1').val().trim() == '') {
        alert('Please enter address line 1!');
        $('#txtAddress1').focus();
        return false;
    }
    else if ($('#txtCity').val().trim().length < 2) {
        alert('Please enter valid city name!');
        $('#txtCity').focus();
        return false;
    }

    else if ($('#txtZIPCode').val().length != 6) {
        alert('Please enter valid ZIP code!');
        $('#txtZIPCode').focus();
        return false;
    }
    else if ($('#txtMobileNo_V').val().length != 10) {
        alert('Please enter valid mobile number!');
        $('#txtMobileNo_V').focus();
        return false;
    }
    else if ($('#txtPhone').val().trim() != '' && $('#txtPhone').val().length < 8) {
        alert('Please enter valid phone number!');
        $('#txtPhone').focus();
        return false;
    }
    else if (!validateEmail('#txtEmailID') && $('#txtEmailID').val().length < 8) {
        alert('Please enter valid email address!');
        $('#txtEmailID').focus();
        return false;
    }
    else if ($('#txtContractFrom').val().length < 10) {
        alert('Please enter contract from date!');
        $('#txtContractFrom').focus();
        return false;
    }
    else if ($('#txtContractTo').val().length < 10) {
        alert('Please enter contract to date!');
        $('#txtContractTo').focus();
        return false;
    }
    return true;
}
var addVendorDetails = function () {
    $('#hdnVendorIDToUpdate').val('0');
    if (validateVendorDetails()) {
        var req = {
            'LocationCode': $('#ddlLocationForVendor').val(),
            'ShortName': $('#txtShortName').val(),
            'PrefixTag': $('#txtPrefix').val(),
            'IsActive': $("#cbActive").is(":checked") == true ? "Y" : "N",
            'CompanyName': $('#txtCompanyName').val(),
            'EffectiveDate': $('#txtEffectiveDate').val(),
            'FName': $('#txtFName').val(),
            'MName': $('#txtMName').val(),
            'LName': $('#txtLName').val(),
            'Address1': $('#txtAddress1').val(),
            'Address2': $('#txtAddress2').val(),
            'City': $('#txtCity').val(),
            'ZIPCode': $('#txtZIPCode').val(),
            'Phone1': $('#txtMobileNo_V').val(),
            'Phone2': $('#txtPhone').val(),
            'EmailID': $('#txtEmailID').val(),
            'ValidFrom': $('#txtContractFrom').val(),
            'ValidTo': $('#txtContractTo').val()
        };

        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref +  '/MasterItems/AddVendor',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Vendor added successfully!');
                    SearchVendors();
                }
                else {
                    alert(data.Result);
                }
            },
            error: function (xhr) {
                alert('Error: unable to add vendor. Please try after some time!');
            }
        });
    }
    else {
        alert('Some error in registring vendors.');
    }
}
var makeFieldDisableEnable=function(status)
{
    $('#ddlLocationForVendor').attr('disabled', status);
    $('#txtShortName').attr('disabled', status);
    $('#txtCompanyName').attr('disabled', status);
    $('#txtPrefix').attr('disabled', status);
    $('#txtEffectiveDate').attr('disabled', status);
    $('#cbActive').attr('disabled', status);
}
var openPoptotoUpdateVendorDetails = function (vendorId) {
   // alert($('#hdnAddress1_' + vendorId).val());
    $('#addVendor').text('Update Vendor');
    $('#btnAddVendor').text('Update Vendor');
    openModelToAddVendor();
    $('#ddlLocationForVendor').val($('#hdnLocationCode_' + vendorId).val());
    $('#txtShortName').val($('#hdnShortName_' + vendorId).val());
    $('#txtPrefix').val($('#hdnPrefixTag_' + vendorId).val());
    $('#txtCompanyName').val($('#hdnCompanyName_' + vendorId).val());
    $('#txtEffectiveDate').val($('#hdnEffectiveDate_' + vendorId).val());
    makeFieldDisableEnable(true);
    $('#txtFName').val($('#hdnFName_' + vendorId).val());
    $('#txtMName').val($('#hdnFName_' + vendorId).val());
    $('#txtLName').val($('#hdnFName_' + vendorId).val());
    $('#txtAddress1').val($('#hdnAddress1_' + vendorId).val());
    $('#txtAddress2').val($('#hdnAddress2_' + vendorId).val());
    $('#txtCity').val($('#hdnCity_' + vendorId).val());
    $('#txtZIPCode').val($('#hdnZIPCode_' + vendorId).val());
    $('#txtMobileNo_V').val($('#hdnPhone1_' + vendorId).val());
    $('#txtPhone').val($('#hdnPhone2_' + vendorId).val());
    $('#txtEmailID').val($('#hdnEmailID_' + vendorId).val());
    $('#txtContractFrom').val($('#hdnValidFrom_' + vendorId).val());
    $('#txtContractTo').val($('#hdnValidTo_' + vendorId).val());
    $('#hdnVendorIDToUpdate').val(vendorId)
}
var updateVendorDetails=function()
{
    if (validateVendorDetails()) {
        var req = {
            'VendorId': $('#hdnVendorIDToUpdate').val(),
            'CompanyName': $('#txtCompanyName').val(),
            'ShortName': $('#txtShortName').val(),
            'FName': $('#txtFName').val(),
            'MName': $('#txtMName').val(),
            'LName': $('#txtLName').val(),
            'Address1': $('#txtAddress1').val(),
            'Address2': $('#txtAddress2').val(),
            'City': $('#txtCity').val(),
            'ZIPCode': $('#txtZIPCode').val(),
            'Phone1': $('#txtMobileNo_V').val(),
            'Phone2': $('#txtPhone').val(),
            'EmailID': $('#txtEmailID').val(),
            'ValidFrom': $('#txtContractFrom').val(),
            'ValidTo': $('#txtContractTo').val()

        };

        $.ajax({
            type: "POST",
            data: { 'request': req },
            url: urlPref +  '/MasterItems/UpdateVendor',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Vendor updated successfully!');
                    SearchVendors();
                    $('#hdnVendorIDToUpdate').val('0');
                }
                else {
                    alert(data.Result);
                }
            },
            error: function (xhr) {
                alert('Error: unable to update vendor. Please try after some time!');
            }
        });

    }
}
var viewVendorDetails = function (vendorId) {

        $('#divViewVendorDetails').modal('toggle');
        
        $("#lblVendorFirstName").text($('#hdnFName_' + vendorId).val());
        $("#lblVendorMiddleName").text($('#hdnMName_' + vendorId).val());

        $("#lblVendorLastName").text($('#hdnLName_' + vendorId).val());
        $("#lblVendorLocationCode").text($('#hdnLocationCode_' + vendorId).val());

        $("#lblStatus").text($('#hdnStatus_' + vendorId).val()=='Y'?'Active':'De-Active');
        $("#lblVendorShortName").text($('#hdnShortName_' + vendorId).val());

        $("#lblVendorCompanyName").text($('#hdnCompanyName_' + vendorId).val());
        $("#lblVendorPrefixTag").text($('#hdnPrefixTag_' + vendorId).val());

        $("#lblVendorAddress1").text($('#hdnAddress1_' + vendorId).val());
        $("#lblVendorAddress2").text($('#hdnAddress2_' + vendorId).val());

        $("#lblVendorCity").text($('#hdnCity_' + vendorId).val());
        $("#lblVendorPhone1").text($('#hdnPhone1_' + vendorId).val());

        $("#lblVendorPhone2").text($('#hdnPhone2_' + vendorId).val());
        $("#lblVendorZIPCode").text($('#hdnZIPCode_' + vendorId).val());

        $("#lblVendorEffectiveDate").text($('#hdnEffectiveDate_' + vendorId).val());
        $("#lblVendorEmailId").text($('#hdnEmailID_' + vendorId).val());

        $("#lblVendorValidFrom").text($('#hdnValidFrom_' + vendorId).val());
        $("#lblVendorValidTill").text($('#hdnValidTo_' + vendorId).val());
}
var resetFields=function()
{
    makeFieldDisableEnable(false);
    $('#ddlLocationForVendor').val('0');
    $('#txtShortName').val('');
    $('#txtPrefix').val('');
    $('#txtCompanyName').val('');
    $('#txtEffectiveDate').val('');   
    $('#txtFName').val('');
    $('#txtMName').val('');
    $('#txtLName').val('');
    $('#txtAddress1').val('');
    $('#txtAddress2').val('');
    $('#txtCity').val('');
    $('#txtZIPCode').val('');
    $('#txtMobileNo_V').val('');
    $('#txtPhone').val('');
    $('#txtEmailID').val('');
    $('#txtContractFrom').val('');
    $('#txtContractTo').val('');
    $('#hdnVendorIDToUpdate').val('0')
}

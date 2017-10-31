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
var DriverGuard;
$(document).ready(function () {  
    bindAllMenuItems();
    bindDataTablesProp();
    $('#btnAddMenu').on('click', function () { $('#btnAddMenu').text().trim() == 'Save Menu' ? addMenuDetails() : updateMenuDetails(); });
    $(document).on('keydown', '#txtActionName,#txtControllerName', function (e) {
        
        if ((e.keyCode >= 65 && e.keyCode <= 90) || (e.keyCode == 20) || (e.keyCode == 16) || (e.keyCode == 8) || (e.keyCode == 46))
            return true
        else
            return false;
    });
    checkNumeric('#txtMenuOrder');
});
var bindParentMenu = function (cntrlID) {
    
    $.ajax({
        type: "GET",
        url: urlPref + '/Access/GetParentMenu',
        dataType: "json",
        async: false,
        success: function (data) {
            
            listitems = '<option value=0>--SELECT--</option>';
            if (data.Result == true) {
                
                $.each(data.List, function (key, value) {
                    listitems += '<option value=' + "'" + value.ActionID + "'" + '>' + value.ActionText + '</option>';
                });
                $(cntrlID).empty();
                $(cntrlID).append(listitems);
            }
            else {
                $(cntrlID).empty();
                $(cntrlID).append(listitems);
            }

        },
        error: function (xhr) {
            
            alert('Err '+ xhr);
        }
    });
}
var bindDataTablesProp = function () {
    $(function () {
        $('#tblMenus').dataTable({
            stateSave: true,
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': 'no-sort'
            }]
        });
    });
}
var bindAllMenuItems = function () {
    $('.enableLoader').show();
    $.ajax({
        type: "POST",
        url: urlPref + '/Access/BindMenus',
        success: function (data) {
            $('#divMenuList').html('');
            $('#divMenuList').html(data);
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
var showPopupForEditMenu = function (actionID, actionName, actionText, controllerName, menuActive, isMenuItems, menuOrder, rootID) {
    
    $('#divAddEditMenu').modal('toggle');
    $('#heading').text('Update Menu')
    $('#hdnActionIDForUpdate').val(actionID);
    bindParentMenu('#ddlParentMenu');
    $('#txtActionName').val(actionName);
    $('#txtActionText').val(actionText);
    $('#txtControllerName').val(controllerName);
    $('#txtMenuOrder').val(menuOrder);
    $('#ddlParentMenu').val(rootID);
    isMenuItems == 1 ? $('#cbMenuItem').bootstrapToggle('on') : $('#cbMenuItem').bootstrapToggle('off');
    menuActive == 1 ? $('#cbStatus').bootstrapToggle('on') : $('#cbStatus').bootstrapToggle('off');
    $('#btnAddMenu').text('Update Menu');
}
var showPopupForAddMenu = function () {
    $('#divAddEditMenu').modal('toggle');
    $('#heading').text('Add Menu')
    $('#btnAddMenu').text('Save Menu');
    
    bindParentMenu('#ddlParentMenu');
    resetFields();
}
var addMenuDetails = function () {
    

    if (validateMenuDetails())
    {
        var ActionName=$('#txtActionName').val();
        var ActionText = $('#txtActionText').val();
        var controllerName = $("#cbMenuItem").is(":checked") == true ? $('#txtControllerName').val() : '#';        
        var rootId = $("#cbMenuItem").is(":checked") == true ? $('#ddlParentMenu').val() : 0;
        var RootID= rootId;
        var status= $("#cbStatus").is(":checked") == true ? true : false;
        var IsMenuItems = $("#cbMenuItem").is(":checked") == true ? true : false;
        var MenuOrder = $('#txtMenuOrder').val();
        

        $.ajax({
            type: "POST",
            data: { 'ActionName': ActionName, 'ActionText': ActionText, 'RootID': RootID, 'status': status, 'ControllerName': controllerName, 'MenuOrder': MenuOrder, 'IsMenuItems': IsMenuItems },
            url: urlPref + '/Access/AddNewMenu',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Menu added successfully!');
                    $('#divAddEditMenu').modal('hide');
                    $('#hdnActionIDForUpdate').val('0');
                    bindAllMenuItems();
                }
                else {
                    alert('Error: ' + data.Result);
                }
            },
            error: function (xhr) {
                
                
                alert('Error: No data found!');
            }
        });


    }
}
var updateMenuDetails = function () {
    if (validateMenuDetails())
    {
        var ActionId = $('#hdnActionIDForUpdate').val();
        var ActionName = $('#txtActionName').val();
        var ActionText = $('#txtActionText').val();
        var controllerName = $("#cbMenuItem").is(":checked") == true ? $('#txtControllerName').val() : '#';
        var rootId = $("#cbMenuItem").is(":checked") == true ? $('#ddlParentMenu').val() : 0;
        var RootID = rootId;
        var status = $("#cbStatus").is(":checked") == true ? true : false;
        var IsMenuItems = $("#cbMenuItem").is(":checked") == true ? true : false;
        var MenuOrder = $('#txtMenuOrder').val();
        

        $.ajax({
            type: "POST",
            data: {'ActionId':ActionId, 'ActionName': ActionName, 'ActionText': ActionText, 'RootID': RootID, 'status': status, 'ControllerName': controllerName, 'MenuOrder': MenuOrder, 'IsMenuItems': IsMenuItems },
            url: urlPref + '/Access/UpdateNewMenu',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Menu updated successfully!');
                    $('#divAddEditMenu').modal('hide');
                    $('#hdnActionIDForUpdate').val('0');
                    bindAllMenuItems();
                }
                else {
                    alert('Error: ' + data.Result);
                }
            },
            error: function (xhr) {
                
                //console.log('xhr ' + xhr);
                alert('Error: No data found!');
            }
        });
    }
}
var validateMenuDetails = function () {
    if ($('#txtActionName').val().trim() == '') {
        alert('Please enter action name!');
        $('#txtActionName').focus();
        return false;
    }
    else if ($('#txtActionText').val().trim() == '') {
        alert('Please enter action text!');
        $('#txtActionText').focus();
        return false;
    }
    else if (($('#txtControllerName').val().trim() == '') || ( ($('#ddlParentMenu').val() != '0') && $('#txtControllerName').val().trim() == '#')) {
        alert('Please enter controller name!');
        $('#txtControllerName').focus();
        return false;
    }    
    else if ($("#cbMenuItem").is(":checked") == true)
    {
        if ($('#ddlParentMenu').val() == '0')
        {
            alert('Please select parent menu');
            $('#ddlParentMenu').focus();
            return false;
        }
    }
    return true;
}
var SetParnetMenu = function () {
    
    if ($("#cbMenuItem").is(":checked") == false) {
        
        $('#ddlParentMenu').val('0');
        $('#ddlParentMenu').prop('disabled', 'disabled');
    }
    else {
        $('#ddlParentMenu').removeAttr('disabled');
    }
}
var resetFields = function () {
    $('#txtActionName').val('');
    $('#txtActionText').val('');
    $('#txtControllerName').val('');
    $('#ddlParentMenu').val('0');
    $('#txtMenuOrder').val('');
    $('#cbMenuItem').bootstrapToggle('on')
    $('#cbStatus').bootstrapToggle('on')    
}

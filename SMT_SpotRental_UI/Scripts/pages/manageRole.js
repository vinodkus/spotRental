/*--------------------------------------------------------------------------------------  
 *------------------------SPOT RENTAL manageRole.js-----------------------------------------
 *  
 * manageRole.js is jquery file manage roles functionalitis.
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @Copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/

var urlPref;
$(document).ready(function () {
    bindDataTablesProp();
    $('#btnUpdate').on('click', function () { if ($('#hdnRoleIDForUpdate').val() == "A") { addNewRole() } else { updateRole() } });
});
var bindDataTablesProp = function () {
    $(function () {
        $('#tblRoles').dataTable({
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': 'no-sort'
            }]
        });
    });
}
var changeStatus = function (roleID) {
    $.ajax({
        type: "POST",
        data: { 'RoleID': roleID },
        url: urlPref + '/Access/ChangeRoleStatus',
        success: function (data) {
            if (data && data.Result == "TRUE") {
                alert('Roles status updated successfully!');
                getAllRoles();
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
var getAllRoles = function () {
    $.ajax({
        type: "POST",
        url: urlPref + '/Access/BindAllRoles',
        success: function (data) {
            $('#divRoleList').html('');
            $('#divRoleList').html(data);
            bindDataTablesProp();
        },
        error: function (xhr) {
            alert('Error: No data found!');
        }
    });
}
var showPopupForUpdate = function (roleID) {
    $('#divAddEditRoles').modal('toggle');
    $('#roleName').val($('#tdRoleName_' + roleID).text());
    $('#cbStatus').bootstrapToggle($('#hdnStatus_' + roleID).val().toUpperCase() == "TRUE" ? 'on' : 'off');
    $('#hdnRoleIDForUpdate').val(roleID);
}
var updateRole = function () {
    var roleID = $('#hdnRoleIDForUpdate').val();
    var status = $('#cbStatus').is(":checked") == true ? "Y" : "N";
    if (validateRole()) {
        $.ajax({
            type: "POST",
            data: { 'RoleID': roleID, 'RoleName': $('#roleName').val(), 'Status': status },
            url: urlPref + '/Access/UpdateRole',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Role updated successfully!');
                    $('#divAddEditRoles').modal('hide');
                    $('#hdnRoleIDForUpdate').val('0');
                    getAllRoles();
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
}
var showPopupForAddRole = function () {
    $('#divAddEditRoles').modal('toggle');
    $('#roleName').val('');
    $('#cbStatus').bootstrapToggle('on');
    $('#hdnRoleIDForUpdate').val("A");
}
var addNewRole = function () {
    var status = $('#cbStatus').is(":checked") == true ? "Y" : "N";
    if (validateRole()) {
        $.ajax({
            type: "POST",
            data: { 'RoleName': $('#roleName').val(), 'Status': status },
            url: urlPref + '/Access/AddNewRole',
            success: function (data) {
                if (data && data.Result == "TRUE") {
                    alert('Role added successfully!');
                    $('#divAddEditRoles').modal('hide');
                    $('#hdnRoleIDForUpdate').val('0');
                    getAllRoles();
                }
                else {
                    alert('Error: '+ data.Result);
                }
            },
            error: function (xhr) {
                alert('Error: No data found!');
            }
        });
    }
}
var validateRole = function () {

    if ($('#roleName').val().trim() == '') {
        alert('Please enter role name.');
        $('#roleName').focus();
        return false;
    }
    else if ($('#roleName').val().trim().length < 4) {
        alert('Role name must have atleast four characters.');
        $('#roleName').focus();
        return false;
    }
    return true;
}
/*--------------------------------------------------------------------------------------  
 *------------------------SPOT RENTAL roleActionMap.js-----------------------------------------
 *  
 * roleActionMap.js is jquery file map roles with multiple actions/menu
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @Copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/
var urlPref;
$(document).ready(function () {
    bindAllRoles('#ddlRoles');
    $('#btnSave').on('click', function () { updateActionRole() });
    
});
var getAllMenuItems = function () {
    if ($('#ddlRoles').val().trim() != "0") {
        $('#divActionList').css('display', 'block');
        $('.enableLoader').show();
        $.ajax({
            url: urlPref + '/Access/GetAllMenu',
            data: { 'RoleID': $('#ddlRoles').val() },
            success: function (data) {
                $('#divActionList').html('');
                $('#divActionList').html(data);
                $('.enableLoader').hide();
                //Make parent child checked/unchecked.............
                $('li :checkbox').on('click', function () {
                    var $chk = $(this),
                        $li = $chk.closest('li'),
                        $ul, $parent;
                    if ($li.has('ul')) {
                        $li.find(':checkbox').not(this).prop('checked', this.checked)
                    }
                    do {
                        $ul = $li.parent();
                        $parent = $ul.siblings(':checkbox');
                        if ($chk.is(':checked')) {
                            $parent.prop('checked', $ul.has(':checkbox:not(:checked)').length == 0)
                        } else {
                            $parent.prop('checked', false)
                        }
                        $chk = $parent;
                        $li = $chk.closest('li');
                    } while ($ul.is(':not(.someclass)'));

                    $('.enableLoader').hide();
                });
                //---------------------------------------------------------------------------
            },
            error: function (xhr) {
                $('.enableLoader').hide();
                // alert('Error: No data found!');
            }
        });
    }
    else {
        $('#divActionList').css('display', 'none');
    }
}

var updateActionRole=function()
{
    var actIds = getSelectedActionIds();
    if ($('#ddlRoles').val().trim() == "0")
    {
        alert('Please select role.');
        $('#ddlRoles').focus();
        return false;
    }
    else if (actIds==null || actIds==undefined || actIds.length < 2) {
        alert('Please select atleast one action to map.');
        return false;
    }
    else {
        $('.enableLoader').show();
        $.ajax({
            url: urlPref + '/Access/UpdateActionRole',
            data: { 'RoleID': $('#ddlRoles').val(), 'ActionIds': actIds },
            success: function (data) {
                if (data.Result == true && data.Message == "TRUE") {
                    getAllMenuItems();
                    alert('Actions successfully maped with selected role.');
                    location.reload();// to update menu items as per new mapping...
                }
                else {
                    $('.enableLoader').hide();
                    alert(data.Message);
                }

            },
            error: function (xhr) {
                $('.enableLoader').hide();
            }
        });
    }
}
var getSelectedActionIds = function () {
    var actIds = ""; var cbid = '';
    $('input[type=checkbox]').each(function () {
        if (this.checked)
        {
            cbid = this.id;
            
            if((cbid.match(/_/g) || []).length==1)
            {
                // for parent checkboxes
                actIds += cbid.split('_')[1] + ',';

            }
            else  if((cbid.match(/_/g) || []).length==2)
            {
                // for child checkboxes
                actIds += cbid.split('_')[2] + ',';
            }

        }
        
    });

    return actIds;
}
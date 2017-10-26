/*--------------------------------------------------------------------------------------  
 * -------------------------SPOT RENTAL profile.js----------------------------------------
 *  
 *profile.js is jquery file to use to manage profile of users
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/

var urlPref;
var BaseAddress;
$(document).ready(function () {
    bindBaseOffice('#ddlBaseOffice');
    // alert(BaseAddress);
    //$('#ddlBaseOffice option[value=' + BaseAddress + ']').attr('selected', true);
    $('#ddlBaseOffice').val(BaseAddress);

    $('#btnUpdateProfile').on('click', function () {
        updateProfile();
    });

});

var updateProfile = function () {

    var mobileNo = $('#userDetails_MobileNo').val();
    var gender = $('#ddlGender').val();
    var baseAddress = $('#ddlBaseOffice').val();
    var homeAddress = $('#userDetails_HomeAddress').val();
    var emailID = $('#userDetails_EmailID').val();


    if (mobileNo == "" || mobileNo.length != 10 || !validateMobile(mobileNo)) {
        alert('Please enter valid mobile number.');
        return false;
    }
    else if (gender == "" || gender == "0") {
        alert('Please select gender');
        return false;
    }
    else if (baseAddress == "" || baseAddress == "0") {
        alert('Please select base address');
        return false;
    }
    else if (homeAddress == "" || homeAddress.length < 10) {
        alert('Please enter valid home address');
        return false;
    }
    else {
        var empData = {
            'MobileNo': mobileNo,
            'Gender': gender,
            'OfficeLocation': baseAddress,
            'HomeAddress': homeAddress,
            'EmailID': emailID
        };

        $.ajax({
            cache: false,
            type: "POST",
            url: urlPref +'/UserBucket/UpdateUserProfile',
            data: empData,
            dataType: "json",
            success: function (data) {
                if (data.Result == true) {
                    alert('Your profile updated successfully!');
                }
                else {
                    alert(data.Msg);
                }

            },
            error: function (xhr) {
                alert("Critical Error!. Failed to call the server.");
            }
        });


    }

}
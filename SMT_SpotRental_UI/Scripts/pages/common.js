/*--------------------------------------------------------------------------------------  
 *------------------------SPOT RENTAL common.js-----------------------------------------
 *  
 * common.js is jquery file to use for common purpose/functionality of application.
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @Copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/

var urlPref;
var bindDesignation = function (cntrlID) {
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetDesignation',
        dataType: "json",
        success: function (data) {
            listitems = '<option value=0>--SELECT--</option>';
            if (data.Result == true) {
                $.each(data.List.listDesignation, function (key, value) {
                    listitems += '<option value=' + "'" + value.designation_id + "'" + '>' + value.designation_name + '</option>';
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
            alert("Critical Error!. Failed to call the server.");
        }
    });
}
var bindBaseOffice = function (cntrlID) {
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetBaseLocation',
        dataType: "json",
        async: false,
        success: function (data) {
            listitems = '<option value=0>--SELECT--</option>';
            if (data.Result == true) {
                $.each(data.List.listBaseLocation, function (key, value) {
                    listitems += '<option value=' + "'" + value.LocCode + "'" + '>' + value.LocName + '</option>';
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
        }
    });
}
var bindRentalCity = function (cntrlID) {
    $.ajax({
        cache: true,
        type: "GET",
        url: urlPref + '/Common/GetBaseLocation',
        dataType: "json",
        success: function (data) {
            var listitems = '<option value=0>--Select--</option>', cityList = [];

            if (data.Result == true) {

                $.each(data.List.listBaseLocation, function (key, value) {
                    if ($.grep(cityList, function (e) { return e.City == value.City; }).length === 0) {
                        cityList.push(value);
                    }
                });
                $.each(cityList, function (key, value) {
                    listitems += '<option value=' + "'" + value.City + "'" + '>' + value.City + '</option>';
                });

                $(cntrlID).empty();
                $(cntrlID).append(listitems);
                $(cntrlID).append('<option value=Other>Other</option>');
            }
            else {
                $(cntrlID).empty();
                $(cntrlID).append(listitems);
            }

        },
        error: function (xhr) {
        }
    });
}
var bindVehicleTypes = function (cntrlID) {
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetAllVehicle',
        dataType: "json",
        async: false,
        success: function (data) {
            listitems = '<option value=0>SELECT VEHICLE TYPE</option>';
            if (data.Result == true) {
                $.each(data.List.listVehicleType, function (key, value) {
                    listitems += '<option value=' + "'" + value.VehicleTypeID + "'" + '>' + value.VehicleType + '</option>';
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
            alert("Critical Error!. Failed to call the server.");
        }
    });
}
var bindAllVendors = function (cntrlID) {
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetAllVendors',
        dataType: "json",
        async: false,
        success: function (data) {
            listitems = '<option value=0>SELECT VENDOR</option>';
            if (data.Result == true) {
                $.each(data.List.listVendor, function (key, value) {
                    listitems += '<option value=' + "'" + value.VendorId + "'" + '>' + value.CompanyName + '</option>';
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
            alert("Critical Error!. Failed to call the server.");
        }
    });
}
var bindDocumentList = function (cntrlID, docsFor) {
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetDocumentList',
        data: { 'DocType': docsFor },
        dataType: "json",
        async: false,
        success: function (data) {
            listitems = '<option value=0>SELECT</option>';
            if (data.Result == true) {
                $.each(data.List.listDocs, function (key, value) {
                    listitems += '<option value=' + "'" + value.DocumentID + "'" + '>' + value.DocDesc + '</option>';
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
            alert("Critical Error!. Failed to call the server.");
        }
    });
}
var bindAllRoles = function (cntrlID) {
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetAllRoles',
        dataType: "json",
        success: function (data) {
            listitems = '<option value=0>--SELECT--</option>';
            if (data.Result == true) {
                $.each(data.List.listRoles, function (key, value) {
                    listitems += '<option value=' + "'" + value.RoleID + "'" + '>' + value.RoleName + '</option>';
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
            alert("Critical Error!. Failed to call the server.");
        }
    });
}
var getAllRolesAsString = function () {
    var items = '';
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetAllRoles',
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.Result == true) {
                $.each(data.List.listRoles, function (key, value) {
                    if (value.Active == "True") {
                        items += value.RoleID + "-" + value.RoleName + '|';
                    }
                });
            }
        },
        error: function (xhr) {

        }
    });

    return items;
}
var getUploadedDocs = function (VehicleID, DriverGuardId) {

    $('.enableLoader').show();
    $.ajax({
        url: urlPref + '/Common/GetUploadedDocsDetails',
        type: "POST",
        data: {
            'VehicleID': VehicleID,
            'DriverGuardID': DriverGuardId
        },

        success: function (data) {
            $('#divUploadedDocsDetails').html('');
            $('#divUploadedDocsDetails').html(data);
            $('.enableLoader').hide();
        },
        error: function (err) {
            $('.enableLoader').hide();
            alert('Error: ' + err.responseText);
        }
    });
}
var validateMobile = function (input) {
    var filter = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
    if (filter.test(input)) {
        input = input.replace(/\D+/g, '');
        if (input.length == 10) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }

}
var checkNumeric = function (cntrlID) {
    $(cntrlID).keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                e.preventDefault();
            }
        }
    });
}
var validateEmail = function (input) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (!filter.test(input) || input.length < 8) {
        return false;
    }
    else {
        return true;
    }

}
var bindwithGoogleAPI = function (cntrlID) {
    var optionscity;
    if (cntrlID.search('rental')) {
        optionscity = {
            types: ['(cities)'],
            componentRestrictions: { country: "in" }
        };
    }
    else {
        optionscity = {
            types: ['establishment']
        };
    }
    autocomplete = new google.maps.places.Autocomplete((document.getElementById(cntrlID)),
           optionscity);

}
var getEmployeeDetails = function (empID) {
    return $.parseJSON($.ajax({
        url: urlPref + '/UserBucket/GetEmployeeDetails',
        type: "GET",
        data: { 'EmployeeCode': empID },
        contenttype: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
        },
        error: function (err) {
            alert("Error::App not responding");
        }
    }).responseText);
}
var bindReason = function (groupName, cntrlID) {
    $.ajax({
        type: "GET",
        url: urlPref + '/Common/GetReasonList',
        dataType: "json",
        data: { 'GroupName': groupName },
        success: function (data) {
            listitems = '<option value=0>--SELECT--</option>';
            if (data.Result == true) {
                $.each(data.List.reasonList, function (key, value) {
                    listitems += '<option value=' + "'" + value.ReasonID + "'" + '>' + value.ReasonName + '</option>';
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
            alert("Critical Error!. Failed to call the server.");
        }
    });
}
var bindTripStatus = function (cntrlID, groupName, displayFor) {
    $.ajax({
        url: urlPref + '/Common/GetTripStatusList',
        type: "GET",
        data: { 'DisplayFor': displayFor, 'GroupName': groupName },
        contenttype: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            var listitems = '<option value=0>--Select--</option>'
            if (data.Result == true) {

                $.each(data.List.listTripStatus, function (key, value) {
                    listitems += '<option value=' + "'" + value.StatusCode + "'" + '>' + value.StatusName + '</option>';
                });
                if (groupName == '' || groupName == undefined || groupName ==null)
                listitems += '<option value=-1>ALL</option>'

            }
            else {
                listitems += '<option value=-1>ALL</option>'
            }

            $(cntrlID).empty();
            $(cntrlID).append(listitems);
        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });
}
var viewTripDetails = function (ReqID) {

    $('#divViewTripDetails').modal('toggle');
    $('#lblTripStatus').text($('#hdnStatusName_' + ReqID).val());
    $('#lblVehicleType').text($('#hdnVehhicleName_' + ReqID).val());
    $('#lblSourceLandmark').text($('#hdnSourceLandMark_' + ReqID).val());
    $('#lblDestinationLandmark').text($('#hdnDestinationLandMark_' + ReqID).val());
    $('#lblRequestedByName').text($('#hdnRequestedByName_' + ReqID).val());
    $('#lblVendorName').text($('#hdnVendorName_' + ReqID).val());
    $('#lblTripType').text($('#hdnTripType_' + ReqID).val() == "Y" ? "Official" : "Personal");

    $.ajax({
        url: urlPref + '/UserBucket/GetAdhocRequestHistory',
        type: "GET",
        data: { 'RequestID': ReqID },
        contenttype: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            $('#divHistory').html('');
            $('#divHistory').html(data);
        },
        error: function (err) {
            alert("Error::Not Responding");
        }
    });
}
var showProfilePicPopup = function () {
    $('#mdChangePP').modal('toggle');
}
var updateProfilePic = function () {

    var fileInfor = $("#fuProfile").val();
    var extension = fileInfor.split('.').pop().toUpperCase();
    if (fileInfor.length < 1) {
        fileInfor = 0;
        alert("Please select image to update your profile picture.");
        return false;
    }
    else if (extension != "PNG" && extension != "JPG" && extension != "GIF" && extension != "JPEG") {
        fileInfor = 0;
        alert("Please select [PNG], [JPG], [JPEG] or [GIF] image only.");
        return false;
    }
    else {


        if (window.FormData !== undefined) {
            var fileUpload = $("#fuProfile").get(0);
            var size = parseFloat(fileUpload.files[0].size / 1024).toFixed(2);

            if (size > 50) {
                alert("Picture size should be less or equal to 50 KB only.");
                return false;
            }
            else {

                var files = fileUpload.files;
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
            }
        }

        $.ajax({
            url: urlPref + '/Common/UploadProfilePic',
            type: "POST",
            data: fileData,
            dataType: "json",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function (res) {
                if (res.Result == true) {

                    alert("Your profile picture was updated successfully.");
                    $('#ImgPrflBiglId').attr("src", $("#hdnPrfPicPath").val() + res.FilePath);
                    $('#ImgPrflSmalllId').attr("src", $("#hdnPrfPicPath").val() + res.FilePath);
                    $('#imgupdateProfilePic').attr("src", $("#hdnPrfPicPath").val() + res.FilePath);
                }
                else {
                    $('.enableLoader').hide();
                    alert("There is some issue in uploading profile picture.");
                }

                $('#mdChangePP').modal('toggle');
            },
            error: function () {

            }

        });
    }
}

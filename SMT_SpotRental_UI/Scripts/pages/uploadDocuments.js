/*--------------------------------------------------------------------------------------  
 *------------------------SPOT RENTAL uploadDocuments.js-----------------------------------------
 *  
 * uploadDocuments.js is jquery file which having functions to upload document for vehicle and driver/Guard.
 *
 * @Author      Kapil D. Tripathi 
 * @Email      <ktripathi@ictrlbiz.com>
 * @Copyright  iCtrlBiz Consulting pvt ltd.
 * @version    1.0.0
 *-------------------------------------------------------------------------------------*/
$(document).ready(function () {
    bindDocumentList('#ddlDocumentType', 'V');   
    //$('#btnUploadDocs').on('click', function () { uploadVehicleDocuments() });
    $('#btnUploadDocs').on('click', function () { uploadDocuments() });
    $('.thumbnail').mouseover(function () { showhideDive('block') });
    $('.thumbnail').mouseout(function () { showhideDive('none') });
});
var showhideDive = function (act) {
    $('.caption').css('display', act);
}
var closePopups = function (divId) {
    $(divId).modal('hide');
}
var uploadDocuments = function () {
     
    var VehicleID = $('#hdnVehicleIdForUploadDocs').val();
    var DriverGuardID = $('#hdnDriverIdForUploadDocs').val();
    
    var docID = $('#ddlDocumentType').val();
    if (docID == '' || docID == '0') {
        alert("Please select document type!");
        return false;
    }
    else {
        if (VehicleID != '0')
        {
            DriverGuardID = 0;
            uploadVehicleDriverDocuments(VehicleID, DriverGuardID, docID);
        }
            
        else if (DriverGuardID != '0')
        {
            VehicleID = 0;
            uploadVehicleDriverDocuments(VehicleID, DriverGuardID, docID);
        }
            
    }

}
var uploadVehicleDriverDocuments = function (VehicleID, DriverGuardID, docID) {

    var fileInfor = $("#fuUploadDocument").val();
    var extension = fileInfor.split('.').pop().toUpperCase();
    if (fileInfor.length < 1) {
        fileInfor = 0;
        alert("Please select document/files to upload.");
        return false;
    }
    else if (extension != "PNG" && extension != "JPG" && extension != "PDF" && extension != "JPEG") {
        fileInfor = 0;
        alert("Please select [PNG], [JPG], [JPEG] or [PDF] image/docs only.");
        return false;
    }
    else {


        if (window.FormData !== undefined) {
            var fileUpload = $("#fuUploadDocument").get(0);
            var size = parseFloat(fileUpload.files[0].size / 1024).toFixed(2);

            if (size / 1024 > 3) {
                alert("Picture size should be less or equal to 3 MB only.");
                return false;
            }
            else {

                $('.enableLoader').show();

                var files = fileUpload.files;
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }


            }
        }
        var alertMsg = VehicleID != '0' ? "Your vehicle document uploaded successfully." : "Your Driver/Guard document uploaded successfully."
        $.ajax({
            url: urlPref + '/Common/UploadDocument',
            type: "POST",
            data: fileData,
            dataType: "json",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function (res) {
                if (res.Result == true) {
                    var req = {
                        'VehicleID': VehicleID,
                        'DriverGuardID': DriverGuardID,
                        'DocumentID': docID,
                        'DocumentFileName': res.FilePath,
                        'Active': 'Y'
                    };
                    //----------------------------------------//
                    $.ajax({
                        url: urlPref + '/MasterItems/UploadDocumentDetails',
                        type: "POST",
                        data: JSON.stringify({ 'request': req }),
                        dataType: "json",
                        contentType: 'application/json;utf-8',
                        success: function (res) {
                            if (res.Result == "TRUE") {
                                $('.enableLoader').hide();
                                alert(alertMsg);
                                getUploadedDocs(VehicleID, DriverGuardID);
                            }
                            else {
                                $('.enableLoader').hide();
                                alert("There is some issue in uploading vehicle document. Please try after some time");
                            }
                            $('#ddlDocumentType').val('0');
                            $('#fuUploadDocument').val('');

                        },
                        error: function () {
                            $('.enableLoader').hide();
                            alert("Error: There is some issue in uploading vehicle document.");
                        }

                    });
                    //-----------------------------------------//

                }
                else {
                    $('.enableLoader').hide();
                    alert("There is some issue in uploading vehicle document.");
                }


            },
            error: function () {

            }

        });
    }
}

//var uploadVehicleDocuments = function () {
//    var VehicleID = $('#hdnVehicleIdForUploadDocs').val();
   
//    var docID = $('#ddlDocumentType').val();

//    if (VehicleID == '' || VehicleID == '0' || VehicleID == undefined) {
//        alert("This vendor could not be found!");
//        return false;
//    }
//    else if (docID == '' || docID == '0') {
//        alert("Please select document type!");
//        return false;
//    }
//    var fileInfor = $("#fuUploadDocument").val();
//    var extension = fileInfor.split('.').pop().toUpperCase();
//    if (fileInfor.length < 1) {
//        fileInfor = 0;
//        alert("Please select document/files to upload.");
//        return false;
//    }
//    else if (extension != "PNG" && extension != "JPG" && extension != "PDF" && extension != "JPEG") {
//        fileInfor = 0;
//        alert("Please select [PNG], [JPG], [JPEG] or [PDF] image/docs only.");
//        return false;
//    }
//    else {


//        if (window.FormData !== undefined) {
//            var fileUpload = $("#fuUploadDocument").get(0);
//            var size = parseFloat(fileUpload.files[0].size / 1024).toFixed(2);

//            if (size / 1024 > 3) {
//                alert("Picture size should be less or equal to 3 MB only.");
//                return false;
//            }
//            else {

//                $('.enableLoader').show();

//                var files = fileUpload.files;
//                var fileData = new FormData();
//                for (var i = 0; i < files.length; i++) {
//                    fileData.append(files[i].name, files[i]);
//                }


//            }
//        }

//        $.ajax({
//            url: urlPref + '/Common/UploadDocument',
//            type: "POST",
//            data: fileData,
//            dataType: "json",
//            contentType: false, // Not to set any content header
//            processData: false, // Not to process data
//            success: function (res) {
//                if (res.Result == true) {
//                    var req = {
//                        'VehicleID': VehicleID,
//                        'DocumentID': docID,
//                        'DocumentFileName': res.FilePath,
//                        'Active':'Y'
//                    };
//                    //----------------------------------------//
//                    $.ajax({
//                        url: urlPref + '/MasterItems/UploadDocumentDetails',
//                        type: "POST",
//                        data: JSON.stringify({ 'request': req }),
//                        dataType: "json",
//                        contentType: 'application/json;utf-8',
//                        success: function (res) {
//                            if (res.Result == "TRUE") {
//                                $('.enableLoader').hide();
//                                alert("Your vehicle document uploaded successfully.");
//                                getUploadedDocs(VehicleID);
//                            }
//                            else {
//                                $('.enableLoader').hide();
//                                alert("There is some issue in uploading vehicle document. Please try after some time");
//                            }
//                            $('#ddlDocumentType').val('0');
//                            $('#fuUploadDocument').val('');

//                        },
//                        error: function () {
//                            $('.enableLoader').hide();
//                            alert("Error: There is some issue in uploading vehicle document.");
//                        }

//                    });
//                    //-----------------------------------------//

//                }
//                else {
//                    $('.enableLoader').hide();
//                    alert("There is some issue in uploading vehicle document.");
//                }


//            },
//            error: function () {

//            }

//        });
//    }
//}
var viewUploadedImage=function(docsFileName)
{
    $("#divViewDocuments").modal('show');

    $('#divViewImg').css('display', 'block');
    $('#imgView').css('display', 'block');

    $('#hrfdwn' ).attr('download',  docsFileName);
    $('#hrfdwn').attr('href', $("#hdnDocsPath").val() + docsFileName);
    $('#hrfdwn').css('display', 'block');
    var fileicons = docsFileName.split('.');
    
    if (fileicons[1] == 'pdf' || fileicons[1] == 'PDF') {
        $('#imgView' ).attr('src', '../images/pdf_icon.jpg');
        $('#hrfview' ).css('display', 'none');
    }
    else {
        $('#hrfview').css('display', 'block');
        $('#imgView').attr('src', $("#hdnDocsPath").val() + docsFileName);
    }

  
}
var viewImageFile = function () {
    var src = $('#imgView' ).attr('src');
    var img = '<img src="' + src + '" class="img-responsive" style="border: 2px solid #263E53; max-width:100%; max-height:100%"/>';
    $('#myModal').modal();
    $('#myModal').on('shown.bs.modal', function () {
        $('#myModal .modal-body').html(img);
    });
    $('#myModal').on('hidden.bs.modal', function () {
        $('#myModal .modal-body').html('');
    });

}


/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload();

    $('#fileupload').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload',
        downloadTemplateId: 'template-download',
        complete: function (e, data) {
            window.location = "/UploadXMLNFE/Index";
        }
    });    
    
    $('#fileuploadOne').fileupload();

    $('#fileuploadOne').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-one',
        downloadTemplateId: 'template-download-one'
    });
});

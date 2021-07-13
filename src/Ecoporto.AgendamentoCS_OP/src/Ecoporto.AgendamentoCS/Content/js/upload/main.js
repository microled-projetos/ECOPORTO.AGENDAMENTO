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
        downloadTemplateId: 'template-download'
    });
    
    $('#fileuploadAlteracao').fileupload();

    $('#fileuploadAlteracao').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-files-update',
        downloadTemplateId: 'template-download-files-update'
    });
    $('#fileuploadAlteracao2').fileupload();

    $('#fileuploadAlteracao2').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-files-update-2',
        downloadTemplateId: 'template-download-files-update-2'
    });
    $('#fileuploadAlteracao3').fileupload();

    $('#fileuploadAlteracao3').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-files-update-3',
        downloadTemplateId: 'template-download-files-update-3'
    });
    $('#fileuploadAlteracao4').fileupload();

    $('#fileuploadAlteracao4').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-files-update-4',
        downloadTemplateId: 'template-download-files-update-4'
    });
    $('#fileuploadOne').fileupload();

    $('#fileuploadOne').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-one',
        downloadTemplateId: 'template-download-one'
    });
    $('#fileuploadTwo').fileupload();

    $('#fileuploadTwo').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-two',
        downloadTemplateId: 'template-download-two'
    });
    $('#fileuploadThree').fileupload();

    $('#fileuploadThree').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-three',
        downloadTemplateId: 'template-download-three'
    });
    $('#fileuploadFour').fileupload();

    $('#fileuploadFour').fileupload('option', {
        maxFileSize: 500000000,
        resizeMaxWidth: 400,
        resizeMaxHeight: 300,
        uploadTemplateId: 'template-upload-four',
        downloadTemplateId: 'template-download-four'
    });
    $('#uploadDocumentos').fileupload();

    $('#uploadDocumentos').fileupload('option', {
            maxFileSize: 500000000,
            resizeMaxWidth: 400,
            resizeMaxHeight: 300,
            uploadTemplateId: 'template-upload-files',
            downloadTemplateId: 'template-download-files'
    });

    //$('#uploadUsuarioImagem').fileupload();
    //$('#uploadUsuarioImagem').fileupload('option', {
    //    maxFileSize: 500000000,
    //    resizeMaxWidth: 400,
    //    resizeMaxHeight: 300,
    //    uploadTemplateId: 'template-upload-usuario-imagem',
    //    downloadTemplateId: 'template-download-usuario-imagem'
    //});    
});

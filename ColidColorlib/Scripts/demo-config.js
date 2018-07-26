$(function () {
    $('#drag-and-drop-zone').dmUploader({ //
        destroy: true,
        url: '/Invitations/UploadInvitations',
        maxFileSize: 3000000, // 3 Megs max
        multiple: false,
        allowedTypes: 'image/*',
        extFilter: ['jpg', 'jpeg', 'png', 'gif'],
        onDragEnter: function () {
            // Happens when dragging something over the DnD area
            this.addClass('active');
        },
        onDragLeave: function () {
            // Happens when dragging something OUT of the DnD area
            this.removeClass('active');
        },
        onInit: function () {
            // Plugin is ready to use
            ui_add_log('Penguin initialized :)', 'info');
        },
        onComplete: function () {
            // All files in the queue are processed (success or error)
            ui_add_log('All pending tranfers finished');
        },
        onNewFile: function (id, file) {

            ui_add_log('New file added #' + id);
            ui_multi_add_file(id, file);

        },
        onBeforeUpload: function (id) {
            // about tho start uploading a file
            ui_add_log('Starting the upload of #' + id);
            ui_multi_update_file_status(id, 'uploading', 'Uploading...');
            ui_multi_update_file_progress(id, 0, '', true);
        },
        onUploadCanceled: function (id) {
            // Happens when a file is directly canceled by the user.
            ui_multi_update_file_status(id, 'warning', 'Canceled by User');
            ui_multi_update_file_progress(id, 0, 'warning', false);
        },
        onUploadProgress: function (id, percent) {
            // Updating file progress
            ui_multi_update_file_progress(id, percent);
        },
        onUploadSuccess: function (id, data) {
            if (data.key) {
                var imgsrc = data.path;
                $(".img-uploaded").prop("src", imgsrc);
                // A file was successfully uploaded
                ui_add_log('Server Response for file #' + id + ': ' + JSON.stringify(data.value));
                ui_add_log('Upload of file #' + id + ' COMPLETED', 'success');
                ui_multi_update_file_status(id, 'success', 'Upload Complete');
                ui_multi_update_file_progress(id, 100, 'success', false);
                $.toast({
                    heading: 'Success',
                    text: data.value,
                    showHideTransition: 'slide',
                    icon: 'success'
                });
                Invitation.initInvitationList();
            }
            else {
                ui_add_log('Server Response for file #' + id + ': ' + JSON.stringify(data.value));
                ui_add_log('Upload of file #' + id + ' FAILED', 'danger');
                ui_multi_update_file_status(id, 'danger', 'Upload Failed');
                ui_multi_update_file_progress(id, 100, 'danger', false);
                $.toast({
                    heading: 'Error',
                    text: data.value,
                    showHideTransition: 'fade',
                    icon: 'error'
                });
            }

        },
        onUploadError: function (id, xhr, status, message) {
            ui_multi_update_file_status(id, 'danger', message);
            ui_multi_update_file_progress(id, 0, 'danger', false);
        },
        onFallbackMode: function () {
            // When the browser doesn't support this plugin :(
            ui_add_log('Plugin cant be used here, running Fallback callback', 'danger');
        },
        onFileSizeError: function (file) {
            ui_add_log('File \'' + file.name + '\' cannot be added: size excess limit', 'danger');
        }
    });
});
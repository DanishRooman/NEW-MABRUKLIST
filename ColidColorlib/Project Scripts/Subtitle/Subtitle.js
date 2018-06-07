var Subtitle = function () {
    //private Static
    var handleCreateSubtitle = function () {

        //Ajax call
        $.ajax({
            url: '/Subtitle/AddSubtitle',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateSubtitle").empty();
                $("#CreateSubtitle").html(result);
                $("#CreateSubtitle").modal("show");

            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleSubtitleSucsess = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateSubtitle").modal("hide");
            handleSubtitleList();

        }
        else {
            $.toast({
                heading: 'Error',
                text: result.value,
                showHideTransition: 'fade',
                icon: 'error'
            });
        }
    };

    var handleSubtitleList = function () {
        $.ajax({
            url: '/Subtitle/SubtitleListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divSubtitle").empty();
                $("#divSubtitle").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });

    }

    var handleEditSubtitle = function (id) {
        $.ajax({
            url: '/Subtitle/GetSubtitle',
            type: 'GET',
            dataType: 'html',
            data: { "id": id },
            success: function (result) {
                if (result.key == false) {
                    $.toast({
                        heading: 'Error',
                        text: result.value,
                        showHideTransition: 'fade',
                        icon: 'error'
                    });
                }
                else {
                    $("#CreateSubtitle").empty();
                    $("#CreateSubtitle").html(result);
                    $("#CreateSubtitle").modal("show");
                }

            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleDeleteSubtitle = function (id) {
        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this Subtitle?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Subtitle/DeleteSubtitle',
                            type: 'GET',
                            dataType: 'json',
                            data: { "id": id },
                            success: function (result) {
                                if (result.key) {
                                    $.toast({
                                        heading: 'Success',
                                        text: result.value,
                                        showHideTransition: 'slide',
                                        icon: 'success'
                                    });
                                    handleSubtitleList();
                                }
                                else {
                                    $.toast({
                                        heading: 'Error',
                                        text: result.value,
                                        showHideTransition: 'fade',
                                        icon: 'error'
                                    });
                                }
                            },
                            error: function () {
                                console.log("Error");
                            }
                        });
                    }
                },
                cancel: function () {

                },
            }
        });

    };

    return {

        initCreateSubtitle: function () {
            handleCreateSubtitle();
        },
        initSubtitleSucsess: function (result) {

            handleSubtitleSucsess(result);
        },
        initSubtitleList() {

            handleSubtitleList();
        },
        initEditSubtitle: function (id) {

            handleEditSubtitle(id);
        },
        initDeleteSubtitle: function (id) {
            handleDeleteSubtitle(id);
        },
    }

}();
$(function () {
    Subtitle.initSubtitleList();

});
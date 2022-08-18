$(".update-board-btn").on('click', function () {
    var boardName = $(this).attr("data-board-name");
    var boardId = $(this).attr("data-board-id");

    $("#update-board-partial").toggleClass("hidden");

    var model = {
        BoardId: boardId,
        Name: boardName
    };

    $.ajax({
        type: "POST",
        url: "/Board/UpdatePartial",
        data: model,
        success: function (result) {
            $("#update-board-partial").empty();
            $("#update-board-partial").html(result);
        }
    });
});

$(".add-job-btn").on('click', function () {
    var boardId = $(this).attr("data-board-id");
    var jobListId = $(this).attr("data-list-id");
    var boardName = $(this).attr("data-board-name");
    var jobListName = $(this).attr("data-list-name");

    $("#add-job-partial").toggleClass("hidden");

    var model = {
        BoardId: boardId,
        JobListId: jobListId,
        JobListName: jobListName,
        BoardName: boardName
    };

    $.ajax({
        type: "POST",
        url: "/Job/CreatePartial",
        data: model,
        success: function (result) {
            $("#add-job-partial").empty();
            $("#add-job-partial").html(result);
        }
    });
});

$(".delete-job-btn").on('click', function () {

    var jobId = $(this).attr("data-job-id");
    var jobTitle = $(this).attr("data-job-title");
    var jobCompany = $(this).attr("data-job-company");
    var boardId = $(this).attr("data-board-id");

    $("#delete-job-partial").toggleClass("hidden");

    var model = {
        JobId: jobId,
        BoardId: boardId,
        JobTitle: jobTitle,
        JobCompany: jobCompany
    };

    $.ajax({
        type: "POST",
        url: "/Job/DeletePartial",
        data: model,
        success: function (result) {
            $("#delete-job-partial").empty();
            $("#delete-job-partial").html(result);
        }
    });
});

$(".delete-board-btn").on('click', function () {

    var boardId = $(this).attr("data-board-id");
    var boardName = $(this).attr("data-board-name");

    $("#delete-board-partial").toggleClass("hidden");

    var model = {
        BoardId: boardId,
        BoardName: boardName
    };

    $.ajax({
        type: "POST",
        url: "/Board/DeletePartial",
        data: model,
        success: function (result) {
            $("#delete-board-partial").empty();
            $("#delete-board-partial").html(result);
        }
    });
});

$(".create-board-btn").on('click', function () {
    $("#create-board-partial").toggleClass("hidden");
});
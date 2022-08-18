$(".update-board-btn").on('click', function () {
    var boardName = $(this).attr("data-name");
    var boardId = $(this).attr("data-id");

    $("#update-partial").toggleClass("hidden");

    var model = {
        BoardId: boardId,
        Name: boardName
    };

    $.ajax({
        type: "POST",
        url: "/Board/UpdatePartial",
        data: model,
        success: function (result) {
            $("#update-partial").empty();
            $("#update-partial").html(result);
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
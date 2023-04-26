
$(document).ready(function () {
    loadData();
});

function loadData() {
    let id = getUrlParameter("id");
    $("#content").html('');
    ShowLoading();

    $.get(`/Client/Home/GetDetail/${id}`, function (data, status) {
        console.log(data);
        $("#content").html(data.data.content);
        HiddenLoading();
    }).fail(function () {
        toastr.error("Quá trình xử lý đã có lỗi xảy ra!");
        HiddenLoading();
    });
}

function ShowLoading() {
    $("#loading").removeClass('hidden');
}

function HiddenLoading() {
    $("#loading").addClass('hidden');
}
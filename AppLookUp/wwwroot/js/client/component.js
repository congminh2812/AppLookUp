
$(document).ready(function () {
    loadData("");

    $("#search").on('input', $.debounce(400, function (event) {
        loadData(event.target.value);
    }));
});

function loadData(keyword) {
    $("#content").html('');
    ShowLoading();

    $.get(`/Client/Component/GetList?keyword=${keyword}`, function (data, status) {
        let html = '';


        if (!data || data.data.length == 0)
            html = `
                <div class="alert alert-primary mt-2" role="alert">
                  Không có dữ liệu !
                </div>
            `;

        for (let item of data.data) {
            let name = item.name.replace(keyword, `<span class="bg-warning">${keyword}</span>`);
            let content = item.content;

            html += `
                <div class="list border-bottom">
                    <i class="bi bi-bookmark-fill text-primary"></i>
                    <div class="d-flex flex-column ml-3">
                        <a class="text-hover-primary"><strong>${name}</strong></a>
                        <small class="text-dark">${content}</small>
                    </div>
                 </div>
            `;
        }

        $("#content").html(html);
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
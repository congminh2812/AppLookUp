
$(document).ready(function () {
    loadData("");

    $("#search").on('input', $.debounce(400, function (event) {
        loadData(event.target.value);
    }));
});

function loadData(keyword) {
    $("#content").html('');
    ShowLoading();

    $.get(`/Client/Home/GetList?keyword=${keyword}`, function (data, status) {
        let html = '';
    

        if (!data || data.data.length == 0)
            html = `
                <div class="alert alert-primary mt-2" role="alert">
                  Không có dữ liệu !
                </div>
            `;

        for (let item of data.data) {
            let content = item.content.split(" <p>")[0];
            let subContent = content.substring(0, content.length > 100 ? 100 : content.length);

            html += `
                <div class="list border-bottom">
                    <i class="bi bi-bookmark-fill text-primary"></i>
                    <div class="d-flex flex-column ml-3">
                        <a class="text-hover-primary"><strong>${item.name}</strong></a>
                        <small>${subContent}</small>
                    </div>
                 </div>
            `;
        }

        console.log(html);
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
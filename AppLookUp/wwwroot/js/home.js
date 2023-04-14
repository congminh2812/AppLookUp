
$(document).ready(function () {
    $("#search").on('input', $.debounce(400, function (event) {
        loadData(event.target.value);
    }));
});

function loadData(keyword) {
    $("#list").html(`<div class="spinner-border text-primary" role="status">
                <span class= "visually-hidden">Loading...</span>
            </div>`);

    $.get(`/Client/Home/GetList?keyword=${keyword}`, function (data, status) {
        let html = '';

        for (let item of data.data) {
            html += `
                <div class="list border-bottom">
                    <i class="bi bi-bookmark-fill text-primary"></i>
                    <div class="d-flex flex-column ml-3">
                        <a class="text-hover-primary"><strong>${item.name}</strong></a>
                        <small>${item.content.substring(0, item.content.length > 100 ? 100 : item.content.length)}</small>
                    </div>
                 </div>
            `;
        }

        $("#list").html(html);
    });
}
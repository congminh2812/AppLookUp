var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/vi.json',
        },
        "ajax": {
            "url": "/Admin/Information/GetAll"
        },
        columns: [
            { "data": "name", "width": "15%" },
            { "data": "typeInfo.name", "width": "15%" },
            { "data": "content", "width": "55%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <a href="/Admin/Information/Upsert?id=${data}" class="btn btn-primary mx-2"> 
                            <i class="bi bi-pencil-square"></i> Sửa
                         </a>
                         <a onclick="Delete('thông tin', '/Admin/Information/Delete/${data}')" class="btn btn-danger mx-2 mt-1"> <i class="bi bi-trash-fill"></i> Xóa</a>
                    `;
                },
                "width": "15%"
            },
        ]
    });
}

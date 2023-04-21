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
            "url": "/Admin/TypeInfo/GetAll"
        },
        columns: [
            { "data": "name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <a href="/Admin/TypeInfo/Upsert?id=${data}" class="btn btn-primary mx-2"> 
                            <i class="bi bi-pencil-square"></i> Sửa
                         </a>
                         <a onclick="Delete('loại thông tin', '/Admin/TypeInfo/Delete/${data}')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Xóa</a>
                    `;
                },
                "width": "15%"
            },
        ]
    });
}
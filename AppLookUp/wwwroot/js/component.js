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
            "url": "/Admin/Component/GetAll"
        },
        columns: [
            { "data": "name", "width": "15%" },
            { "data": "content", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <a href="/Admin/Component/Upsert?id=${data}" class="btn btn-primary mx-2"> 
                            <i class="bi bi-pencil-square"></i> Sửa
                         </a>
                         <a onclick="Delete('/Admin/Component/Delete/${data}')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Xóa</a>
                    `;
                },
                "width": "15%"
            },
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Xóa component',
        text: "Bạn có muốn xóa?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Không',
        confirmButtonText: 'Có, xóa nó!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else
                        toastr.error(data.message);
                }
            });
        }
    });
}
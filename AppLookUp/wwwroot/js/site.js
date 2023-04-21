function Delete(title, url) {
    Swal.fire({
        title: `Xóa ${title}`,
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
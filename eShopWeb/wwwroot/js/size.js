﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Size/GetAll"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "type", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-30 btn-group" role="group">
                        <a href="/Admin/Size/Upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>&nbsp;Edit</a>
                        <a onClick=Delete('/Admin/Size/Delete/+${data}') class="btn btn-danger mx-2"><i class="bi bi-x-circle"></i>&nbsp;Delete</a>
                   </div>
                    `
                },
                "width": "15 % "
            },
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

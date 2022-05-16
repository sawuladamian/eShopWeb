var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData2').DataTable({
        "ajax": {
            "url": "/Admin/Tshirt/GetAll"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            {
                "data": "imageUrl",
                "render": function (data) {
                    return '<img style="height:70px;width:70px;" src="' + data + '">'

                },
                "width": "15 % "
            },
            { "data": "name", "width": "15%" },
            { "data": "price", "width": "15%" },
                       
            { "data": "size.type", "width": "7%" },
              {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-30 btn-group" role="group">
                        <a href="/Admin/Tshirt/Upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>&nbsp;Edit</a>
                        <a onClick=Delete('/Admin/Tshirt/Delete/+${data}') class="btn btn-danger mx-2"><i class="bi bi-x-circle"></i>&nbsp;Delete</a>
                   </div>
                    `
                },
                "width": "10 % "
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
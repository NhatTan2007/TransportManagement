function confirmDelete(id) {
    bootbox.confirm({
        size: "small",
        message: "Bạn muốn xóa phân quyền này?",
        buttons: {
            cancel: {
                label: 'Không',
                className: 'btn-dark pl-3 pr-3'
            },
            confirm: {
                label: 'Có',
                className: 'btn-danger pr-3 pl-3'
            }
        },
        callback: async function (result) {
            if (result) {
                window.location.href = `Role/Delete?roleId=${id}`;
            }
        }
    })
}
var role = role || {}
const editRoleName = document.getElementById("EditRoleName");
const editRoleId = document.getElementById("EditRoleId");
const editRolePriority = document.getElementById("EditRolePriority");
role.edit = async function ({ target }) {
    let roleId = target.id;
    var ajaxGetRole = await $.ajax({
        url: '/Role/GetRole',
        method: 'GET',
        contentType: 'application/json',
        data: { roleId: roleId },
        dataType: 'json'
    }).done(function (data) {
        editRoleName.value = data.roleName;
        editRolePriority.value = data.rolePriority;
        editRoleId.value = data.roleId;
    });
    $("#editRole").modal();
}
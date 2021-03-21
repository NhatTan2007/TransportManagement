var role = role || {}
const editRoleName = document.getElementById("EditRoleName");
const editRoleId = document.getElementById("EditRoleId");
const editRolePriority = document.getElementById("EditRolePriority");
const editRoleActive = document.getElementById("EditIsActive");
role.edit = async function (id) {
    await $.ajax({
        url: '/Role/GetRole',
        method: 'GET',
        contentType: 'application/json',
        data: { roleId: id },
        dataType: 'json'
    }).done(function (data) {
        editRoleName.value = data.roleName;
        editRolePriority.value = data.rolePriority;
        editRoleId.value = data.roleId;
        editRoleActive.checked = data.isActive;
    });
    $("#editRole").modal();
}
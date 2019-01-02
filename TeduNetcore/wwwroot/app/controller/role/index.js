var roleController = function () {
    this.initialize = function () {
        RegisterEvent();
        LoadRoles();
        //LoadPermission();
    }

    function RegisterEvent() {
        $('body').on('click', '.btn-grant', function (e) {
            var roleId = $(this).data('id');
            $('#hdmId').val(roleId);
            LoadFunction();
            FillPermission(roleId);
            //$.when(function () {
            //    return ;
            //}).done();
            $('#modal-grantpermission').modal('show');
        });

        // Handle check all
        $('#ckCheckAllView').on('click', function () {
            var isCheck = $('#ckCheckAllView:checked').length > 0;
            $('input[type=checkbox].ckView').prop('checked', isCheck);
        });
        $('#ckCheckAllCreate').on('click', function () {
            var isCheck = $('#ckCheckAllCreate:checked').length > 0;
            $('input[type=checkbox].ckAdd').prop('checked', isCheck);
        });
        $('#ckCheckAllEdit').on('click', function () {
            var isCheck = $('#ckCheckAllEdit:checked').length > 0;
            $('input[type=checkbox].ckEdit').prop('checked', isCheck);
        });
        $('#ckCheckAllDelete').on('click', function () {
            var isCheck = $('#ckCheckAllDelete:checked').length > 0;
            $('input[type=checkbox].ckDelete').prop('checked', isCheck);
        });
        $('#btnSavePermission').on('click', function () {
            var listPermissions = [];
            $.each($('#tblFunction tbody tr'), function (i, element) {
                listPermissions.push({
                    roleId: $('#hdmId').val(),
                    functionId: $(element).data('id'),
                    canRead: $(element).find('.ckView').prop('checked'),
                    canCreate: $(element).find('.ckAdd').prop('checked'),
                    canUpdate: $(element).find('.ckEdit').prop('cheked'),
                    canDelete: $(element).find('.ckDelete').prop('checked')
                });
            });
            $.ajax({
                type: 'POST',
                url: '/admin/role/savePermisssion',
                data: {
                    listPermmission: listPermissions
                },
                dataType: 'json',
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (statusCode) {
                    tedu.notify('Save permission success', 'success');
                    tedu.stopLoading();
                    $('#modal-grantpermission').modal('hide');
                },
                error: function (error) {
                    tedu.stopLoading();
                    tedu.notify('Fail to save permission', 'error');
                }
            });
        });
    }

        function FillPermission(roleId) {
            $.ajax({
                type: 'GET',
                url: '/admin/role/GetPermission',
                data: {
                    roleId: roleId
                },
                beforeSend: function () {
                    tedu.startLoading();
                },
                dataType: 'json',
                success: function (permissions) {
                    var element = $('#tblFunction tbody tr');
                    $.each(element, function (i, element) {
                        $.each(permissions, function (index, permission) {
                            if (permission.FuctionId === $(element).data('id')) {
                                $(element).find('.ckView').prop('checked', permission.CanRead);
                                $(element).find('.ckAdd').prop('checked', permission.CanCreate);
                                $(element).find('.ckEdit').prop('checked', permission.CanUpdate);
                                $(element).find('.ckDelete').prop('checked', permission.CanDelete);
                            }
                        });
                    });
                    var canViewAll = HandleCheckAll('view', permissions);
                    $('input[type=checkbox]#ckCheckAllView').prop('checked', canViewAll);
                    var canCreateAll = HandleCheckAll('create', permissions);
                    $('input[type=checkbox]#ckCheckAllCreate').prop('checked', canCreateAll);
                    var canEditAll = HandleCheckAll('update', permissions);
                    $('input[type=checkbox]#ckCheckAllEdit').prop('checked', canEditAll);
                    var canDeleteAll = HandleCheckAll('delete', permissions);
                    $('input[type=checkbox]#ckCheckAllDelete').prop('checked', canDeleteAll);
                    tedu.stopLoading();
                },
                error: function (error) {
                    console.log(error);
                    tedu.notify("Assign permission fail", 'error');
                    tedu.stopLoading();
                }
            });
        }

        function HandleCheckAll(action, permissions) {
            if (permissions.length === 0) {
                return false;
            }
            for (i = 0; i < permissions.length; i++) {
                switch (action) {
                    case 'view':
                        if (permissions[i].CanRead === false) {
                            return false;
                        }
                        break;
                    case 'create':
                        if (permissions[i].CanCreate === false) {
                            return false;
                        }
                        break;
                    case 'update':
                        if (permissions[i].CanUpdate === false) {
                            return false;
                        }
                        break;
                    case 'delete':
                        if (permissions[i].CanDelete === false) {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        function LoadRoles() {
            $.ajax({
                type: 'GET',
                url: '/admin/role/GetAll',
                dataType: 'json',
                success: function (roles) {
                    var template = $('#table-template').html();
                    var render = '';
                    roles.forEach(function (role) {
                        render += Mustache.render(template, {
                            Id: role.Id,
                            Name: role.Name,
                            Description: role.Description
                        });
                    });
                    $('#tbl-content').html(render);
                }
            });
        }

        function LoadFunction() {
            $.ajax({
                type: 'GET',
                url: '/admin/function/GetAll',
                beforeSend: function () {
                    tedu.startLoading();
                },
                dataType: 'json',
                success: function (functions) {
                    var template = $('#result-data-function').html();
                    var render = '';
                    functions.forEach(function (f) {
                        render += Mustache.render(template, {
                            treegridparent: f.ParentId !== null ? "treegrid-parent-" + f.ParentId : "",
                            parrentId: f.ParentId !== null ? f.ParentId : "",
                            Name: f.Name,
                            Id: f.Id
                        });
                    });
                    if (render !== '') {
                        $('#lst-data-function').html(render);
                    }
                    var elementNeedRemove = $('label.parrent');
                    $.each(elementNeedRemove, function (i, v) {
                        v.remove();
                    });
                    $('.tree').treegrid();
                }
            });
        }
        //function LoadPermission() {
        //    $.ajax({
        //        type: 'GET',
        //        url: '/admin/role/permission',
        //        dataType:'json'
        //    });
        //}
    }
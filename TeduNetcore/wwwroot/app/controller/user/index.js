var allUser = []
var userController = function () {
    this.initialize = function () {
        RegisterEvent();
        LoadUser(false);
    }
   
    function RegisterEvent() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtFullName: { required: true },
                txtUserName: { required: true },
                txtPassword: {
                    required: true,
                    minlength: 6
                },
                txtConfirmPassword: {
                    equalTo: "#txtPassword"
                },
                txtEmail: {
                    required: true,
                    email: true
                }
            }
        });

        // PageSize change
        $('#pageSize').on('change', function () {
            tedu.pageSize = $(this).val();
            tedu.startPageIndex = 1;
            LoadUser(true);
        });

        var seletedUser = '';
        // Delete user
        $('body').on('click', '.btn-delete', function () {
            var id = $(this).data('id');
            for (var i = 0; i < allUser.length; i++) {
                if (allUser[i].Id === id) {
                    seletedUser = user.UserName;
                    break;
                }
            }

            tedu.confirm('Do you delete user: ' + seletedUser, function () {
                $.ajax({
                    type: 'POST',
                    data: {
                        id: id
                    },
                    url: '/admin/user/DeleteUser',
                    beforeSend: function () {
                        tedu; startLoading();
                    },
                    success: function (res) {
                        LoadUser(true);
                        tedu.notify('Delete user: ' + seletedUser + ' successfully', 'success');
                        tedu.stopLoading();
                    },
                    error: function (err) {
                        console.log(err);
                        tedu.notify('Delete user: ' + seletedUser + ' fail', 'error');
                        tedu.stopLoading();
                    }
                });
            });
        });

        // show form create user
        $('#btn-create').on('click', function (e) {
            // Reset from
            ResetFormMaintainance();

            // Init roles
            InitRoles();

            // Show from create
            $('#modal-add-edit').modal('show');
        });

        // Save user
        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidId').val();
                var fullName = $('#txtFullName').val();
                var userName = $('#txtUserName').val();
                var password = $('#txtPassword').val();
                var email = $('#txtEmail').val();
                var phoneNumber = $('#txtPhoneNumber').val();
                var roles = [];
                $.each($('input[name="ckRoles"]'), function (i, item) {
                    if ($(item).prop('checked') === true)
                        roles.push($(item).prop('value'));
                });
                var status = $('#ckStatus').prop('checked') === true ? 1 : 0;
                $.ajax({
                    type: 'POST',
                    url: '/admin/user/SaveChange',
                    data: {
                        Id: id,
                        FullName: fullName,
                        UserName: userName,
                        Password: password,
                        Email: email,
                        PhoneNumber: phoneNumber,
                        Status: status,
                        Roles: roles
                    },
                    dataType: 'json',
                    async: false,
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (res) {
                        tedu.notify('save user success', 'success');
                        $('#modal-add-edit').modal('hide');
                        ResetFormMaintainance();
                        LoadUser(true);
                        tedu.stopLoading();
                    },
                    error: function (error) {
                        console.log(error);
                        tedu.notify('Has an error', 'error');
                        tedu.stopLoading();
                    }
                });
            }
        });
    }

    function InitRoles() {
        $.ajax({
            type: 'GET',
            url: '/admin/role/GetAll',
            dataType: 'json',
            success: function (roles) {
                var template = $('#role-template').html();
                var render = '';
                roles.forEach(function (role) {
                    render += Mustache.render(template,
                        {
                            Name: role.Name,
                            Description: role.Description,
                            Checked: ''
                        });
                });
                $('#list-roles').html(render);
            }
        });
    }

    function LoadUser(isPageSizeChanged) {
        $.ajax({
            type: 'GET',
            url: '/admin/user/GetAllPage',
            data: {
                keyword: $('#txt-search-keyword').val(),
                indexCurrentPage: tedu.configs.startPageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: 'json',
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (users) {
                var render = RenderData(users.Result);
                allUser = users.Result;
                $("#lbl-total-records").text(users.TotalRow);
                if (render !== undefined) {
                    $('#tbl-content').html(render);
                }
                wrapPaging(user.TotalRow, function () {
                    LoadUser(false);
                }, isPageSizeChanged);
                tedu.stopLoading();
            },
            error: function (error) {
                console.log(error);
                tedu.stopLoading();
            }
        });
    }

    function RenderData(users) {
        var template = $('#table-template').html();
        var render = "";
        users.forEach(function (user) {
            render += Mustache.render(template, {
                Id: user.Id,
                UserName: user.UserName,
                FullName: user.FullName,
                Avatar: user.Avatar === null ? '<img src="/admin-side/images/user.png" width=25 />' : '<img src="' + user.Avatar + '" width=25 />',
                DateCreated: tedu.dateTimeFormatJson(user.DateCreated),
                Status: tedu.getStatus(user.Status)
            });
        });
        return render;
    }

    function wrapPaging(totalRow, callBack, changePageSize) {
        var totalsize = Math.ceil(totalRow / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                tedu.configs.startPageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }

    function ResetFormMaintainance() {
        $('#txtFullName').val('');
        $('#txtUserName').val('');
        $('#txtPassword').val('');
        $('#txtConfirmPassword').val('');
        $('#txtEmail').val('');
        $('#txtPhoneNumber').val('');
    }
}



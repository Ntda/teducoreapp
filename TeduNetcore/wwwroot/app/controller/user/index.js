var userController = function () {
    this.initialize = function () {
        RegisterEvent();
        LoadUser(false);
    }

    function RegisterEvent() {
        // PageSize change
        $('#pageSize').on('change', function () {
            tedu.pageSize = $(this).val();
            tedu.startPageIndex = 1;
            LoadUser(true);
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
        $('#btn-save').on('click', function (e) {
            var id = $('#hidId').val();
            var fullName = $('#txtFullName').val();
            var userName = $('#txtUserName').val();
            var password = $('#txtPassword').val();
            var email = $('#txtEmail').val();
            var phoneNumber = $('#txtPhoneNumber').val();
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



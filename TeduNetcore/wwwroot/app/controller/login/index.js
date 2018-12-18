var loginController = function () {
    this.initialize = function () {
        registerEvents();
    }
    function registerEvents() {
        //    $('frmLogin').valid({
        //        errorClass: 'red',
        //        ignore: [],
        //        lang: 'vi',
        //        rule: {
        //            userName: {
        //                required: true
        //            },
        //            passWord: {
        //                required: true
        //            }
        //        }
        //    });
        $('#btnLogin').on('click', function (e) {
            e.preventDefault();
            var username = $('#txtUserName').val();
            var password = $('#txtPassword').val();
            login(username, password);
        });
    }
    function login(user, pass) {
        $.ajax({
            type: 'POST',
            data: {
                UserName: user,
                Password: pass
            },
            dataType: 'json',
            url: '/Admin/login/Authen',
            success: function (res) {
                if (res.Success) {
                    window.location.href = "/Admin/Home/Index";
                }
                else {
                    tedu.notify('Đăng nhập không đúng', 'eror');
                }
            }
        });
    }
}

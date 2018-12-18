var tedu = {
    configs: {
        pageSize: 5,
        startPageIndex: 1
    },
    notify: function (message, type) {
        $.notify(message, {
            // whether to hide the notification on click
            clickToHide: true,
            // whether to auto-hide the notification
            autoHide: true,
            // if autoHide, hide after milliseconds
            autoHideDelay: 5000,
            // show the arrow pointing at the element
            arrowShow: true,
            // arrow size in pixels
            arrowSize: 5,
            // position defines the notification position though uses the defaults below
            position: '...',
            // default positions
            elementPosition: 'top left',
            globalPosition: 'top right',
            // default style
            style: 'bootstrap',
            // default class (string or [string])
            className: type,
            // show animation
            showAnimation: 'slideDown',
            // show animation duration
            showDuration: 400,
            // hide animation
            hideAnimation: 'slideUp',
            // hide animation duration
            hideDuration: 200,
            // padding between element and notification
            gap: 2
        });
    },
    confirm: function (message, Okcallback) {
        bootbox.confirm({
            message: message,
            buttons: {
                confirm: {
                    label: "Đồng ý",
                    className: "btn-success"
                },
                cancel: {
                    label: "Hủy",
                    className: "btn-danger"
                }
            },
            callback: function (result) {
                if (result === true) {
                    Okcallback();
                }
            }
        });
    },
    dateFormatJson: function (datetime) {
        if (datetime === null || datetime === '')
            return '';
        var obj = GetDateTime(datetime);
        var month = obj.newdate.getMonth() + 1;
        var day = obj.newdate.getDate();
        var year = obj.newdate.getFullYear();
        var hh = obj.newdate.getHours();
        var mm = obj.newdate.getMinutes();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        return day + "/" + month + "/" + year;
    },
    dateTimeFormatJson: function (datetime) {
        if (datetime === null || datetime === '')
            return '';
        var obj = GetDateTime(datetime);
        var hh = obj.newdate.getHours();
        var mm = obj.newdate.getMinutes();
        var ss = obj.newdate.getSeconds();
        if (obj.month < 10)
            month = "0" + obj.month;
        if (obj.day < 10)
            day = "0" + obj.day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        if (ss < 10)
            ss = "0" + ss;
        return day + "/" + month + "/" + obj.year + " " + hh + ":" + mm + ":" + ss;
    },
    startLoading: function () {
        if ($('.dv-loading').length > 0)
            $('.dv-loading').removeClass('hide');
    },
    stopLoading: function () {
        if ($('.dv-loading').length > 0)
            $('.dv-loading')
                .addClass('hide');
    },
    getStatus: function (status) {
        if (status === 1)
            return '<span class="badge bg-green">Kích hoạt</span>';
        else
            return '<span class="badge bg-red">Khoá</span>';
    },
    formatNumber: function (number, precision) {
        if (!isFinite(number)) {
            return number.toString();
        }

        var a = number.toFixed(precision).split('.');
        a[0] = a[0].replace(/\d(?=(\d{3})+$)/g, '$&,');
        return a.join('.');
    },
    unflattern: function (arr) {
        var map = {};
        var roots = [];
        for (var i = 0; i < arr.length; i += 1) {
            var node = arr[i];
            node.children = [];
            map[node.Id] = i; // use map to look-up the parents
            if (node.ParentId !== null) {
                arr[map[node.ParentId]].children.push(node);
            } else {
                roots.push(node);
            }
        }
        return roots;
    }
}

function GetDateTime(datetime) {
    var day = new Date(parseInt(datetime.substr(6)));
    var obj = {
        newdate: day,
        month: day.getMonth() + 1,
        day: day.getDate(),
        year: day.getFullYear()
    };
    return obj;
}

$(document).ajaxSend(function (e, xhr, options) {
    if (options.type.toUpperCase() === "POST" || options.type.toUpperCase() === "PUT") {
        var token = $('form').find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});
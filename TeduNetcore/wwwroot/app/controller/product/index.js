var productController = function () {
    this.initialize = function () {
        LoadData();
        RegisterEvent();
    }

    function RegisterEvent() {
        $('#ddlShowPage').on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.startPageIndex = 1;
            LoadData(true);
        });
    }

    function LoadData(isPageChanged) {
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                categoryId: null,
                keyWord: $('#txtSearchKeyword').val(),
                indexCurrentPage: tedu.configs.startPageIndex,
                pageSize: tedu.configs.pageSize
            },
            url: '/admin/product/GetAllPage',
            dataType: 'json',
            success: function (res) {
                render = RenderData(res.Result);
                $('#lblTotalRow').text(res.TotalRow);
                if (render !== '') {
                    $('#table-content').html(render);
                }
                wrapPaging(res.TotalRow, function () {
                    LoadData();
                }, isPageChanged);
            },
            error: function (status) {
                console.log(status);
                tedu.notify("fail to load data", 'error');
            }
        });
    }

    function RenderData(products) {
        var template = $('#table-template').html();
        var render = "";
        products.forEach(function (product) {
            render += Mustache.render(template, {
                Id: product.Id,
                ProductCategoryName: product.ProductCategory.Name,
                ProductName: product.Name,
                ProductPrice: tedu.formatNumber(product.Price, 0),
                ProductImage: product.Image === null ? '<image src="/admin-side/images/user.png" width=25' :
                    '<image src="' + product.Image + '" width=25',
                ProductCreatedDate: tedu.dateTimeFormatJson(product.DateCreated),
                ProductStatus: tedu.getStatus(product.Status)
            });
        });
        return render;
    }

    function wrapPaging(totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / tedu.configs.pageSize);

        // Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }

        //Bind pagination event
        $('#paginationUL').twbsPagination({
            totalPages: totalPage,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, indexCurrentPage) {
                tedu.configs.startPageIndex = indexCurrentPage;
                setTimeout(callback(), 200);
            }
        });
    }
}
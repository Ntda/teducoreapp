var productController = function () {
    this.initialize = function () {
        LoadData();

    }

    function RegisterEvent() {

    }

    function LoadData() {
        var render = "";
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAll',
            dataType: 'json',
            success: function (products) {
                render = RenderData(products);
                if (render !== '') {
                    $('#table-content').html(render);
                }
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
}
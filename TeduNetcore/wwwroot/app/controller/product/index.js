var productController = function () {
    this.initialize = function () {
        LoadData();
         
    },

    function RegisterEvent() {

    },

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
    },

    function RenderData(products) {
        var template = $('#table-template').html();
        var render = "";
        forEach(products, function (product) {
            render += Mustache.render(template, {
                Id: product.Id,
                ProductCategoryName: product.ProductCategory.Name,
                ProductName: product.Name,
                ProductPrice: product.Price,
                ProductImage: product.Image,
                ProductCreatedDate: product.DateCreated,
                ProductStatus: product.Status
            });
        });
        return render;
    }
}
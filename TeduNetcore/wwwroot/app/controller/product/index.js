var productController = function () {
    this.initialize = function () {
        LoadCategoryHome();
        LoadProduct();
        AddProduct();
        EditProduct();
        SaveChange();
        DeleteProduct();
        RegisterEvent();
        RegisterCkEditor();
    }
    function LoadCategoryHome() {
        LoadCategory('#categorySearch');
    }

    function LoadProduct(isPageSizeChanged) {
        var render = "";
        var productName = $('#txtSearchKeyword').val();
        var selectorCategory = $('select[name=selectorCategory]').val();
        $.ajax({
            type: 'GET',
            data: {
                categoryId: selectorCategory,
                keyWord: productName,
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
                    LoadProduct();
                }, isPageSizeChanged);
            },
            error: function (status) {
                console.log(status);
                tedu.notify("fail to load data", 'error');
            }
        });
    }

    function AddProduct() {
        $("#btnCreate").on('click', function () {
            resetFormMaintainance();
            LoadCategory('#categorySearchAdd');
            $('#modal-add-edit').modal('show');
        });
    }

    function EditProduct() {
        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'GET',
                url: '/admin/product/GetById',
                data: {
                    id: $(this).data('id')
                },
                beforeSend: function () {
                    tedu.startLoading();
                },
                dataType: 'json',
                success: function (product) {
                    $('#hidIdM').val(product.Id);
                    $('#txtNameM').val(product.Name);
                    LoadCategory('#categorySearchAdd', product.CategoryId);

                    $('#txtDescM').val(product.Description);
                    $('#txtUnitM').val(product.Unit);

                    $('#txtPriceM').val(product.Price);
                    $('#txtOriginalPriceM').val(product.OriginalPrice);
                    $('#txtPromotionPriceM').val(product.PromotionPrice);

                    // $('#txtImageM').val(data.ThumbnailImage);

                    $('#txtTagM').val(product.Tags);
                    $('#txtMetakeywordM').val(product.SeoKeywords);
                    $('#txtMetaDescriptionM').val(product.SeoDescription);
                    $('#txtSeoPageTitleM').val(product.SeoPageTitle);
                    $('#txtSeoAliasM').val(product.SeoAlias);

                    CKEDITOR.instances.txtContentM.setData(product.Content);
                    $('#ckStatusM').prop('checked', product.Status === 1);
                    $('#ckHotM').prop('checked', product.HotFlag);
                    $('#ckShowHomeM').prop('checked', product.HomeFlag);

                    $('#modal-add-edit').modal('show');
                    tedu.stopLoading();
                },
                error: function (err) {
                    tedu.stopLoading();
                }
            });
        });
    }

    function SaveChange() {
        $('#btnSave').on('click', function () {
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var categoryId = $('select[name=categoryName]').val();

            var description = $('#txtDescM').val();
            var unit = $('#txtUnitM').val();

            var price = $('#txtPriceM').val();
            var originalPrice = $('#txtOriginalPriceM').val();
            var promotionPrice = $('#txtPromotionPriceM').val();

            //var image = $('#txtImageM').val();

            var tags = $('#txtTagM').val();
            var seoKeyword = $('#txtMetakeywordM').val();
            var seoMetaDescription = $('#txtMetaDescriptionM').val();
            var seoPageTitle = $('#txtSeoPageTitleM').val();
            var seoAlias = $('#txtSeoAliasM').val();

            var content = CKEDITOR.instances.txtContentM.getData();
            var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;
            var hot = $('#ckHotM').prop('checked');
            var showHome = $('#ckShowHomeM').prop('checked');
            $.ajax({
                type: 'POST',
                url: '/admin/product/SaveEntity',
                dataType: 'json',
                data: {
                    Id: id,
                    Name: name,
                    CategoryId: categoryId,
                    Image: '',
                    Price: price,
                    OriginalPrice: originalPrice,
                    PromotionPrice: promotionPrice,
                    Description: description,
                    Content: content,
                    HomeFlag: showHome,
                    HotFlag: hot,
                    Tags: tags,
                    Unit: unit,
                    Status: status,
                    SeoPageTitle: seoPageTitle,
                    SeoAlias: seoAlias,
                    SeoKeywords: seoKeyword,
                    SeoDescription: seoMetaDescription
                },
                success: function (res) {
                    tedu.notify("Update product successfully", 'success');
                    $('#modal-add-edit').modal('hide');
                    LoadProduct(true);
                },
                error: function (error) {
                    Console.log(error);
                    tedu.notify("Fail to update product", 'error');
                }
            });
        });
    }

    function DeleteProduct() {
        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var Id = $(this).data('id');
            tedu.confirm('Are you sure delete?', function () {
                $.ajax({
                    type: 'POST',
                    url: '/admin/product/Delete',
                    data: {
                        id: Id
                    },
                    dataType: 'Json',
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (result) {
                        tedu.notify('Delete product successfully', 'success');
                        tedu.stopLoading();
                        LoadProduct(true);
                    },
                    error: function (err) {
                        Console.log(err);
                        tedu.notify('Faild to delete product', 'error');
                        tedu.stopLoading();
                    }
                });
            });
        });
    }

    function RegisterEvent() {
        $('#pageSize').on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.startPageIndex = 1;
            LoadProduct(true);
        });

        $('#btnSearch').on('click', function () {
            LoadProduct(true);
        });

        $('#categorySearch').on('keypress', function (e) {
            if (e.which === 13) {
                LoadProduct();
            }
        });
    }

    function RegisterCkEditor() {
        CKEDITOR.replace('txtContent', {});

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };

    }

    function LoadCategory(category, categoryId) {
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAllCategories',
            dataType: 'json',
            success: function (categories) {
                var render = "<option value=''>--Select category--</option>";
                categories.forEach(function (category) {
                    render += "<option value='" + category.Id + "'>" + category.Name + "</option>";
                });
                $(category).html(render);
                $('#categorySearchAdd').val(categoryId);
            },
            error: function (status) {
                console.log(status);
            }
        });
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

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        LoadCategory();

        $('#txtDescM').val('');
        $('#txtUnitM').val('');

        $('#txtPriceM').val('0');
        $('#txtOriginalPriceM').val('');
        $('#txtPromotionPriceM').val('');

        //$('#txtImageM').val('');

        $('#txtTagM').val('');
        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        //CKEDITOR.instances.txtContentM.setData('');
        $('#ckStatusM').prop('checked', true);
        $('#ckHotM').prop('checked', false);
        $('#ckShowHomeM').prop('checked', false);
    }
}
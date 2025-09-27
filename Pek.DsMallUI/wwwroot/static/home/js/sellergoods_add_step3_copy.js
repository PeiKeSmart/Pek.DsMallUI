//import { debuglog } from "util";

$(function(){
    // 商品图片ajax上传
    $('.dssc-upload-btn').find('input[type="file"]').on('change', function(){
        var id = $(this).attr('id');
        var file = this.files[0];
         // 可以在这里添加文件验证
        if(file) {
            // 验证文件类型
            if(!/^image\/(jpeg|png|gif)$/.test(file.type)) {
                alert('请选择正确的图片文件格式（JPG、PNG、GIF）');
                return;
            }
            // 验证文件大小（这里设置最大1MB）
            if(file.size > 1024 * 1024) {
                alert('图片大小不能超过1MB');
                return;
            }
        }
        //var id = $(this).attr('data');
        ajaxFileUpload(id,file);
    });
    //浮动导航  waypoints.js
//    $("#uploadHelp").waypoint(function(event, direction) {
//        $(this).parent().toggleClass('sticky', direction === "down");
//        event.stopPropagation();
//    }); 
    // 关闭相册
    $('a[dstype="close_album"]').click(function(){
        $(this).hide();
        $(this).prev().show();
        $(this).parent().next().html('');
    });
    // 绑定点击事件
    $('div[dstype^="file"]').each(function(){
        if ($(this).find('input[type="hidden"]').val() != '') {
            selectDefaultImage($(this));
        }
    });
});

// 图片上传ajax
function ajaxFileUpload(id, file) {
    $('img[dstype="' + id + '"]').attr('src', HOMESITEROOT + "/images/loading.gif");
   // 创建 FormData 对象
   var formData = new FormData();
   formData.append('aPic', file);
    $.ajax({
        url: '/Sellers/SellerGoodsAdd/UploadPicture',
        type: 'POST',
        data: formData,
        processData: false,  // 不处理数据
        contentType: false,  // 不设置内容类型
        dataType: 'json',
        success: function (result) {
            if (!result.success) {
                alert(result.error);
                        $('img[dstype="' + id + '"]').attr('src',DEFAULT_GOODS_IMAGE);
                    } else {
                        //$('input[dstype="' + id + '"]').val(data.name);
                        $('input[dstype="' + id + '"]').val(result.data.Name);
                        $('input[dsId="' + id + '"]').val(result.data.Id);
                        $('input[typeId="' + id + '"]').val("1");   
                        $('img[dstype="' + id + '"]').attr('src', result.msg);
                        selectDefaultImage($('div[dstype="' + id + '"]'));   // 选择默认主图
                    }

                },
        error : function (data, status, e) {
                    alert(e);

                }
    });
    return false;

}

// 选择默认主图&&删除
function selectDefaultImage($this) {
    // 默认主题
    $this.click(function(){
        $(this).parents('ul:first').find('.show-default').removeClass('selected').find('input').val('0');
        $(this).addClass('selected').find('input').val('1');
    });
    // 删除
    $this.parents('li:first').find('a[dstype="del"]').click(function(){
        $this.unbind('click').removeClass('selected').find('input').val('0');
        $this.prev().find('input').val('').end().find('img').attr('src', DEFAULT_GOODS_IMAGE);
    });
}

// 从图片空间插入主图
function insert_img(name, src, color_id) {
    console.log("插入图片"+name+","+ src+","+color_id);
    var $_thumb = $('ul[dstype="ul'+ color_id +'"]').find('.upload-thumb');
    $_thumb.each(function(){
        if ($(this).find('input').val() == '') {
            $(this).find('img').attr('src', src);
            $(this).find('input').val(name);
     
            selectDefaultImage($(this).next());      // 选择默认主图
            return false;
        }
    });
}

function insert_imgs(id,name, src, color_id) {
    var $_thumb = $('ul[dstype="ul' + color_id + '"]').find('.upload-thumb');
    $_thumb.each(function () {
        if ($(this).find('input:eq(0)').val() == '') {
            $(this).find('img').attr('src', src);
            $(this).find('input:eq(0)').val(name);
            $(this).find('input:eq(1)').val(id);
            $(this).find('input:eq(2)').val(name);

            selectDefaultImage($(this).next());      // 选择默认主图
            return false;
        }
    });
}
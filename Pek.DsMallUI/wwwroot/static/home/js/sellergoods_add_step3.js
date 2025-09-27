//import { debuglog } from "util";

$(function(){
    // 商品图片ajax上传
    $('.dssc-upload-btn').find('input[type="file"]').on('change', function(){
        var id = $(this).attr('id');
        var file = this.files[0];
        var $uploadBtn = $(this);

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

        // 传递上传按钮的上下文，确保更新正确的元素
        ajaxFileUpload(id, file, $uploadBtn);
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
function ajaxFileUpload(id, file, $uploadBtn) {
    console.log('ajaxFileUpload 被调用，id:', id);

    // 通过上传按钮的上下文来精确定位目标元素
    var $container = $uploadBtn.closest('.dssc-goodspic-upload');
    var $targetImg = $container.find('img[dstype="' + id + '"]');
    var $targetInput = $container.find('input[dstype="' + id + '"]');
    var $targetIdInput = $container.find('input[dsId="' + id + '"]');
    var $targetTypeInput = $container.find('input[typeId="' + id + '"]');
    var $targetDiv = $container.find('div[dstype="' + id + '"]');

    if ($targetImg.length === 0) {
        console.log('没有找到目标图片元素:', id);
        return;
    }

    $targetImg.attr('src', HOMESITEROOT + "/images/loading.gif");

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
                layer.msg(result.msg, { icon: 2 });
                $targetImg.attr('src', DEFAULT_GOODS_IMAGE);
            } else {
                console.log('上传成功，更新元素:', id, result.data);
                $targetInput.val(result.data.Name);
                $targetIdInput.val(result.data.Id);
                $targetTypeInput.val("1");
                $targetImg.attr('src', result.msg);
                selectDefaultImage($targetDiv);      // 选择默认主图
            }
        },
        error : function (data, status, e) {
            console.log('上传失败:', e);
            alert(e);
            if ($targetImg.length > 0) {
                $targetImg.attr('src', DEFAULT_GOODS_IMAGE);
            }
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
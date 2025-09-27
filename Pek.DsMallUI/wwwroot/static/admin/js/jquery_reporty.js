$(document).ready(function () {
    //列表下拉
    $('img[ds_type="flex"]').click(function () {
        var status = $(this).attr('status');
        if (status == 'open') {
            var pr = $(this).parent('td').parent('tr');
            var id = $(this).attr('fieldid');
            var obj = $(this);
            var xavier_type = $(this).attr('xavier_type');
            $(this).attr('status', 'none');
            //ajax
            $.ajax({
                //url: ADMINSITEURL + '/Goodsclass/goods_class.html?ajax=1&gc_parent_id=' + id,
                //url: ADMINSITEURL + '/ArticleClass/GetSubordinateData?Id=' + id,
                url: getData + '?Id=' + id,
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                    var src = '';
                    for (var i = 0; i < data.zList.length; i++) {
                        var tmp_vertline = "<img class='preimg' src='" + ADMINSITEROOT + "/images/treetable/vertline.gif'/>";
                        src += "<tr id='ds_row_" + data.zList[i].gc_id + "' class='" + pr.attr('class') + " row" + id + "'>";
                        src += "<td class='w36'><input type='checkbox' name='check_gc_id[]' value='" + data.zList[i].gc_id + "' class='checkitem'>";
                        //图片
                        if (data.zList[i].have_child == 1) {
                            src += " <img fieldid='" + data.zList[i].gc_id + "' status='open' ds_type='flex' src='" + ADMINSITEROOT + "/images/treetable/tv-expandable.gif' />";
                        } else {
                            src += " <img fieldid='" + data.zList[i].gc_id + "' status='none' ds_type='flex' src='" + ADMINSITEROOT + "/images/treetable/tv-item.gif' />";
                        }
                        src += "</td><td class='w48 sort'>";
                        //排序
                        src += " <span title='可编辑下级分类排序' ajax_branch='goods_class_sort' datatype='number' fieldid='" + data.zList[i].gc_id + "' fieldname='gc_sort'    ds_type='inline_edit' class='editable tooltip'>" + data.zList[i].gc_sort + "</span></td>";
                        //名称
                        src += "<td class='w50pre name'>";


                        for (var tmp_i = 0; tmp_i < data.zList[i].deep; tmp_i++) {
                            src += tmp_vertline;
                        }
                        if (data.zList[i].have_child == 1) {
                            src += " <img fieldid='" + data.zList[i].gc_id + "' status='open' ds_type='flex' src='" + ADMINSITEROOT + "/images/treetable/tv-item1.gif' />";
                        } else {
                            src += " <img fieldid='" + data.zList[i].gc_id + "' status='none' ds_type='flex' src='" + ADMINSITEROOT + "/images/treetable/tv-expandable1.gif' />";
                        }
                        src += " <span title='可编辑下级分类名称' required='1' fieldid='" + data.zList[i].gc_id + "' ajax_branch='goods_class_name' fieldname='gc_name'   ds_type='inline_edit' class='editable tooltip'>" + data.zList[i].gc_name + "</span>";
                        //新增下级
                        if (data.zList[i].deep < 2) {
                            src += "<a class='btn-add-nofloat marginleft' href='" + createData + "?parent_id=" + data.zList[i].gc_id + "'><span>新增下级</span></a>";
                        }
                        src += "</td>";
                        //是否显示
                        /*src += "<td class='power-onoff'>";
                         if(data[i].gc_show == 0){
                         src += "<a href='JavaScript:void(0);' class='tooltip disabled' fieldvalue='0' fieldid='"+data[i].gc_id+"' ajax_branch='goods_class_show' fieldname='gc_show' ds_type='inline_edit'><img src='"+ADMIN_TEMPLATES_URL+"/images/transparent.gif'></a>";
                         }else{
                         src += "<a href='JavaScript:void(0);' class='tooltip enabled' fieldvalue='1' fieldid='"+data[i].gc_id+"' ajax_branch='goods_class_show' fieldname='gc_show' ds_type='inline_edit'><img src='"+ADMIN_TEMPLATES_URL+"/images/transparent.gif'></a>";
                         }
                         src += "</td>";
                         */ 
                        // 商品分类
                        if (xavier_type == 'ProductCategory') {
                            src += "<td>" + "</td>";
                            src += "<td>" + data.zList[i].type_name + "</td>";
                            //分佣
                            src += "<td>" + data.zList[i].commis_rate + " %</td>";
                        }
                       
                        //虚拟
                        if (data.zList[i].gc_virtual == 1) {
                           src += "<td>虚拟</td>";
                        } else {
                        src += "<td></td>";
                        }
                        //操作
                        src += "<td class='w200'>";
                        //src += "<a href='" + ADMINSITEURL + "/ArticleClass/EditArticleClass?Id=" + data.zList[i].gc_id + "'>编辑</a>";
                        src += data.zList[i].gc_enable == 0
                            ? `<a href="javascript:void(0)" onclick="confirmToggleStatus('${setEnable}', ${data.zList[i].gc_id}n, 0)" class="red">禁用</a>`
                            : `<a href="javascript:void(0)" onclick="confirmToggleStatus('${setEnable}', ${data.zList[i].gc_id}n, 1)" class="skyblue">启用</a>`;
                        //src += "<a class='skyblue' href='" + setEnable + "?classId=" + data.zList[i].gc_id + "'>启用</a>";
                        src += " | <a class='skyblue' href='" + settingData + "?ClassId=" + data.zList[i].gc_id + "'>设置</a>";
                        src += " | <a class='skyblue' href='" + editData + "?Id=" + data.zList[i].gc_id + "'>编辑</a>";
                        src += " | <a class='skyblue' href=\"javascript:submit_delete('" + data.zList[i].gc_id + "')\">删除</a>";
                        //src += " | <a href=\"javascript:dsLayerConfirm('" + ADMINSITEURL + "/ArticleClass/Delete?Ids=" + data.zList[i].gc_id + "','您确定要删除吗'," + data.zList[i].gc_id + ");\">删除</a>";
                        src += "</td>";
                        src += "</tr>";
                    }
                    //插入
                    pr.after(src);
                    obj.attr('status', 'close');
                    obj.attr('src', obj.attr('src').replace("tv-expandable", "tv-collapsable"));
                    $('img[ds_type="flex"]').unbind('click');
                    $('span[ds_type="inline_edit"]').unbind('click');
                    //重现初始化页面
                    $.getScript(ADMINSITEROOT + "/js/jquery.edit.js");
                    $.getScript(ADMINSITEROOT + "/js/jquery_reporty.js");
                },
                error: function () {
                    alert('获取信息失败');
                }
            });
        }
        if (status == 'close') {
            $(".row" + $(this).attr('fieldid')).remove();
            $(this).attr('src', $(this).attr('src').replace("tv-collapsable", "tv-expandable"));
            $(this).attr('status', 'open');
        }
    })
});
// 添加确认操作的函数
function confirmToggleStatus(url, classId, status) {
    var msg = status === 1 ? '确定要禁用该分类吗？' : '确定要启用该分类吗？';
    layer.confirm(msg, {
        btn: ['确定', '取消'],
        title: '提示'
    }, function () {
        var loading = layer.load(1, { shade: [0.1, '#fff'] });
        $.post(url, {
            classId: classId
        }, function(res) {
            layer.close(loading);
            if (res.success) {
                layer.msg(status === 1 ? '禁用成功' : '启用成功', { icon: 1 });
                setTimeout(function() {
                    window.location.reload();
                }, 800);
            } else {
                layer.msg(res.msg || '操作失败', { icon: 2 });
            }
        });
    });
}
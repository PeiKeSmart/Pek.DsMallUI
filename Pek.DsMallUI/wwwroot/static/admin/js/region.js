$(function()
{
    //给图标的加减号添加展开收缩行为
    $('img[ds_type="flex"]').click(function(){
        var status = $(this).attr("status");
        //状态是加号的事件
        if(status == 'open')
        {
            var pr = $(this).parent('td').parent('tr');
            var id = $(this).attr("fieldid");
            var type = $(this).attr("type");//0是国家 1是省市区
            var obj = $(this);
            $(this).attr('status', 'none');
            //$.get(ADMINSITEURL + '/Regions/GetSubordinateData', { id: id, type}, function(data){
            $.get(getDate, { id: id, type}, function(data){
                if(data)
                {
                    var str = "";
                    //var res = eval('(' + data+')');
                    var res = data;
                    for(var i = 0; i < res.length; i++)
                    {
                        var tmp_vertline = "<img class='preimg' src='"+ADMINSITEROOT+"/images/treetable/vertline.gif'/>";
                        
                        str += "<tr id='ds_row_" + res[i].AreaCode + "' class='" + pr.attr('class') + " row" + id + "'>";
                        str += "<input type=\"hidden\" class=\"en\" name=\"en\" value=\"1\">";
                        str += "<td><input type='checkbox' name='check_gc_id[]' value='" + res[i].gc_id+"' class='checkitem'>";
                        //给每一个异步取出的数据添加伸缩图标后者无状态图标
                        if (res[i].have_child == 1)
                        {
                            str += "<img src='" + ADMINSITEROOT + "/images/treetable/tv-expandable.gif' ds_type='flex' status='open' fieldid='" + res[i].AreaCode + "' type='" + res[i].type + "'>";
                        }
                        else
                        {
                            str += "<img src='" + ADMINSITEROOT + "/images/treetable/tv-item.gif' ds_type='flex' status='none' fieldid='" + res[i].AreaCode + "' type='" + res[i].type + "'>";
                        }
                        str += "</td><td class='sort'>";
                        //排序
                        // .AreaCode<td class="sort">
                            
                            
                        str += " <span title='可编辑下级分类排序' ajax_branch='gc_sort' datatype='number' fieldid='" + res[i].gc_id  + "' fieldname='gc_sort' ds_type='inline_edit' class='editable tooltip'>" + res[i].Sort + "</span></td>";
			            // 行政代码
                        str += `<td class="sort"><span fieldname="AreaCode" class="editable ">${res[i].AreaCode}</span></td>`;
                        //名称
                        str += "<td class='name'>";
                        for (var tmp_i = 1; tmp_i <= (res[i].deep); tmp_i++) {
                            console.log("添加一个空" );
                            str += tmp_vertline;
                        }
                        if (i == res.length-1) {
                            str += "   <img fieldid='" + res[i].gc_id + "' status='open' src='" + ADMINSITEROOT + "/images/treetable/tv-item1.gif' />";
                        } else {
                            str += "   <img fieldid='" + res[i].gc_id + "' status='none'  src='" + ADMINSITEROOT + "/images/treetable/tv-expandable1.gif' />";
                        }
                        str += " <span title='可编辑下级分类名称' required='1' typeof='1' fieldid='" + res[i].gc_id +"' ajax_branch='area_name' fieldname='area_name' ds_type='inline_edit' class='editable tooltip'>"+res[i].gc_name+"</span>";
                        ADMINSITEURL
			str += "</td>";
                                              
                        //大区名称
                        str += "<td class='name'>";
                        for (var tmp_i = 1; tmp_i < (res[i].deep - 1); tmp_i++) {
                            str += tmp_vertline;
                        }
                        if(res[i].gc_region == null){
                            res[i].gc_region= ' ';
                        }
                        if (res[i].deep == 0) {
                            str += " <span title='可编辑下级分类名称' required='1' fieldid='" + res[i].gc_id + "' ajax_branch='area_region' typeof='1' fieldname='area_region' ds_type='inline_edit' class='editable tooltip'>" + res[i].CreateUser + "</span>";
                        }
                        str += "</td>";
                        str += "<td><a href=\"" + UpdateUrl + "?Id=" + res[i].gc_id + "\"')\" class='dsui-btn-edit'><i class='iconfont'></i>编辑</a>";
                        str += "<a href=\"javascript:dsLayerConfirm('" + DelUrl + "?Ids=" + res[i].gc_id + "','您确定要删除吗？该数据的子集会被一起删除！'," + res[i].AreaCode + ");\" class='dsui-btn-del'><i class='iconfont'></i>删除</a>";
                        if (res[i].deep<2){
                            str += "<a href=\"" + AddUrl + "?ParentId=" + res[i].gc_id + "\" class='dsui-btn-add'><i class='iconfont'></i>新增下级</a>";
                        }
                        str += "</td></tr>"
                    }
                    
                    //将组成的字符串添加到点击对象后面
                    pr.after(str);
                    obj.attr('status', 'close');
                    obj.attr('src', obj.attr('src').replace("tv-expandable", "tv-collapsable"));
                    $('img[ds_type="flex"]').unbind('click');
                    $('span[ds_type="inline_edit"]').unbind('click');
                    //重现初始化页面
                    $.getScript(ADMINSITEROOT + "/js/jquery.edit.js");
                    $.getScript(ADMINSITEROOT + "/js/region.js");
                }
            });
        }
        //状态是减号的事件
        if(status == "close")
        {
            $(".row"+$(this).attr('fieldid')).remove();
            $(this).attr('src',$(this).attr('src').replace("tv-collapsable","tv-expandable"));
	    $(this).attr('status','open');
        }
    });
});
/* name 請必須使用此設定*/
//$('#upload').on('change', function (e) {
//$("#upload").submit(function (event) {
//    //取得檔案
//    readURL(this);
//    //event.preventDefault();
//});

//function readURL(input) {
//    if (input.files && input.files[0]) {
//        /*原先預覽功能，修改為直接Reload Div*/
//        //var reader = new FileReader();
//        //reader.onload = function (e) {
//        //    $('#Preview').attr('src', e.target.result);
//        //}
//        //reader.readAsDataURL(input.files[0]);
//    }
//}

function AddTr(setID,tmpNowTr) {
    alert(setID);
    //var tmpNowTr = $('#SettingNowTr');
    //var setID = 0;
    if (tmpNowTr.val() != '') {
        //if (parseInt(tmpNowTr) % 4 == 0) {
            setID = parseInt(tmpNowTr.val()) + 1;
            $('#SettingNowTr').val(setID);
        //}
    }
    $('#PreviewTable tr:last').after('<tr id=' + setID + '><td>Hi ' + setID + '</td></tr>');
}
function AddLast(link, images,guid) {
    alert($('#PreviewTable tr:last').get(0).id);
    var LastTr = $('#PreviewTable tr:last').get(0).id;

    // 1.使用jquery
    var CreateAppend = "<td class='container'>";
    CreateAppend += "<a class='example-image-link' href='";
    CreateAppend += link + "' data-lightbox='example-set'>";
    CreateAppend += "<img class='example-image' src='";
    CreateAppend += images + "' alt='' />";
    CreateAppend += "</a><div class='middle'><div class='text' id='pic_" + guid +"'>刪除</div></div></td>";

    $('#' + LastTr).append(CreateAppend);
    //<td class="container">
    //    <a class="example-image-link" href="http://lokeshdhakar.com/projects/lightbox2/images/image-3.jpg" data-lightbox="example-set" data-title="Click the right half of the image to move forward.">
    //        <img class="example-image" src="http://lokeshdhakar.com/projects/lightbox2/images/thumb-3.jpg" alt="" />
    //    </a>
    //    <div class="middle">
    //        <div class="text" id="pic_" + @item.PicID>刪除</div>
    //                    </div>
    //                </td >

    // 2.使用javascript
    //var x = document.createElement("TD");
    //var t = document.createTextNode("new cell");
    //x.appendChild(t);
    //document.getElementById(LastTr).appendChild(x);
}


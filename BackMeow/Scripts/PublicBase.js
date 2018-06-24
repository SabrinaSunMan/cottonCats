var pattern = new RegExp("[`~!#$^&*()=|{}':;',\\[\\]<>/?~！@@#￥……&*（）——|{}【】‘；：”“'。，、？%+_]");

//特殊符號取代
function filterStr(str)
{
    var specialStr = "";
    for (var i = 0; i < str.length; i++) {
        specialStr += str.substr(i, 1).replace(pattern, '');
    }
    return specialStr;
}

//跳出提醒視窗,檔案名稱不得包含特殊符號
function checkFileName(str)
{
    if (str.match(pattern)) {
        showDialog("檔名不得包含特殊符號", false, null);
        return false;
    } else {
        return true;
    } 
}


$(document).ready(function () {
    pg.initTree();

    $(document).on("click", "a.jstree-anchor", pg.showNodeInfo);
    $(document).on("click", "a#btnClose", pg.closeModal);
    $(document).on("submit", "form#data", pg.uploadFile);
   
});

var pg = {
    linkGetCatalogueTree: '/Home/GetCatalogueTree',
    linkGetNodeInfo: '/Home/GetNodeInfo',
    linkDownloadFile: '/File/DownloadFile',
    linkUploadFile: '/Folder/UploadToFolder',

    initTree : function() {
        $('#filer-demo').jsfiler({
            /* 1 - right-click menu, 2 - icon menu, 3 - both */
            menuMode: 3,
            mainMenu: '.menu',
            /* path to tree and menu icons */
            iconPath: '',
            /* no tree checkboxes */
            checkbox: false,
            /* allow drag & drop */
            canDrag: false,
            /* allow multiple roots */
            rootSingle: false,
            /* allow leafs for root node */
            rootLeaf: true,
            /* root parent id */
            rootParent: -1,
            /* save opened/selected state */
            saveState: false,
            /* open the node on: 1 - click, 2 - dblclick, 3 - both 04.2017 */
            selectOpen: 3,
            /* knots deletion: 0 - empty only, 1 - +copied, 2 - all */
            knotRemove: 0,
            /* duplicate child names: 2 - allow, 1 - case-sensitive, 0 - no */
            nameDupl: 0,
            /* name trim patterm (leading & trailing spaces */
            nameTrim: /^\s+|\s+$/g,
            nameValidate: false,
            /* user authorization token */
            userAuth: null,
            /* ajax request url */
            urlAjax: pg.linkGetCatalogueTree
        });
    },
    
    showNodeInfo : function(e) {
        var inode = $(this);
        // nearest li
        var nodeId = inode.closest("li").attr('id');
        $.ajax({
            type: "POST",
            url: pg.linkGetNodeInfo,
            data: '{id: "' + nodeId + '" }',
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var size = data.size === "0 B" ? "not available" : data.size;
                var info = "<b>Type: </b>" + data.filetype + "<br/>" +
                    "<b>Name: </b>" + data.name + "<br/>" +
                    "<b>Size: </b>" + size + "";

                $('.info').html(info);
            },
            error: function() {
                $('.info').html("Error occurred");
            }
        });
    },

    closeModal : function() {
        var $modalCloseBtn = $('a#btnClose');
        $modalCloseBtn.attr('href', '#close');
        $modalCloseBtn[0].click();
        $modalCloseBtn.attr('href', '');
    },

    uploadFile: function (e) {
        var formData = new FormData(this);

        $.ajax({
            type: "POST",
            url: pg.linkUploadFile,
            data: formData,
            success: function (data) {
                pg.closeModal();
                var text = "File '" + data.file + "' uploaded to " +
                    "the folder '" + data.folder + "'";
                alert(text);
            },
            error: function () {
                alert("Error occurred on file upload");
            },
            async: false,
            cache: false,
            contentType: false,
            processData: false
        });

        return false;
    }
}
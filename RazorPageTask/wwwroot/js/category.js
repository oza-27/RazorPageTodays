$(document).ready(function () {
    $("#searchtxt").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tableData tbody tr").filter(function () {
            $(this).toggle(($(this.children[0]).text().toLowerCase().indexOf(value) > -1) || ($(this.children[2]).text().toLowerCase().indexOf(value) > -1))
        });
    });
});
$(document).ready(function () {
    $("#searchBox").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tblData tbody tr").filter(function () {
            $(this).toggle(($(this.children[0]).text().toLowerCase().indexOf(value) > -1) || ($(this.children[3]).text().toLowerCase().indexOf(value) > -1))
        });
    });
});

function ValidateInput() {
    if (document.getElementById("uploadImage").value == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please upload an image file!',
        });
        return false;
    }
    return true;
}
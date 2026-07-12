$(document).ready(function () {
    load();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        load();
    }
});
function load() {
    //$('#example1 tfoot th,#example2 tfoot th,#example3 tfoot th,#example4 tfoot th').each(function () {
    //    var title = $(this).text();
    //    $(this).html('<input type="text" class="form-control" placeholder="Search" />');
    //});
    $("#btnSave,#btnEdit,#btnSchoolInfo").on('click', function () {
        $(".message").slideDown('slow');
        $(".message").delay(5000).fadeOut(2000);
    });

    var table = $('#example1,#example2,#example3,#example4').DataTable().destroy();
    
    table = $('#example1,#example2,#example3,#example4').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true,
        "order": [[0, "desc"]],
        "lengthMenu": [[ -1,20, 40, 100], ["All",20, 40, 100]]
    });

    //table.columns().every(function () {
    //    var that = this;

    //    $('input', this.footer()).on('keyup change', function () {
    //        if (that.search() !== this.value) {
    //            that
    //                .search(this.value)
    //                .draw();
    //        }
    //    });
    //});
    $(".select2").select2();
}

function getCookieValue(name) {
    cookieList = document.cookie.split('; ');
    cookies = {};
    for (i = cookieList.length - 1; i >= 0; i--) {
        cookie = cookieList[i].split('=');
        cookies[cookie[0]] = cookie[1];
    }
    return cookies[name];
}

$('li a').each(function () {
    if ($($(this))[0].href == String(window.location)) {
        $(this).parent().addClass('active');
    }
});
$('li ul li a').each(function () {
    if ($($(this))[0].href == String(window.location)) {
        $(this).parent().addClass('active');
        $(this).parent().parent().show();
    }
});


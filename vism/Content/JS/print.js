$(document).ready(function () {
    loads();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        loads();
    }
});
function loads() {
    $("#info").hide();
    $("#btnPrintBill").click(function () {
        $("#info").show();
        var contents = $(".mainbody").html();
        var frame1 = $('<iframe />');
        frame1[0].name = "frame1";
        $("body").append(frame1);
        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
        frameDoc.document.open();
        frameDoc.document.write('<html><head><link href="../../Styles/bootstrap.min.css" rel="stylesheet" type="text/css" /><title></title>');
        frameDoc.document.write('</head><body><div class="container"><div class="row"><div class="col-sm-12">');
        frameDoc.document.write(contents);
        frameDoc.document.write('</div></div></div></body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            frame1.remove();
        }, 500);
    });
    
}

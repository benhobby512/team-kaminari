$(function() {
    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: true,
        autoclose: true
    });

    $(document).ready(function () {
        $("#tableSorter").tablesorter();
    });
});

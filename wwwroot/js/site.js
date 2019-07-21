// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function UpdateRolesOnScheduleChange(url) {
   $(".scheduleList").change(function () {
      var scheduleId = $(".scheduleList").val();
      $(".shiftRoleList").prop("disabled", !scheduleId);

      if (scheduleId) {
         $(".shiftRoleList").html("<option>Loading...</option>")

         $.getJSON(url, { ScheduleId: scheduleId }, function (data) {
            var items = "<option value=\"\">None</option>";
            $.each(data, function (i, shiftRole) {
               items += "<option value=\"" + shiftRole.value + "\">" + shiftRole.text + "</option>"
            });
            $(".shiftRoleList").html(items);
         }).fail(function () {
            $(".shiftRoleList").html("<option>Error</option>")
         });
      }
      else {
         $(".shiftRoleList").empty();
      }
   });
}

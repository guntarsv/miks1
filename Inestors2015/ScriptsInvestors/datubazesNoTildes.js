



// 0. HttpPOST
// 1. load Partners for one DB (AJAX)
// 2. load dropdown => _PartnerDropDown.cshtml
// 3. write selected Partner to hidden iput => id,name = SelectedPartnerName
// 4. add selectedDB onchange event => update hidden input value
var loadPartnerNames = function () {
    var databaseElement = $("#SelectListDatabases_SelectedValue")[0];
    var selectedDatabaseIndex = databaseElement.selectedIndex;
    var selectedDatabaseName = databaseElement.options[selectedDatabaseIndex].text;
    var serializedFormAndOtherData = $("#mikaForma").serialize() + '&' + $.param({ 'SelectedDatabaseName': selectedDatabaseName });

    $("#partnerDropDownPlaceholder").html("");

    $.ajax({
        url: "/Investors2015/DataEntry/GetDatabasePartnersHtml",
        type: "POST",
        data: serializedFormAndOtherData

    })
        .success(function (result) {
            // 2.

            $("#partnerDropDownPlaceholder").html(result);
        })
        .error(function (xhr, status) {
            alert("kļūda JS funkcijā loadPartnerNames mxxx " + status + "\n" + xhr.responseText);
        });
};



// 1. load DB list (AJAX)
// 2. load dropdown => _DatabaseDropDown.cshtml
// 3. write selectedDB to hidden iput => id,name = SelectedDatabaseName
// 4. load Partners for selected DB
// 5. add selectedDB onchange event => reload partners + update hidden input value
var loadDatabaseNames = function () {
    $.ajax({
        url: "/Investors2015/DataEntry/GetDatabaseDropDownHtml",
        contentType: "application/html; charset=utf-8",
        type: "GET",
        dataType: "html"
    })
           .success(function (result) {
               // 2.
               $("#datubazeDropDownPlaceholder").html(result);
               // 3.
               var databaseElement = $("#SelectListDatabases_SelectedValue")[0];
               var selectedValue = databaseElement.options[databaseElement.selectedIndex].text;
               $("#SelectedDatabaseName").val(selectedValue);

               // 5.
               $("#SelectListDatabases_SelectedValue").on("change", function () {
                   $("#partnerDropDownPlaceholder").html("");
                   loadPartnerNames();
               });
           })
           .error(function (xhr, status) {
               alert("kļūdas paziņojums no JS funkcijas loadDatabaseNames() " + status);
           }).done(loadPartnerNames()); // 4.



};



//#endregion function loadDatabaseNames


// DOCUMENT READY procedures
// 1. LoadDatabases => automatically loads also partners
$("#datubazeDropDownPlaceholder").ready(function () {
    ////    // 1.
    ////    var databasePlaceholder = document.getElementById("SelectedDatabaseName");


    loadDatabaseNames();

});


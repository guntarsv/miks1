// $(function(){...}) ir tas pats kas $(document).ready(function() { ... });, respektīvi funkicju palaiž kad doks ir ielasīts

//nākamais koments (//@ sourceURL=test.js) nodrošina, ka brousera developer parādās iespēja debugot šo js failu!!
//@ sourceURL=test.js

// partneru ielasīšana AUTOCOMPLETE BOXĀ (ar papildinformāciju)
// 1. nolasām ierakstu skaitu
// 2. nomapojam ielasītos datus - lai iegūtu objektu ar 'label', 'value', 'partnerId', un 'papildinfo'
// 3. fiksējam atsevišķi papildinfo
// šī ir pašizpildoša funkcija, pie ielādes=documentReady
$(function () {
    
    var databaseElement = $("#SelectListDatabases_SelectedValue")[0];
    var selectedDatabaseIndex = databaseElement.selectedIndex;
    var selectedDatabaseName = databaseElement.options[selectedDatabaseIndex].text;

    // definējam SOURCE autocomplete mēŗķiem
    var source_PartnerSearch_AJAX = function(request, response) {
        $.ajax({
            url: "/Investors2015/DataEntry/GetTest",
            dataType: "json",
            data: {
                term: request.term,
                datubaze: selectedDatabaseName
            },
            success: function(data) {

                // 1
                var indeksis, ierakstuSkaits = 0;
                for (indeksis in data) {
                    ierakstuSkaits++;
                    if (indeksis == "skaits") {
                        var ierakstuSkaits = data[indeksis];
                    }
                }

                // 2 
                response($.map(data, function(value, key) {
                    // autocomplete sarakstā NEIEKĻAUJAM papildingo
                    // pārējo mapojam
                    if (key !== "skaits") {
                        return {
                            label: value,
                            value: value,
                            partnerId: key, // we define it here
                            skaits: ierakstuSkaits // we define it here
                        }
                    }
                    return null;
                }));
                // nākamā rinda izpildās, kad izciklējis visus atdodamos rezultātus, bet pirms esam selektējuši ierakstu
                $("#txtnumberOfPartners").val(ierakstuSkaits);
            }
    });
    };

    // pievienojam #txtPartnerSearchPhrase AUTOCOMPLETE eventu ar atbilstošo SOURCE
    $("#txtPartnerSearchPhrase").autocomplete({
        source: source_PartnerSearch_AJAX,
        ŗesponse: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "no partners found" };
                ui.content.push(noResult);
            }
        },
        // ar šo pietiktu, izņemot viena problēma = neizvēloties vērtību bet 'focusout', paliek ievadīta vērtība. tāpēc vajag 'change' zemāk
        select: function (event, ui) {
                    //console.log("select triggered. this value= " + this.value + " ui.item= " + ui.item.value);
                    $("#txtSearchPartnerId").val(ui.item.partnerId);
                    $("#txtnumberOfPartners").val(""); // izdzēšam pieejamo skaitu, kad ir izvēlēta viena vērtība
                },
        // šo nākamo vajag tikai tāpēc, ka citādi ir iespēja ievadīt vērtību ne no listes un tā paliks tur iekšā pie 'outfocus'
        change: function (event, ui) {
            if (ui.item) {
                $("#txtSearchPartnerId").val(ui.item.partnerId);
                $("#txtnumberOfPartners").val(""); // izdzēšam pieejamo skaitu, kad ir izvēlēta viena vērtība
                //console.log("ui.item.value: " + ui.item.value);
            } else {
                // šeit ui.item == null, kas nozīmē, ka vērtība ir ievadīta, bet nav izvēlēta no listes. tātad tā jādžēš ārā. ir noticis outfocus
                $("#txtPartnerSearchPhrase").val("");
                $("#txtSearchPartnerId").val("");
                $("#txtnumberOfPartners").val("");
            }
                //console.log("this.value: " + this.value);
        },
        min_length: 1,
        delay: 300
    });



    // attīram visus laukus, kad lietotājs uzklikšķina uz lauka
    $("#txtPartnerSearchPhrase").click(function() {
        $(this).val("");
        $("#txtSearchPartnerId").val("");
        $("#txtnumberOfPartners").val("");
    });

});


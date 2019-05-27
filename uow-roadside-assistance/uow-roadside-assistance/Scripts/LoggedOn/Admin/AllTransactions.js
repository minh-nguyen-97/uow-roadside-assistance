window.onload = function () {
    AdminService.getUserFromSession(function (session) {
        if (session == null) {
            window.location.href = '../../LoggedOff/Home.aspx'
        } else {
            var curUser = JSON.parse(session);
            if (curUser.UserType != 'Admin') {
                window.history.back();
            }
            else {
                // do stuff

                AdminService.getAllTransactions(function (result) {
                    var res = JSON.parse(result);

                    for (var i = 0; i < res.length; i++) {
                        addRow(res[i].TransactionID, res[i].ContractorID, res[i].CustomerID, res[i].Cost, res[i].TransactionDateToString);
                    }
                });
            }
        }
    });
}


function addRow(transactionID, contractorID, customerID, cost, transactionDateTime) {

    var tr = document.createElement('tr');
    tr.setAttribute("id", "rowFor" + transactionID);

    var rowContent =
        "<td>" + transactionID + "</td>" +
        "<td>" + contractorID + "</td>" +
        "<td>" + customerID + "</td>" +
        "<td>$" + cost + "</td>" +
        "<td>" + transactionDateTime + "</td>";

    tr.innerHTML = rowContent;

    document.getElementById('allTransactionsTable').appendChild(tr);
}


$(document).ready(function () {
    $('#FilterModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var filterType = button.data('filtertype') // Extract info from data-* attributes

        console.log(filterType);

        $('#filterType').text(filterType);
    });

    $('#searchForID').click(function (e) {

        var ID = $('#filterID').val();

        var filterable = false;

        if ($('#filterType').text() == 'Transaction') {
            filterable = filterable || searchIDInColumn(0, ID);
        }

        if ($('#filterType').text() == 'Contractor') {
            filterable = filterable || searchIDInColumn(1, ID);
        }

        if ($('#filterType').text() == 'Customer') {
            filterable = filterable || searchIDInColumn(2, ID);
        }

        if (filterable)
            $('#FilterModalCenter').modal('toggle');
    });

    $('.filterDateTime').datetimepicker({
        defaultDate: new Date(),
        format: 'YYYY/MM/DD HH:mm:ss',
        icons: {
            time: 'far fa-clock',
            date: 'fas fa-calendar-alt',
            up: 'fas fa-arrow-up',
            down: 'fas fa-arrow-down',
            previous: 'fas fa-chevron-left',
            next: 'fas fa-chevron-right',
            today: 'far fa-calendar-check',
            clear: 'fas fa-trash',
            close: 'fas fa-times'
        }
    });


    $('#filterDateTimeButton').click(function (e) {
        var table = document.getElementById('allTransactionsTable').childNodes;

        var fromDateTime = $('#FromDateTime').val();
        var toDateTime = $('#ToDateTime').val();

        for (var i = 1; i < table.length; i++) {

            if (table[i].style.display != 'none') {
                var datetime = table[i].childNodes[4];

                if (fromDateTime <= datetime.textContent && datetime.textContent <= toDateTime) {
                    table[i].style.display = 'table-row';
                } else {
                    table[i].style.display = 'none';
                }
            }
        }
    });

    $('#clearFilterButton').click(function (e) {
        var table = document.getElementById('allTransactionsTable').childNodes;

        for (var i = 1; i < table.length; i++) {
            table[i].style.display = 'table-row';
        }
    });
});

function searchIDInColumn(col, ID) {
    if (/^\d+$/.test(ID) == false) {
        alert('The ID must be an integer !!!');
        return false;
    }

    var table = document.getElementById('allTransactionsTable').childNodes;

    for (var i = 1; i < table.length; i++) {
        if (table[i].style.display != 'none') {
            var idInTable = table[i].childNodes[col];

            if (idInTable.textContent == ID) {
                table[i].style.display = 'table-row';
            } else {
                table[i].style.display = 'none';
            }
        }
    }

    return true;
}
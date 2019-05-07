window.onload = function () {
    ContractorService.getUserFromSession(function (session) {
        if (session == null) {
            window.location.href = '../../LoggedOff/Home.aspx'
        } else {
            var curUser = JSON.parse(session);
            if (curUser.UserType != 'Contractor') {
                window.history.back();
            }
            else {
                $('#UserNameLabel').text('Welcome, ' + curUser.FullName);
                loadIncompleteTransaction();
            }
        }
    });
}

function loadIncompleteTransaction() {
    ContractorService.contractorUnfinishedTransactions(function (result) {
        var res = JSON.parse(result);
        for (var i = 0; i < res.length; i++) {
            var transactionID = res[i].TransactionID;
            var customerID = res[i].CustomerID;
            var fee = res[i].Cost;

            ContractorService.getCustomerFullName(customerID, function (name) {
                addRow(name, fee, transactionID);
            })
        }
    })
}

function addRow(fullName, fee, transactionID) {
    var tr = document.createElement('tr');
    tr.setAttribute('id', 'rowFor' + transactionID);

    tr.innerHTML =
        "   <th scope = 'row'>" + fullName + "</th >" +
        "   <td>$" + fee + "</td>" +
        "   <td>" +
        "       <button onclick='finishedContractor(" + transactionID + ")' class='btn btn-success' > Completed Work</button > " +
        "    </td>";

    document.getElementById('incompleteCustomersTable').appendChild(tr);
}

function finishedContractor(transactionID) {
    var tr = document.getElementById('rowFor' + transactionID);
    document.getElementById('incompleteCustomersTable').removeChild(tr);
    ContractorService.finishedContractor(transactionID);
}
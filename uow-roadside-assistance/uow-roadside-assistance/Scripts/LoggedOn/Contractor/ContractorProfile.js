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
                loadFieldsFromSession(curUser);
            }
        }
    });
}

function loadFieldsFromSession(curUser) {
    $('#FullName').val(curUser.FullName);
    $('#Username').val(curUser.UserName);
    $('#Email').val(curUser.Email);

    $('#AccountName').val(curUser.AccountName);
    $('#AccountNumber').val(curUser.AccountNumber);
    $('#BSB').val(curUser.BSB);
}

$(document).ready(function () {
    $('#SaveChanges').click(function (e) {
        if (validateFields()) {
            var email = $('#Email').val();
            var accountName = $('#AccountName').val();
            var accountNumber = $('#AccountNumber').val();
            var bsb = $('#BSB').val();
            ContractorService.UpdateContractorProfile(email, accountName, accountNumber, bsb);

            alert('Updated profile successfully');
        }
    });
});

function validateFields() {

    var check = checkRequiredField('Email');

    check = checkRequiredField('AccountName') && check;
    check = checkRequiredField('AccountNumber') && check;
    check = checkRequiredField('BSB') && check;

    if (check) {
        var accNo = $('#AccountNumber').val();
        if (/^\d{9}$/.test(accNo)) {
            $('#AccountNumberErrMess').text('');
        } else {
            $('#AccountNumberErrMess').text('Account number is a 9-digit number!!!');
        }

        var BSB = $('#BSB').val();
        if (/^\d{6}$/.test(BSB)) {
            $('#BSBErrMess').text('');
        } else {
            $('#BSBErrMess').text('BSB is a 6-digit number!!!');
        }

        check = check && /^\d{9}$/.test(accNo) && /^\d{6}$/.test(BSB);
    }

    return check;
}

function checkRequiredField(fieldID) {
    var fieldVal = $('#' + fieldID).val();
    if (fieldVal == '') {
        $('#' + fieldID + 'ErrMess').text('Please fill this field!!!');
        return false;
    } else {
        $('#' + fieldID + 'ErrMess').text('');
        return true;
    }
};
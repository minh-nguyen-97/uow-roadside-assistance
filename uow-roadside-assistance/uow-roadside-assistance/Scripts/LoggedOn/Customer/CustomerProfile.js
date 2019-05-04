window.onload = function () {
    CustomerService.getUserFromSession(function (session) {
        if (session == null) {
            window.location.href = '../../LoggedOff/Home.aspx'
        } else {
            var curUser = JSON.parse(session);
            if (curUser.UserType != 'Customer') {
                window.history.back();
            }
            else {
                $('#UserNameLabel').text('Welcome, ' + curUser.FullName);
                loadFieldsFromSession(curUser);
            }
        }
    });
}

function loadFieldsFromSession(curUser)
{
    $('#FullName').val(curUser.FullName);
    $('#Username').val(curUser.UserName);
    $('#Email').val(curUser.Email);

    $('#CardHolder').val(curUser.CardHolder);
    $('#CardNo').val(curUser.CardNo);
    $('#ExpiryDate').val(curUser.ExpiryDate);
    $('#CVV').val(curUser.CVV);

    $('#Make').val(curUser.Make);
    $('#Model').val(curUser.Model);
    $('#Color').val(curUser.Color);
    $('#RegNo').val(curUser.RegNo);
}

$(document).ready(function () {
    $("#ExpiryDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: 'mm/y',
        onClose: function (dateText, inst) {
            $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
        }
    });

    $('#SaveChanges').click(function (e) {
        if (validateFields()) {
            var email = $('#Email').val();

            // 
            var regNo = $('#RegNo').val();
            var color = $('#Color').val();
            var make = $('#Make').val();
            var model = $('#Model').val();

            // third tab
            var cardHolder = $('#CardHolder').val();
            var expDate = $('#ExpiryDate').val();
            var cardNo = $('#CardNo').val();
            var CVV = $('#CVV').val();

            CustomerService.UpdateCustomerProfile(email, regNo, make, model, color, cardHolder, cardNo, expDate, CVV);

            alert('Updated profile successfully!!!');
        }
    });
});


function validateFields() {

    var check = checkRequiredField('Email');

    check = checkRequiredField('CardNo') && check;
    check = checkRequiredField('ExpiryDate') && check;
    check = checkRequiredField('CVV') && check;

    check = checkRequiredField('Make') && check;
    check = checkRequiredField('Model') && check;
    check = checkRequiredField('Color') && check;
    check = checkRequiredField('RegNo') && check;

    if (check) {
        var cardNo = $('#CardNo').val();
        if (/^\d{16}$/.test(cardNo)) {
            $('#CardNoErrMess').text('');
        } else {
            $('#CardNoErrMess').text('Account number is a 16-digit number!!!');
        }

        var CVV = $('#CVV').val();
        if (/^\d{3}$/.test(CVV)) {
            $('#CVVErrMess').text('');
        } else {
            $('#CVVErrMess').text('CVV is a 3-digit number!!!');
        }

        check = check && /^\d{16}$/.test(cardNo) && /^\d{3}$/.test(CVV);
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
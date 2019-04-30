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

    $(".previous").click(function (e) {
        $('#accordion').accordion('option', 'active', (
            $('#accordion').accordion('option', 'active') - 1));
    });

    $("#accordion").accordion();

    $("#next1").click(function (e) {
        validateFields(checkFirstFields(), 1);
    });

    $("#next2").click(function (e) {
        validateFields(checkSecondFields(), 2);
    });

    $('#signUpCustomerSubmit').click(function (e) {
        if (!validateThirdFields())
            return;

        var username = $('#Username').val();
        var email = $('#Email').val();
        var password = $('#Password').val();
        //var userType = 'Customer';
        var regNo = $('#RegistrationNo').val();
        var color = $('#Color').val();
        var make = $('#Make').val();
        var model = $('#Model').val();
        var cardHolder = $('#CardHolder').val();
        var expDate = $('#ExpiryDate').val();
        var cardNo = $('#CardNumber').val();
        var CVV = $('#CVV').val();

        LoggedOffService.createNewCustomer(username, email, password, regNo, make, model, color, cardHolder, cardNo, expDate, CVV, onCustomerRegister);
    });

    function onCustomerRegister(res) {
        if (res) {
            window.location.href = './SuccessfulRegistration.aspx';
        } else {
            alert('Username has been used. Please choose another !!!');
        }
    }

});


function validateThirdFields() {
    var check = checkRequiredField('CardHolder');
    check = checkRequiredField('ExpiryDate') && check;
    check = checkRequiredField('CardNumber') && check;
    check = checkRequiredField('CVV') && check;

    if (check) {
        var cardNo = $('#CardNumber').val();
        if (/^\d{16}$/.test(cardNo)) {
            $('#CardNumberErrMess').text('');
        } else {
            $('#CardNumberErrMess').text('Account number is a 16-digit number!!!');
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

//

function validateFirstFields() {
    validateFields(checkFirstFields(), 1);
}

function validateFirstSecondFields() {
    validateFields(checkFirstFields() && checkSecondFields(), 2);
}

// Validate general fields
function validateFields(condition, pos) {
    if (condition) {
        if ($($("#accordion > h3")[pos]).hasClass("ui-state-disabled"))
            $($("#accordion > h3")[pos]).removeClass("ui-state-disabled");
        $('#accordion').accordion('option', 'active', pos);
    }
    else {
        if (!$($("#accordion > h3")[pos]).hasClass("ui-state-disabled"))
            $($("#accordion > h3")[pos]).addClass("ui-state-disabled");
    }
}

// Validate Second Fields 
function checkSecondFields() {
    var check = checkRequiredField('RegistrationNo');
    check = checkRequiredField('Color') && check;
    check = checkRequiredField('Make') && check;
    check = checkRequiredField('Model') && check;

    return check;
}

// Validate First Fields

function checkFirstFields() {
    var check = checkRequiredField('Username');
    check = checkRequiredField('Email') && check;
    check = checkRequiredField('Password') && check;
    check = checkRequiredField('ConfirmedPassword') && check;

    if (check) {
        var same = ($('#Password').val() == $('#ConfirmedPassword').val());
        if (same)
            $('#ConfirmedPasswordErrMess').text('');
        else
            $('#ConfirmedPasswordErrMess').text('Passwords must be the same!!!');

        check = check && same;
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
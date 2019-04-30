$(document).ready(function () {

    $("#accordion").accordion();

    $("#next1").click(function (e) {
        validateFirstFields();
    });

    $(".previous").click(function (e) {
        $('#accordion').accordion('option', 'active', 0);
    });
    
    $("#signUpContractorSubmit").click(function (e) {
        if (validateSecondFields() == false)
            return;

        var username = $('#Username').val();
        var email = $('#Email').val();
        var password = $('#Password').val();
        var userType = 'Contractor';
        var accountName = $('#AccountName').val();
        var accountNumber = $('#AccountNumber').val();
        var BSB = $('#BSB').val();
        LoggedOffService.createNewContractor(username, email, password, accountName, accountNumber, BSB, onContractorRegister);
    });

    function onContractorRegister(res) {
        if (res)
            window.location.href = './SuccessfulRegistration.aspx';
        else
            alert('Username has been used. Please choose another !!!');
    }
    
});

// Validate Second Fields
function validateSecondFields() {

    var check = checkRequiredField('AccountName');
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

// Validate First Fields
function validateFirstFields() {
    if (checkFirstFields() == true) {
        if ($($("#accordion > h3")[1]).hasClass("ui-state-disabled"))
            $($("#accordion > h3")[1]).removeClass("ui-state-disabled");
        $('#accordion').accordion('option', 'active', 1);
    }
    else {
        if (!$($("#accordion > h3")[1]).hasClass("ui-state-disabled"))
            $($("#accordion > h3")[1]).addClass("ui-state-disabled");
    }
}

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


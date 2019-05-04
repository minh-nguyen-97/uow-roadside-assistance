$(document).ready(function () {
    $('#SaveChanges').click(function (e) {
        if (validateFields()) {
            var oldPass = $('#OldPassword').val();
            var newPass = $('#NewPassword').val();
            ContractorService.UpdateContractorPassword(oldPass, newPass, function (res) {
                if (res) {
                    alert('Update Password successfully!!!');
                    window.location.href = 'ContractorProfile.aspx';
                } else {
                    alert('Your old password is not correct. Please try again!!!');
                }
            });
        }
    });
});

function validateFields() {

    var check = checkRequiredField('OldPassword');
    check = checkRequiredField('NewPassword') && check;
    check = checkRequiredField('ConfirmedPassword') && check;

    if (check) {
        var newPass = $('#NewPassword').val();
        var confirmPass = $('#ConfirmedPassword').val();

        if (newPass != confirmPass) {
            $('#ConfirmedPasswordErrMess').text('Confirmed password must be the same with new one!!!');
        } else {
            $('#ConfirmedPasswordErrMess').text('');
        }

        check = check && (newPass == confirmPass);
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
var memberStatus, expDate, cancelled;

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

                memberStatus = curUser.MemberStatus;
                expDate = curUser.SubExpDateToString
                cancelled = curUser.SubCancelled;
                loadMembershipStatus(memberStatus, expDate, cancelled);
            }
        }
    });
}

function loadMembershipStatus(memberStatus, expDate, cancelled) {
    //console.log(memberStatus);
    //console.log(expDate);
    //console.log(cancelled);

    if (memberStatus == 'nonmember') {
        $('.subscribed').hide();
        $('#freeBody').css('background-color', '#F5FAFD');

    } else {
        $('#freeSubInfo').hide();

        if (memberStatus == 'monthly') {
            $('#monthlyBody').css('background-color', '#F5FAFD');
            $('#monthlyExpDate').text(expDate);

            $('#notSubscribeMonthly').hide();
            $('#subscribedYearly').hide();

            if (cancelled == false) {
                $('#monthlyExpiresAlert').hide();
            } else {
                $('#cancelMonthlyButton').hide();
            }
        }
        else {
            $('#yearlyBody').css('background-color', '#F5FAFD');
            $('#yearlyExpDate').text(expDate);

            $('#subscribedMonthly').hide();
            $('#monthlySubButton').hide();

            $('#notSubscribeYearly').hide();

            if (cancelled == false) {
                $('#yearlyExpiresAlert').hide();
            } else {
                $('#cancelYearlyButton').hide();
            }


        }
    }
}

$(document).ready(function () {


    $('#PaymentModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var subType = button.data('subtype') // Extract info from data-* attributes

        var modal = $(this)
        if (subType == 'monthly') {
            modal.find('#subType').text('MONTHLY')
            modal.find('#subType').css('color', '#d5d52c');
            modal.find('#confirmFee').text(50);
            modal.find('.acceptButton').attr('id', 'monthlySubAcceptButton');
        } else {
            modal.find('#subType').text('YEARLY')
            modal.find('#subType').css('color', 'orange');
            modal.find('#confirmFee').text(250);
            modal.find('.acceptButton').attr('id', 'yearlySubAcceptButton');
        }

        $('#monthlySubAcceptButton').click(function (e) {
            console.log('AAAAAA');
            CustomerService.subscribeMonthly();
            window.location.href = 'ManageSubscription.aspx';
        });

        //$('#cancelMonthlyButton').click(function (e) {
        //    CustomerService.cancelMonthly();
        //    $('#cancelMonthlyButton').hide();
        //    $('#monthlyExpiresAlert').show();
        //});


        $('#yearlySubAcceptButton').click(function (e) {
            CustomerService.subscribeYearly();
            window.location.href = 'ManageSubscription.aspx';
        });

        //$('#cancelYearlyButton').click(function (e) {
        //    CustomerService.cancelYearly();
        //    $('#cancelYearlyButton').hide();
        //    $('#yearlyExpiresAlert').show();
        //});
    });

    $('#CancelModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var subType = button.data('subtype') // Extract info from data-* attributes

        var modal = $(this)
        if (subType == 'monthly') {
            modal.find('#cancelSubType').text('MONTHLY')
            modal.find('#cancelSubType').css('color', '#d5d52c');
            modal.find('#cancelExpDate').text(expDate);
            modal.find('.confirmCancelButton').attr('id', 'confirmCancelMonthlyButton');
        } else {
            modal.find('#cancelSubType').text('YEARLY')
            modal.find('#cancelSubType').css('color', 'orange');
            modal.find('#cancelExpDate').text(expDate);
            modal.find('.confirmCancelButton').attr('id', 'confirmCancelYearlyButton');
        }

        $('#confirmCancelMonthlyButton').click(function (e) {
            CustomerService.cancelMonthly();
            $('#cancelMonthlyButton').hide();
            $('#monthlyExpiresAlert').show();
            $('#CancelModalCenter').modal('toggle');
        });
        

        $('#confirmCancelYearlyButton').click(function (e) {
            CustomerService.cancelYearly();
            $('#cancelYearlyButton').hide();
            $('#yearlyExpiresAlert').show();
            $('#CancelModalCenter').modal('toggle');
        });
    });

});
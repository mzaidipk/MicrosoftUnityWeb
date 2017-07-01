
var ist = {
    // Toaster Error Options
    toastrOptions: {
        tapToDismiss: true,
        extendedTimeOut: 0,
        timeOut: 0 // Set timeOut to 0 to make it sticky
    },
    //server exceptions enumeration 
    exceptionType: {
        WebGeneralException: 'WebGeneralException',
        UnspecifiedException: 'UnspecifiedException'
    }


};


// Busy Indicator
var spinnerVisibleCounter = 0;

// Show Busy Indicator
function showProgress() {
    ++spinnerVisibleCounter;
    if (spinnerVisibleCounter > 0) {
        $.blockUI({ message: "" });
        $("div#spinner").fadeIn("fast");
    }
};

// Hide Busy Indicator
function hideProgress() {
    --spinnerVisibleCounter;
    if (spinnerVisibleCounter <= 0) {
        spinnerVisibleCounter = 0;
        var spinner = $("div#spinner");
        spinner.stop();
        spinner.fadeOut("fast");
        $.unblockUI(spinner);

    }
};


//status decoder for parsing the exception type and message
amplify.request.decoders = {
    istStatusDecoder: function (data, status, xhr, success, error) {
        if (status === "success") {
            success(data);
        } else {
            if (status === "fail" || status === "error") {
                error(xhr.responseText);
                //var errorObject = {};
                //errorObject.errorType = ist.exceptionType.UnspecifiedException;
                //if (ist.verifyValidJSON(xhr.responseText)) {
                //    errorObject.errorDetail = JSON.parse(xhr.responseText);;
                //    if (errorObject.errorDetail.ExceptionType === ist.exceptionType.WebGeneralException) {
                //        error(errorObject.errorDetail.Message, ist.exceptionType.WebGeneralException);
                //    } else {
                //        error("Unspecified exception", ist.exceptionType.UnspecifiedException);
                //    }
                //} else {
                //    error(xhr.responseText);
                //}
            } else if (status === "nocontent") { // Added by ali : nocontent status is returned when no response is returned but operation is sucessful
                success(data);

            } else {
                error(xhr.responseText);
            }
        }
    }
};

// If while ajax call user shifts to another page then avoid error toasts
amplify.subscribe("request.before.ajax", function (resource, settings, ajaxSettings, ampXhr) {
    var _error = ampXhr.error;

    function error(data, status) {
        _error(data, status);
    }

    ampXhr.error = function (data, status) {
        if (ampXhr.status === 0) {
            return;
        }
        error(data, status);
    };

});

// Knockout Validation + Bindings

var ko = window["ko"];


require(["ko", "knockout-validation"], function (ko) {

    //Custom binding for handling validation messages in tooltip
    ko.bindingHandlers.validationTooltip = {
        update: function (element, valueAccessor) {
            var observable = valueAccessor(), $element = $(element);
            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $element.tooltip({ title: observable.error }); //, delay: { show: 10000, hide: 10000 }
                } else {
                    $element.tooltip('destroy');
                }
            }
        }
    };

});